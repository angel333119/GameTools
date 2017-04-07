using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameTools;
using System.Collections;
using System.IO;

namespace TestHotelDuskDecompress {
    class Program {
        static void Main(string[] args) {

            GTFS fs = new GTFS("AC_01L.bin");
            byte[] header = GT.ReadBytes(fs, 4, false);
            int sizeun = GT.ReadInt32(fs, 4, false);
            int sizeco = GT.ReadInt32(fs, 4, false);
            int zero = GT.ReadInt32(fs, 4, false);

            byte[] uncompressed = new byte[sizeun];
            int pos = 0;

            while(fs.Position < sizeco) { // Wrong but it'll do?
                byte input = GT.ReadByte(fs);
                BitArray bits = new BitArray(new byte[] { input });

                for(int i = 0; i < 8; i++) {
                    if(bits[i]) {
                        byte b = GT.ReadByte(fs);
                        uncompressed[pos++] = b;
                    } else {
                        byte[] lookup = GT.ReadBytes(fs, 3, false);
                        int len = 4 + lookup[2];

                        int offset = lookup[0] + 3;

                        if (lookup[1] < 255 && lookup[1] > 10) {
                            offset -= 256 * (255 - lookup[1]);

                            if(offset >= 0)
                                Array.Copy(uncompressed, offset, uncompressed, pos, len);
                            else {
                                for(int x = 0; x < len; x++)
                                    uncompressed[pos + x] = 0x00; // This is wrong
                            }

                            pos += len;
                        } else {
                            if (lookup[1] < 127)
                                offset += 256 * (lookup[1] + 1);
                            else if(lookup[1] != 255)
                                throw new Exception();

                            Array.Copy(uncompressed, offset, uncompressed, pos, len);
                            pos += len;
                        }
                    }
                }
            }

            File.WriteAllBytes("HotelDuskDecompress.Test.bin", uncompressed);
        }
    }
}
