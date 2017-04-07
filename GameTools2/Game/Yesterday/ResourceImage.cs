using GameTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools2.Game.Yesterday {
    class ResourceImage {

        public static void Open(string file, string outdir) {
            bool flip = false;
            GTFS fs = new GTFS(file);
            uint count = GT.ReadUInt32(fs, 4, flip) / 4;

            if (count < 300) {

                uint[] ExOffset = new uint[count];
                uint[] ExSize = new uint[count];

                //Outdir is wrong, but other than that this is fine.

                if (count > 0) Directory.CreateDirectory(outdir);

                for (int i = 0; i < count; i++)
                    ExOffset[i] = GT.ReadUInt32(fs, 4, flip);
                for (int i = 0; i < count; i++)
                    ExSize[i] = GT.ReadUInt32(fs, 4, flip);

                for (int i = 0; i < count; i++) {
                    if (ExSize[i] > 0) {
                        string ext = "bin";
                        uint head = GT.ReadUInt32(fs, 4, flip);
                        if (head == 0x474E5089) ext = "png";
                        else if (head == 0xE0FFD8FF || head == 0xE1FFD8FF) ext = "jpg";

                        if (!File.Exists(outdir + "\\" + i + "." + ext)) {
                            string newfile = outdir + "\\" + i + "." + ext;
                            GT.WriteSubFile(fs, newfile, ExSize[i], ExOffset[i]);

                            /*
                            if (ext == "bin") { //Might be a Resource pack
                                string newDirIn = outdir + "\\" + i + "-" + ext;
                                ResourceImage.Open(outdir + "\\" + i + "." + ext, newDirIn);
                            }
                            */
                        }
                    }
                }

            } //End Count limit
        }
    }
}
