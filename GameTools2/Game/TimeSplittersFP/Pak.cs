using GameTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools2.Game.TimeSplittersFP {
    class Pak {

        public static List<string> Open(string file, string outdir) {
            GTFS fs = new GTFS(file);

            byte[] bHeader = GT.ReadBytes(fs, 4, false);
            bool flip = false;

            List<Pack> pack = new List<Pack>();

            if (bHeader[1] == 0x35) { //P5CK (TS:FP GC)

                bool isGamecube = false;
                Dictionary<string, string> c2n = new Dictionary<string, string>();
                string filec2n = file.ToLower().Replace(".pak", ".c2n");
                if (File.Exists(filec2n)) {
                    isGamecube = true;

                    GTFS cs = new GTFS(filec2n);

                    while (true) {
                        byte[] ox = GT.ReadBytes(cs, 2, flip);

                        if (ox[0] == 0x30 && ox[1] == 0x78) {
                            string sAddress = GT.ReadASCII(cs, 8, flip);
                            byte[] blank = GT.ReadBytes(cs, 2, flip);
                            string sString = GT.ReadASCIItoNull(cs, cs.Position, flip, 0x0A);
                            cs.Position += sString.Length + 1;

                            if (!c2n.ContainsKey(sAddress))
                                c2n.Add(sAddress, sString);
                        } else
                            break;
                    }
                }

                //---

                long lOffsetTail = GT.ReadUInt32(fs, 4, flip);
                long lTailSize = GT.ReadUInt32(fs, 4, flip);
                long lReserved = GT.ReadUInt32(fs, 4, flip);

                long lNumResources = lTailSize / 16;

                fs.Position = lOffsetTail;
                for (int i = 0; i < lNumResources; i++) {
                    byte[] bOff1 = GT.ReadBytes(fs, 4, true);
                    long lOffsetFile = GT.ReadUInt32(fs, 4, flip); //PS2

                    long lFileSizePS2 = GT.ReadUInt32(fs, 4, flip);
                    long lFileSizeGC = GT.ReadUInt32(fs, 4, flip);

                    string sMemory = BitConverter.ToString(bOff1).Replace("-", "").ToLower();

                    long lFileSize = (isGamecube ? lFileSizeGC : lFileSizePS2);

                    string sString;
                    if (c2n.ContainsKey(sMemory)) // Should only be used for GC
                        sString = c2n[sMemory];
                    else // Should only be used for PS2
                        sString = sMemory;

                    //Console.WriteLine(sMemory + "," + lOffsetFile + "," + lFileSize);

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
