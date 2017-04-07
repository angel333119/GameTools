using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GameTools;
using System.IO;

namespace USBIPExplorer {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Wireshark pcapng|*.pcapng|All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {
                List<byte[]> listTCPdata = new List<byte[]>();

                bool flip = false;
                GTFS fs = new GTFS(openFileDialog.FileName);
                //bool linux = false;
                int wiresharkNo = 0;

                while (fs.Position < fs.Length) {
                    bool setflip = flip;

                    int blockType = GT.ReadInt32(fs, 4, false);
                    int blockLength = GT.ReadInt32(fs, 4, false);

                    if (blockType == 0xA0D0D0A) {
                        //Section Header Block
                        int BOM = GT.ReadInt32(fs, 4, false);
                        byte[] blockBodyRem = GT.ReadBytes(fs, blockLength - 16, flip);

                        if (BOM == 0x1a2b3c4d)
                            setflip = true;
                        else if (BOM == 0x4D3C2B1A)
                            setflip = false;
                        else
                            Console.WriteLine();

                    } else if (blockType == 0x01) {
                        //Interface Description Block
                        flip = false;
                        int LinkType = GT.ReadInt16(fs, 2, flip); //1 is ethernet
                        int Reserved = GT.ReadInt16(fs, 2, flip);
                        int SnapLen = GT.ReadInt32(fs, 4, flip);
                        //Options
                        byte[] remaining = GT.ReadBytes(fs, blockLength - 12 - 8, flip);

                        //I don't suspect there's any data in here I need
                    } else if (blockType == 0x05) {
                        //Interface Statistics Block
                        //Nothing special
                        byte[] blockBody = GT.ReadBytes(fs, blockLength - 12, flip);
                    } else if (blockType == 0x06) {
                        wiresharkNo++;
                        flip = false;
                        //Enhanced Packet Block
                        int InterfaceID = GT.ReadInt32(fs, 4, flip);
                        int TimestampHigh = GT.ReadInt32(fs, 4, flip);
                        int TimestampLow = GT.ReadInt32(fs, 4, flip);
                        int CapturedLength = GT.ReadInt32(fs, 4, flip);
                        int PacketLength = GT.ReadInt32(fs, 4, flip);

                        int CapturedLenRem = CapturedLength;

                        //-----
                        byte[] MACdestination = GT.ReadBytes(fs, 6, flip);
                        byte[] MACsource = GT.ReadBytes(fs, 6, flip);
                        int type = GT.ReadInt16(fs, 2, flip);
                        CapturedLenRem -= 14;

                        if (type == 8) { //IPv4
                            flip = true;
                            byte version = GT.ReadByte(fs);
                            byte dsf = GT.ReadByte(fs);
                            int totallen = GT.ReadInt16(fs, 2, flip);
                            int id = GT.ReadInt16(fs, 2, flip);
                            byte flags = GT.ReadByte(fs);
                            int fragmentOffset = GT.ReadByte(fs);
                            byte ttl = GT.ReadByte(fs);
                            byte protocol = GT.ReadByte(fs);
                            int headerChecksum = GT.ReadInt16(fs, 2, flip);
                            byte[] IPsource = GT.ReadBytes(fs, 4, flip);
                            byte[] IPdestination = GT.ReadBytes(fs, 4, flip);
                            CapturedLenRem -= 20;

                            if (protocol == 0x01) { //ICMP
                                byte ICMP_type = GT.ReadByte(fs);
                                byte ICMP_code = GT.ReadByte(fs);
                                int checksum = GT.ReadInt16(fs, 2, flip);
                                int identifier = GT.ReadInt16(fs, 2, flip);
                                int seqnum = GT.ReadInt16(fs, 2, flip);
                                byte[] remaining = GT.ReadBytes(fs, 32, flip);
                                CapturedLenRem -= 40;
                            } else if (protocol == 0x06) { //TCP
                                int srcPort = GT.ReadUInt16(fs, 2, flip);
                                int destPort = GT.ReadUInt16(fs, 2, flip);
                                int seqnum = GT.ReadInt16(fs, 4, false);
                                int acknum = GT.ReadInt16(fs, 4, false);
                                int headerLen = GT.ReadByte(fs) / 4;
                                byte flag = GT.ReadByte(fs);
                                int windowSize = GT.ReadUInt16(fs, 2, flip);
                                int checksum = GT.ReadInt16(fs, 2, flip);
                                int urgent = GT.ReadInt16(fs, 2, flip);
                                CapturedLenRem -= 20;

                                Console.WriteLine(seqnum.ToString() + " / " + acknum.ToString());

                                if (headerLen > 20) {
                                    byte[] options = GT.ReadBytes(fs, headerLen - 20, flip);
                                    CapturedLenRem -= options.Length;
                                }

                                if((flag & 0x08) == 0x08) { //PSH flag
                                    byte[] remainingData = GT.ReadBytes(fs, CapturedLenRem, false);
                                    CapturedLenRem -= CapturedLenRem;

                                    listTCPdata.Add(remainingData);
                                }

                            } else if (protocol == 0x11) { //UDP
                                int srcPort = GT.ReadUInt16(fs, 2, flip);
                                int destPort = GT.ReadUInt16(fs, 2, flip);
                                int length = GT.ReadInt16(fs, 2, flip);
                                int checksum = GT.ReadInt16(fs, 2, flip);
                                CapturedLenRem -= 8;
                                byte[] remaining = GT.ReadBytes(fs, CapturedLenRem, flip);
                                CapturedLenRem -= CapturedLenRem;
                            } else {
                                Console.WriteLine("???");
                            }
                        } else {
                            Console.WriteLine("UNKNOWN");
                        }

                        int padding = blockLength - 32 - CapturedLength;
                        if (padding > 0)
                            GT.ReadBytes(fs, padding, flip);
                        else if(CapturedLenRem > 0)
                            GT.ReadBytes(fs, CapturedLenRem, flip);
                    } else {
                        Console.WriteLine("Block type: " + blockType.ToString("X2"));
                        byte[] blockBody = GT.ReadBytes(fs, blockLength - 12, flip);
                    }

                    int blockLength2 = GT.ReadInt32(fs, 4, false);
                    if (blockLength != blockLength2)
                        Console.WriteLine("Lengths not equal. Block definition wrong.");

                    flip = setflip;
                }

                MemoryStream stream = new MemoryStream();
                foreach (byte[] b in listTCPdata)
                    stream.Write(b, 0, b.Length);
                GTFS usbIPdata = new GTFS(stream.ToArray());
                usbIPdata.WriteBytesToFile("usbip.bin");

                while(usbIPdata.Position < usbIPdata.Length) {
                    var proto = Protocol.Get(usbIPdata);
                    Console.WriteLine(proto.ToString());
                }
            }
        }
    }
}
