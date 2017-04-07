using GameTools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GT_KyleHyde {
    class Decompress {

        public static GTFS ToGTFS(GTFS fs, int diff) {
            fs.Position = 0;

            byte[] header = GT.ReadBytes(fs, 4, false);
            int sizeun = GT.ReadInt32(fs, 4, false);
            int sizeco = GT.ReadInt32(fs, 4, false);
            int zero = GT.ReadInt32(fs, 4, false);

            byte[] uncompressed = new byte[sizeun];
            int pos = 0;

            while (fs.Position < sizeco + 16) { // Wrong but it'll do?
                byte input = GT.ReadByte(fs);
                BitArray bits = new BitArray(new byte[] { input });

                for (int i = 0; i < 8; i++) {
                    if (fs.Position >= sizeco + 16)
                        break;

                    if (bits[i]) {
                        byte b = GT.ReadByte(fs);
                        uncompressed[pos++] = b;
                    } else {
                        int offset = GT.ReadInt16(fs, 2, false);
                        fs.Position -= 2;
                        byte[] bOff = GT.ReadBytes(fs, 2, false);

                        /*
                        Console.WriteLine(bOff[0] + ", " + bOff[1]);

                        //int offsetOrg = offset;

                        offset += 259;
                        int len = 4 + GT.ReadByte(fs);

                        if (offset < 0) {
                            for (int x = 0; x < len; x++)
                                uncompressed[pos + x] = uncompressed[0];
                        } else {
                            for (int x = 0; x < len; x++)
                                uncompressed[pos + x] = uncompressed[offset + x];
                        }
                        */

                        int len = 4 + GT.ReadByte(fs);

                        /*
                        if((bOff[1] & 0x80) == 0x80) {
                            uncompressed[pos + 0] = 0xFF;
                            uncompressed[pos + 1] = bOff[0];
                            uncompressed[pos + 2] = bOff[1];
                            uncompressed[pos + 3] = (byte)len;

                            for (int x = 4; x < len; x++)
                                uncompressed[pos + x] = 0;
                        } else {
                        */
                            offset += 259;
                        if(offset == -1) {
                            for (int x = 0; x < len; x++)
                                uncompressed[pos + x] = uncompressed[0];
                        } else if (offset < 0) {
                            //offset -= diff;
                            
                        } else {
                            for (int x = 0; x < len; x++)
                                uncompressed[pos + x] = uncompressed[offset + x];
                        }
                        //}

                        pos += len;
                    }
                }
            }

            return new GTFS(uncompressed);
        }

    }
}
