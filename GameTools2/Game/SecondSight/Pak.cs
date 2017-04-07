using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools2.Game.SecondSight {
    class Pak {

        public static List<string> Open(string file, string outdir) {
            GTFS fs = new GTFS(file);

            byte[] bHeader = GT.ReadBytes(fs, 4, false);
            bool flip = false;

            List<Pack> pack = new List<Pack>();

            if (bHeader[1] == 0x38) {
                //TS2 P8CK
                long lOffsetTail = GT.ReadUInt32(fs, 4, flip);
                long lNumResources = GT.ReadUInt32(fs, 4, flip);
                long lOffsetStringTable = GT.ReadUInt32(fs, 4, flip);
                long lOffsetStringSize = GT.ReadUInt32(fs, 4, flip);

                string hoho = GT.ReadASCII(fs, 12, flip);

                fs.Position = lOffsetTail;
                for (int i = 0; i < lNumResources; i++) {
                    long lOffsetFileName = GT.ReadUInt32(fs, 4, flip);
                    long lFileSize = GT.ReadUInt32(fs, 4, flip);
                    long lOffsetFile = GT.ReadUInt32(fs, 4, flip);

                    long lStart = lOffsetStringTable + lOffsetFileName;
                    string sString = GT.ReadASCIItoNull(fs, lStart, flip);

                    pack.Add(new Pack(sString, lOffsetFile, lFileSize));
                }
            } else if (bHeader[1] == 0x34) {
                long lOffsetTail = GT.ReadUInt32(fs, 4, flip);
                long lTailSize = GT.ReadUInt32(fs, 4, flip);
                long lReserved = GT.ReadUInt32(fs, 4, flip);

                fs.Position = lOffsetTail;
                long lNumResources = lTailSize / 60;
                for (int i = 0; i < lNumResources; i++) {
                    long lFileName = lOffsetTail + GT.ReadUInt32(fs, 4, false);
                    long lOffsetFile = GT.ReadUInt32(fs, 4, false);
                    long lFileSize = GT.ReadUInt32(fs, 4, false);
                    byte[] bOffR = GT.ReadBytes(fs, 4, false);

                    string sString = GT.ReadASCIItoNull(fs, lFileName, flip);

                    pack.Add(new Pack(sString, lOffsetFile, lFileSize));
                }            
            } else {
                throw new Exception();
            }

            List<string> listFiles = new List<string>();
            foreach (Pack pf in pack) {
                pf.WriteOut(fs, outdir);
                listFiles.Add(pf.Filename);
            }
            return listFiles;
        }

    }
}
