using GameTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools2.Game.LastWindow {
    class BPG {

        public int Width, Height;
        public Bitmap bitmap;

        public BPG(GTFS fs) {

            bool flip = false;
            byte[] magic = GT.ReadBytes(fs, 4, flip); //BPG1
            int paletteNum = GT.ReadInt16(fs, 2, flip);
            int unknown2 = GT.ReadInt16(fs, 2, flip); //Should be 8 ?
            Width = GT.ReadInt16(fs, 2, flip);
            Height = GT.ReadInt16(fs, 2, flip);
            int tileWidth = GT.ReadInt16(fs, 2, flip);
            int tileHeight = GT.ReadInt16(fs, 2, flip);

            int numTilesX = Width / tileWidth;
            int numTilesY = Height / tileHeight;

            Color[] palette = new Color[paletteNum];
            for(int i = 0; i < paletteNum; i++) {
                byte left = GT.ReadByte(fs);
                byte right = GT.ReadByte(fs);
                palette[i] = HotelDusk.FRM.Palette2Color(left, right);
            }

            bitmap = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            for (int y = 0; y < numTilesY; y++) {
                for (int x = 0; x < numTilesX; x++) {
                    for (int ty = 0; ty < tileHeight; ty++) {
                        for (int tx = 0; tx < tileWidth; tx++) {
                            byte lookup = GT.ReadByte(fs);
                            bitmap.SetPixel(x * tileWidth + tx, y * tileHeight + ty, palette[lookup]);
                        }
                    }
                }
            }

        }

    }
}
