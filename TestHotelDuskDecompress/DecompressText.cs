using GameTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHotelDuskDecompress {
    public static class DecompressText {
        public static void Decompress(string file) { //"EP1_BOOK.txt"
            GTFS fs = new GTFS(file);
            byte[] header = GT.ReadBytes(fs, 4, false);
            int sizeun = GT.ReadInt32(fs, 4, false);
            int sizeco = GT.ReadInt32(fs, 4, false);
            int zero = GT.ReadInt32(fs, 4, false);

            string uncompressed = "";

            while (fs.Position < sizeco) { // Wrong but it'll do?
                byte input = GT.ReadByte(fs);
                BitArray bits = new BitArray(new byte[] { input });

                for (int i = 0; i < 8; i++) {
                    if (bits[i]) {
                        byte b = GT.ReadByte(fs);
                        uncompressed += (char)b;
                    } else {
                        byte[] lookup = GT.ReadBytes(fs, 3, false);
                        int len = 4 + lookup[2];

                        int offset = lookup[0] + 3;

                        if (lookup[1] < 255 && lookup[1] > 10) {
                            offset -= 256 * (255 - lookup[1]);
                            string add = uncompressed.Substring(offset, len);
                            uncompressed += add;
                        } else {
                            if (lookup[1] < 127)
                                offset += 256 * (lookup[1] + 1);
                            else
                                throw new Exception();

                            string add = uncompressed.Substring(offset, len);

                            uncompressed += add;
                        }
                    }
                }
            }

            Console.WriteLine(uncompressed);
            Console.WriteLine();
        }
    }
}
