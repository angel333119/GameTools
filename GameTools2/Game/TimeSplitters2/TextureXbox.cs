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
    class TextureXbox : Texture {

        public TextureXbox(string f, string sf, bool useGTFSView = false) : this(f, sf, new GTFS(f), useGTFSView) { }

        public TextureXbox(string f, string sf, GTFS fs, bool useGTFSView = false) {
            file = f;
            safefilename = sf;

            bool flip = false;
            fs.Position = 0;
            int magic = GT.ReadInt32(fs, 4, flip); //Width?
            int stuff = GT.ReadInt32(fs, 4, flip); //Height?
            width = GT.ReadInt32(fs, 4, flip);
            height = GT.ReadInt32(fs, 4, flip);
            int unknown = GT.ReadInt32(fs, 4, flip);
            //fs.Position = 0x14;
            int format = GT.ReadInt32(fs, 4, flip);
            fs.Position = 0x80; //Only for Xbox

            //Console.WriteLine("Texture: " + sf + " Unknown: " + unknown);

            Color[][] blocks = null;

            if (format == 0)
                blocks = DXT.ReadDXT1(fs, flip, width, height);
            else if (format == 1)
                blocks = DXT.ReadDXT3(fs, flip, width, height);
            else if (format == 2) {
                //Morton Order
                blocks = new Color[height][];
                for (int y = 0; y < height; y++) {
                    blocks[y] = new Color[width];
                    for (int x = 0; x < width; x++) {
                        int idx = DXT.Morton2D(y, x);
                        if ((idx * 4 + 4) > fs.Length - 0x80)
                            idx = 0;
                        fs.Position = 128 + idx * 4;
                        blocks[y][x] = Color.FromArgb(GT.ReadInt32(fs, 4, flip));
                    }
                }
            } else if (format == 3)
                Console.WriteLine();
            else
                throw new Exception();

            bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            if (blocks.Length == height) {
                //RGB Style
                for (int y = 0; y < height; y++) {
                    for (int x = 0; x < width; x++) {
                        bitmap.SetPixel(x, y, blocks[y][x]);
                    }
                }
            } else {
                //DXT Style
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
        }

        public override void SavePNG(string path = "") {
            if(path != "" && !Directory.Exists(path))
                Directory.CreateDirectory(path);
            bitmap.Save(path + FileID + ".png", System.Drawing.Imaging.ImageFormat.Png);
        }

    }
}
