using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GameTools;
using System.Collections;

namespace TestDXT1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void btnLoadFile_Click(object sender, EventArgs e) {
            /*
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All Files (*.*)|*.*";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK) {
                img = new Img(openFileDialog1.FileName);

                pictureBox1.Image = img.GeneratePNG();
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }*/

            bool flip = false;
            GTFS fs = new GTFS("textures_misc_xbcontroller.xbt");

            int magic = GT.ReadInt32(fs, 4, flip);
            int stuff = GT.ReadInt32(fs, 4, flip);
            int width = GT.ReadInt32(fs, 4, flip);
            int height = GT.ReadInt32(fs, 4, flip);
            int unknown = GT.ReadInt32(fs, 4, flip);
            int format = GT.ReadInt32(fs, 4, flip);
            fs.Position = 0x80;

            int total = width * height;
            List<Color[]> blocks = new List<Color[]>();

            if (format == 1) {
                for (int i = 0; i < total; i++) {
                    //DXT3
                    byte[] rawalpha = GT.ReadBytes(fs, 8, flip); //Each pixel gets 4 bits for alpha

                    byte[] raw = GT.ReadBytes(fs, 4, flip);

                    int[] alpha = new int[rawalpha.Length*2];

                    for(int a = 0; a < rawalpha.Length; a++) {
                        alpha[a*2] = (rawalpha[a] & 0xF0) >> 4;
                        alpha[a*2+1] = (rawalpha[a] & 0x0F);
                    }

                    //http://www.igeekstudio.com/blog/dtx1-decompression-explained
                    ushort c0 = (ushort)(raw[0] | raw[1] << 8);
                    ushort c1 = (ushort)(raw[2] | raw[3] << 8);

                    // Color 0 RGB
                    byte r0 = (byte)((c0 >> 11) << 3 | 0x04);
                    byte g0 = (byte)((c0 & 0x07E0) >> 3 | 0x02);
                    byte b0 = (byte)((c0 & 0x001F) << 3 | 0x04);
                    // Color 1 RBG
                    byte r1 = (byte)((c1 >> 11) << 3);
                    byte g1 = (byte)((c1 & 0x07E0) >> 3);
                    byte b1 = (byte)((c1 & 0x001F) << 3);
                    //--

                    Color[] cx = new Color[] {
                        Color.FromArgb(Byte.MaxValue, r0, g0, b0),
                        Color.FromArgb(Byte.MaxValue, r1, g1, b1),
                        (c0 > c1) ? Color.FromArgb(byte.MaxValue, (2 * r0 + r1) / 3, (2 * g0 + g1) / 3, (2 * b0 + b1) / 3) : Color.FromArgb(byte.MaxValue, (2 * r1 + r0) / 3, (2 * g1 + g0) / 3, (2 * b1 + b0) / 3),
                        (c0 <= c1) ? Color.FromArgb(byte.MaxValue, (2 * r0 + r1) / 3, (2 * g0 + g1) / 3, (2 * b0 + b1) / 3) : Color.FromArgb(byte.MaxValue, (2 * r1 + r0) / 3, (2 * g1 + g0) / 3, (2 * b1 + b0) / 3)
                    };

                    uint pixels = GT.ReadUInt32(fs, 4, flip);

                    int p15 = (int)((pixels & 0xC0000000) >> 30);
                    int p14 = (int)((pixels & 0x30000000) >> 28);
                    int p13 = (int)((pixels & 0xC000000) >> 26);
                    int p12 = (int)((pixels & 0x3000000) >> 24);
                    int p11 = (int)((pixels & 0xC00000) >> 22);
                    int p10 = (int)((pixels & 0x300000) >> 20);
                    int p9 = (int)((pixels & 0xC0000) >> 18);
                    int p8 = (int)((pixels & 0x30000) >> 16);
                    int p7 = (int)((pixels & 0xC000) >> 14);
                    int p6 = (int)((pixels & 0x3000) >> 12);
                    int p5 = (int)((pixels & 0xC00) >> 10);
                    int p4 = (int)((pixels & 0x300) >> 8);
                    int p3 = (int)((pixels & 0xC0) >> 6);
                    int p2 = (int)((pixels & 0x30) >> 4);
                    int p1 = (int)((pixels & 0xC) >> 2);
                    int p0 = (int)((pixels & 0x3));

                    blocks.Add(new Color[] { cx[p0], cx[p1], cx[p2], cx[p3],
                        cx[p4], cx[p5], cx[p6], cx[p7],
                        cx[p8], cx[p9], cx[p10], cx[p11],
                        cx[p12], cx[p13], cx[p14], cx[p15] });

                    Console.WriteLine();
                }
            } else {
                throw new Exception();
            }

            Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            //bitmap.SetPixel(x * 4 + x1, y * 4 + y1, Color.FromArgb(blocks[i][y * 4 + x]));

            //bitmap.SetPixel(x * 4 + x1, y * 4 + y1, Color.FromArgb(blocks[i * sub + k][y * 4 + x]));

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

            bitmap.Save("test.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
