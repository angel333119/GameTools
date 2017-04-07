using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiiSpeakExplorer {
    namespace USB {

        public class isodesc {

            public int status, offset, length;
            public byte[] data;

            public isodesc(GTFS fs, bool linux) {
                bool flip = false;

                if (linux) {
                    status = GT.ReadInt32(fs, 4, flip);
                    offset = GT.ReadInt32(fs, 4, flip);
                    length = GT.ReadInt32(fs, 4, flip);
                    int padding = GT.ReadInt32(fs, 4, flip);
                } else {
                    offset = GT.ReadInt32(fs, 4, flip);
                    length = GT.ReadInt32(fs, 4, flip);
                    status = GT.ReadInt32(fs, 4, flip);
                }
            }

            public void ReadData(byte[] alldata) {
                data = new byte[length];
                Array.Copy(alldata, offset, data, 0, length);
            }

        }

    }
}
