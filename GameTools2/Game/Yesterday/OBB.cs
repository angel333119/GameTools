using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GameTools2.Game.Yesterday {
    class OBB {

        public static List<string> Open(string file, string outdir) {
            GTFSSlow fs = new GTFSSlow(file);
            bool flip = false;

            Directory.CreateDirectory(outdir + "\\Dataa");
            Directory.CreateDirectory(outdir + "\\Datav");
            Directory.CreateDirectory(outdir + "\\Resource");

            uint numFiles = GT.ReadUInt32(fs, 4, flip);
            List<Pack> obb = new List<Pack>();

            for (int i = 0; i < numFiles; i++) {
                uint inNameLen = GT.ReadUInt32(fs, 4, flip);

                string inName = string.Empty;
                for (int k = 0; k < inNameLen; k++)
                    inName += (char)GT.ReadByte(fs);

                string dest = "Resource\\";
                if (inName[0] == 'V') dest = "Datav\\";
                else if (inName[0] == 'D') dest = "Dataa\\";

                uint inStart = GT.ReadUInt32(fs, 4, flip);
                uint inLen = GT.ReadUInt32(fs, 4, flip);

                obb.Add(new Pack(dest + inName, inStart, inLen));
            }

            List<string> listFiles = new List<string>();
            foreach (Pack pf in obb) {
                pf.WriteOut(fs, outdir);
                listFiles.Add(pf.Filename);
            }
            return listFiles;
        }

    }
}
