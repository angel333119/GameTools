using GameTools;
using GameTools3D.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GameTools2.Game.TimeSplitters2 {
    class TextureGCN : Texture {

        public TextureGCN(string f, string sf, bool useGTFSView = false) : this(f, sf, new GTFS(f), useGTFSView) { }

        public TextureGCN(string f, string sf, GTFS fs, bool useGTFSView = false) {
            file = f;
            safefilename = sf;

            //--

            bool flip = true;
            fs.Position = 0;
            int magic = GT.ReadInt32(fs, 4, flip);
            int stuff = GT.ReadInt32(fs, 4, flip);
            width = GT.ReadInt32(fs, 4, flip);
            height = GT.ReadInt32(fs, 4, flip);
            int unknown = GT.ReadInt32(fs, 4, flip);
            int format = GT.ReadInt32(fs, 4, flip);
            fs.Position = 0x20; //Only for Gamecube

            Color[][] blocks = DXT.ReadCMPR(fs, flip, width, height);

            bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            int bi = 0;
            for (int y = 0; y < height / 4; y++) {
                for (int x = 0; x < width / 4; x++) {
                    for (int i = 0; i < 16; i++) {
                        int row = i / 4;
                        int col = i - (row * 4);
                        bitmap.SetPixel(x * 4 + col, y * 4 + row, blocks[bi][i]);
                    }
                    bi++;
                }
            }
        }

        public override void SavePNG(string path = "") {
            bitmap.Save(path + FileID + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
