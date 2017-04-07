using GameTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace GT_KyleHyde {
    static class HotelDuskANM {
        public static string extract_path = @"C:\Users\jas2o\Desktop\GT-HotelDusk\Extract";

        public static void OpenANM(OpenFileDialog openFileDialog) {
            bool flip = false;
            GTFS fs = new GTFS(openFileDialog.FileName);

            uint unk1 = GT.ReadUInt32(fs, 4, flip);
            uint numFrames = GT.ReadUInt32(fs, 4, flip);
            uint numFramesHeaderLen = GT.ReadUInt32(fs, 4, flip);
            uint unk2 = GT.ReadUInt32(fs, 4, flip);

            ushort height = GT.ReadUInt16(fs, 2, flip); //<<<--------- WRONG FROM HERE
            ushort width = GT.ReadUInt16(fs, 2, flip);
            uint unk3 = GT.ReadUInt32(fs, 4, flip);
            uint unk4 = GT.ReadUInt32(fs, 4, flip);
            uint unk5 = GT.ReadUInt32(fs, 4, flip);

            List<Pack> listFrames = new List<Pack>();

            for (int i = 0; i < numFrames; i++) {
                uint frameOffset = GT.ReadUInt32(fs, 4, flip);
                uint frameLen = GT.ReadUInt32(fs, 4, flip);
                uint frameUnk = GT.ReadUInt32(fs, 4, flip);
                uint framePad = GT.ReadUInt32(fs, 4, flip);

                string name = "Frame " + i + ".frm";
                listFrames.Add(new Pack(name, frameOffset, frameLen));
            }

            string toFolder = openFileDialog.SafeFileName.Replace('.', '_');

            if (!Directory.Exists(extract_path))
                Directory.CreateDirectory(extract_path);

            if (!Directory.Exists(extract_path + "\\" + toFolder))
                Directory.CreateDirectory(extract_path + "\\" + toFolder);

            foreach (Pack frame in listFrames) {
                string newfile = extract_path + "\\" + toFolder + "\\" + frame.Filename;
                GT.WriteSubFile(fs, newfile, frame.Size, frame.Offset);
            }
        }
    }
}