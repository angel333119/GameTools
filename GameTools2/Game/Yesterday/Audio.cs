using GameTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools2.Game.Yesterday {
    class Audio {

        public static void Open(string file, string outdir) {
            bool flip = false;
            GTFS fs = new GTFS(file);

            string newDir = file.Replace('.', '-');//.Replace("\\uk", "");
            //if (newDir.Contains("uk"))
                //throw new Exception();

            uint count = GT.ReadUInt32(fs, 4, flip) / 4;
            uint[] ExOffset = new uint[count];
            uint[] ExSize = new uint[count];
            uint[] ExUnknown = new uint[count];
            byte[] ExType = new byte[count];

            if (count > 0) Directory.CreateDirectory(newDir);

            for (int i = 0; i < count; i++) {
                ExOffset[i] = GT.ReadUInt32(fs, 4, flip);
                ExSize[i] = GT.ReadUInt32(fs, 4, flip);
                ExUnknown[i] = GT.ReadUInt32(fs, 4, flip);
                ExType[i] = GT.ReadByte(fs);
            }

            for (int i = 0; i < count; i++) {
                string ext = "bin";
                if (ExType[i] == 0x00) ext = "wav";
                else if (ExType[i] == 0x02) ext = "ogg";

                if (!File.Exists(newDir + "\\" + i + "." + ext)) {
                    string newfile = newDir + "\\" + i + "." + ext;
                    GT.WriteSubFile(fs, newfile, ExSize[i], ExOffset[i]);
                }
            }

        }
    }
}
