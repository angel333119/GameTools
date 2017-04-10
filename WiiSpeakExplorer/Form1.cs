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

namespace WiiSpeakExplorer {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "Wireshark pcapng|*.pcapng|All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {
                List<USB.URB> listURB = new List<USB.URB>();

                bool flip = false;
                GTFS fs = new GTFS(openFileDialog.FileName);
                bool linux = false;

                while (fs.Position < fs.Length) {
                    int blockType = GT.ReadInt32(fs, 4, flip);
                    int blockLength = GT.ReadInt32(fs, 4, flip);

                    if (blockType == 0xA0D0D0A) {
                        //Header
                        byte[] blockBody = GT.ReadBytes(fs, blockLength - 12, flip);
                    } else if (blockType == 0x01) {
                        //Interface Description Block
                        int LinkType = GT.ReadInt16(fs, 2, flip); //220 is USB Linux, 249 is USB Windows
                        int Reserved = GT.ReadInt16(fs, 2, flip);
                        int SnapLen = GT.ReadInt32(fs, 4, flip);
                        //Options
                        byte[] remaining = GT.ReadBytes(fs, blockLength - 12 - 8, flip);

                        if (LinkType == 220)
                            linux = true;
                        //I don't suspect there's any data in here I need
                    } else if (blockType == 0x05) {
                        //Interface Statistics Block
                        //Nothing special
                        byte[] blockBody = GT.ReadBytes(fs, blockLength - 12, flip);
                    } else if (blockType == 0x06) {
                        //Enhanced Packet Block
                        int InterfaceID = GT.ReadInt32(fs, 4, flip);
                        int TimestampHigh = GT.ReadInt32(fs, 4, flip);
                        int TimestampLow = GT.ReadInt32(fs, 4, flip);
                        int CapturedLength = GT.ReadInt32(fs, 4, flip);
                        int PacketLength = GT.ReadInt32(fs, 4, flip);

                        USB.URB urb;
                        if(linux) urb = new USB.URBLinux(fs, PacketLength);
                        else urb = new USB.URBWindows(fs, PacketLength);
                        listURB.Add(urb);

                        int paddingMod = CapturedLength % 4;
                        if (paddingMod > 0)
                            GT.ReadBytes(fs, 4 - paddingMod, flip);
                    } else {
                        Console.WriteLine("Block type: " + blockType.ToString("X2"));
                        byte[] blockBody = GT.ReadBytes(fs, blockLength - 12, flip);
                    }

                    int blockLength2 = GT.ReadInt32(fs, 4, flip);
                    if (blockLength != blockLength2)
                        Console.WriteLine("Lengths not equal. Block definition wrong.");
                }

                var listURB_Iso = listURB.Where(p => p.transfer_type == USB.URB.TransferType.Isochronous);
                var listURB_Iso_In = listURB_Iso.Where(p => p.GetEndpointDirection() == USB.URB.EndpointDirection.In);
                var listURB_Iso_Out = listURB_Iso.Where(p => p.GetEndpointDirection() == USB.URB.EndpointDirection.Out);

                FileStream fsIsoIn = new FileStream("iso_in.bin", FileMode.Create);
                BinaryWriter binaryIsoIn = new BinaryWriter(fsIsoIn);

                FileStream fsIsoOut = new FileStream("iso_out.bin", FileMode.Create);
                BinaryWriter binaryIsoOut = new BinaryWriter(fsIsoOut);

                foreach (USB.URB urb in listURB_Iso_In) {
                    foreach (USB.isodesc desc in urb.listIsodesc) {
                        if (desc.data != null)
                            binaryIsoIn.Write(desc.data);
                    }
                }

                foreach (USB.URB urb in listURB_Iso_Out) {
                    foreach (USB.isodesc desc in urb.listIsodesc) {
                        if (desc.data != null)
                            binaryIsoOut.Write(desc.data);
                    }
                }

                binaryIsoIn.Close();
                binaryIsoOut.Close();

                //--

                foreach(USB.URB urb in listURB) {
                    string line = urb.ToString();

                    WiiSpeak.Message msg = null;
                    if (urb.remaining.Length == 10) {
                        msg = new WiiSpeak.Message(urb.remaining);
                        line += string.Format(" {0,14} = {1}", msg.Type.ToString(), GT.ByteArrayToString(urb.remaining, " "));
                    } else if(urb.remaining.Length > 0 && urb.remaining.Length < 10) {
                        line += string.Format(" = {0}", GT.ByteArrayToString(urb.remaining, " "));
                    }

                    listBoxMessages.Items.Add(line);
                }

            }
        }
    }
}
