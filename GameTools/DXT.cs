using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools {
    public static class DXT {

        /*
        public enum Format {
            DXT1, DXT, RGBA32, RGBA24
        }
        */

        public static Color[][] ReadCMPR(GTFS fs, bool flip, int width, int height) {
            int total = width * height;
            Color[][] blocks = ReadDXT1(fs, flip, width, height);

            //The blocks require reordering...
            //int rows = (int)Math.Sqrt(blocks.Length);
            int rows = height / 2;
            int cols = width / 2;
            Color[][] reorder = new Color[total / 16][];
            int i = 0;
            for (int y = 0; y < rows; y += 4) {
                for (int x = 0; x < cols; x += 4) {
                    reorder[i++] = blocks[y * rows / 4 + x + 0];
                    reorder[i++] = blocks[y * rows / 4 + x + 1];
                }
                for (int x = 0; x < cols; x += 4) {
                    reorder[i++] = blocks[y * rows / 4 + x + 2];
                    reorder[i++] = blocks[y * rows / 4 + x + 3];
                }
            }

            for(int z = i; z < blocks.Length; z++) {
                reorder[i++] = blocks[0];
            }

            return reorder;
        }

        public static Color[][] ReadDXT1(GTFS fs, bool flip, int width, int height) {
            int total = width * height;
            Color[][] blocks = new Color[total / 16][];

            for (int i = 0; i < total / 16; i++) {

                byte[] raw = GT.ReadBytes(fs, 4, flip);

                ushort c0, c1;
                if (flip) {
                    c0 = (ushort)(raw[2] | raw[3] << 8);
                    c1 = (ushort)(raw[0] | raw[1] << 8);
                } else {
                    c0 = (ushort)(raw[0] | raw[1] << 8);
                    c1 = (ushort)(raw[2] | raw[3] << 8);
                }

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

                if (flip) {
                    blocks[i] = new Color[] {
                            cx[p15], cx[p14], cx[p13], cx[p12],
                            cx[p11], cx[p10], cx[p9], cx[p8],
                            cx[p7], cx[p6], cx[p5], cx[p4],
                            cx[p3], cx[p2], cx[p1], cx[p0]
                        };
                } else {
                    blocks[i] = new Color[] { cx[p0], cx[p1], cx[p2], cx[p3],
                            cx[p4], cx[p5], cx[p6], cx[p7],
                            cx[p8], cx[p9], cx[p10], cx[p11],
                            cx[p12], cx[p13], cx[p14], cx[p15] };
                }
            }

            return blocks;
        }

        private static int MortonSeparateBy1(int x) {
            x &= 0x0000ffff;
            x = (x ^ (x << 8)) & 0x00ff00ff;
            x = (x ^ (x << 4)) & 0x0f0f0f0f;
            x = (x ^ (x << 2)) & 0x33333333;
            x = (x ^ (x << 1)) & 0x55555555;
            return x;
        }

        public static int Morton2D(int y, int x) {
            //http://asgerhoedt.dk/?p=276
            return MortonSeparateBy1(x) | (MortonSeparateBy1(y) << 1);
        }

        public static Color[][] ReadDXT3(GTFS fs, bool flip, int width, int height) {
            int total = width * height;
            Color[][] blocks = new Color[total / 16][];

            byte[] compare = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < total / 16; i++) {
                byte[] rawalpha = GT.ReadBytes(fs, 8, flip); //Each pixel gets 4 bits for alpha

                byte[] raw = GT.ReadBytes(fs, 4, flip);

                int[] alpha = new int[rawalpha.Length * 2];

                for (int a = 0; a < rawalpha.Length; a++) {
                    alpha[a * 2] = (rawalpha[a] & 0x0F) * 17;
                    alpha[a * 2 + 1] = ((rawalpha[a] & 0xF0) >> 4) * 17;
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

                blocks[i] = new Color[] { cx[p0], cx[p1], cx[p2], cx[p3],
                        cx[p4], cx[p5], cx[p6], cx[p7],
                        cx[p8], cx[p9], cx[p10], cx[p11],
                        cx[p12], cx[p13], cx[p14], cx[p15] };

                for (int k = 0; k < blocks[i].Length; k++)
                    blocks[i][k] = Color.FromArgb(alpha[k], blocks[i][k]);
            }

            return blocks;
        }
    }
}
