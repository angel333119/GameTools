using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools2.Game.Yesterday {
    class Video {

        public static void Open(string file, string outdir) {
            bool flip = false;
            GTFS fs = new GTFS(file);

            string newDir = file.Replace('.', '-');//.Replace("\\uk", "");
            //if (newDir.Contains("uk"))
                //throw new Exception();

            uint head = GT.ReadUInt32(fs, 4, flip);

            string ext = "txt";
            if (head == 0x46464952) ext = "mp4"; //Android: RIFF
            else if (head == 0x694B4942) ext = "bik"; //Steam: BIKi

            if (!System.IO.File.Exists(newDir + "." + ext)) {
                //Copy the file under a new name
                System.IO.File.Copy(file, newDir + "." + ext);
            }
        }
    }
}
