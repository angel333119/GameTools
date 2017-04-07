using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameTools;
using System.IO;

namespace TestHotelDuskANMFRM {
    class Program {
        static void Main(string[] args) {

            bool flip = false;
            GTFS fs = new GTFS("Bracelet-F0.frm");
            int zero = GT.ReadInt32(fs, 4, flip);
            int len = GT.ReadInt32(fs, 4, flip);
            int sixtyfour = GT.ReadInt32(fs, 4, flip);
            int next = GT.ReadInt32(fs, 4, flip);

            List<byte> listBytes = new List<byte>();

            while(fs.Position - 16 < len) {
                byte first = GT.ReadByte(fs);

                if (first < 0x40 || first == 0x7F) { //0x7F for Br_bracelet_.anm
                    for (int i = 0; i < first; i++) {
                        byte b = GT.ReadByte(fs);
                        listBytes.Add(b);
                    }
                } else if (first == 0x40) {
                    throw new Exception();
                } else if (first < 0x80) {
                    int repeatlen = first - 0x40;
                    byte second = GT.ReadByte(fs);
                    for (int i = 0; i < repeatlen; i++) {
                        listBytes.Add(second);
                    }
                } else if (first == 0x80) {
                    throw new Exception();
                } else {
                    int gap = first - 0x80;
                    for (int i = 0; i < gap; i++) {
                        listBytes.Add(0xFF);
                    }
                }
                
            }
            
            FileStream nf = new FileStream("Test-HTD-ANMFRM.bin", FileMode.Create);
            foreach (byte b in listBytes) {
                nf.WriteByte(b);
            }
            nf.Close();

        }
    }
}
