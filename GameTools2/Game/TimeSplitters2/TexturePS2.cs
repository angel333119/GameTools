using GameTools;
using GameTools3D.Formats;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools2.Game.TimeSplitters2 {
    class TexturePS2 : Texture {

        public TexturePS2(string f, string sf, bool useGTFSView = false) : this(f, sf, new GTFS(f), useGTFSView) { }

        public TexturePS2(string f, string sf, GTFS fs, bool useGTFSView = false) {
            file = f;
            safefilename = sf;

            //--

            bool flip = false;

            fs.Position = 0;
            int magic = GT.ReadInt32(fs, 4, flip);
            int unknown = GT.ReadInt32(fs, 4, flip);
            width = GT.ReadInt32(fs, 4, flip);
            height = GT.ReadInt32(fs, 4, flip);
            long apal = fs.Position;
            long atex = 0x410;
            int HALFX = width / 2;
            int HALFY = height / 2;
            int TLONG = width * height + 1372;
            int XY = width * height;

            // Color Palette = RGBA8888
            fs.Position = apal;
            Color[] palette = new Color[256];
            for (int p = 0; p < 256; p++) {
                byte r = GT.ReadByte(fs);
                byte g = GT.ReadByte(fs);
                byte b = GT.ReadByte(fs);
                byte a = GT.ReadByte(fs); // Max is 127, is it alpha?
                //a = 255;

                //Console.WriteLine(r + " " + g + " " + b + " " + a);
                palette[p] = Color.FromArgb(a*2, r, g, b);
            }

            fs.Position = atex;
            bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    byte pix = GT.ReadByte(fs);
                    bitmap.SetPixel(x, y, palette[pix]);
                }
            }
        }

        public override void SavePNG(string path = "") {
            bitmap.Save(path + FileID + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
