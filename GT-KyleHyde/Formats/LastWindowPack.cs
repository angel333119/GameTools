using System;
using System.IO;
using GameTools;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GT_KyleHyde
{
    static class LastWindowPack
    {
        public static string extract_path = @"C:\Users\jas2o\Desktop\GT-LastWindow\Extract";

        public static void OpenPack(OpenFileDialog openFileDialog)
        {
            bool flip = true;
            GTFS fs = new GTFS(openFileDialog.FileName);

            uint nothing = GT.ReadUInt32(fs, 4, flip);
            uint numFiles = GT.ReadUInt32(fs, 4, flip);
            uint offset = GT.ReadUInt32(fs, 4, flip); // + 4 I guess?

            uint unknown = GT.ReadUInt32(fs, 4, flip);

            List<Pack> listPack = new List<Pack>();

            for (int i = 0; i < numFiles; i++)
            {
                byte nameLen = GT.ReadByte(fs);
                string name = GT.ReadASCII(fs, nameLen, false);
                int fileLen = GT.ReadInt32(fs, 4, flip);

                listPack.Add(new Pack(name, -1, fileLen));
            }

            string toFolder = openFileDialog.SafeFileName.Replace('.', '_');

            if (!Directory.Exists(extract_path))
                Directory.CreateDirectory(extract_path);

            if (!Directory.Exists(extract_path + "\\" + toFolder))
                Directory.CreateDirectory(extract_path + "\\" + toFolder);

            foreach(Pack pack in listPack) {
                string newfile = extract_path + "\\" + toFolder + "\\" + pack.Filename;
                GT.WriteSubFile(fs, newfile, (int)pack.Size);
            }
        }

    }
}
