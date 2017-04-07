using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameTools {
    public static class Colour {

        public static Color RGB565(int rgb) {
            int r5 = (rgb & 0xF800) >> 11;
            int g6 = (rgb & 0x7E0) >> 5;
            int b5 = (rgb & 0x1F);

            //fast: http://stackoverflow.com/questions/2442576/how-does-one-convert-16-bit-rgb565-to-24-bit-rgb888
            int r8 = (r5 * 527 + 23) >> 6;
            int g8 = (g6 * 259 + 33) >> 6;
            int b8 = (b5 * 527 + 23) >> 6;

            return Color.FromArgb(r8, g8, b8);
        }

        public static Color ARGB1555(int argb) {
            int a = (argb & 0x8000) >> 15;
            int r5 = (argb & 0x7C00) >> 10;
            int g5 = (argb & 0x3E0) >> 5;
            int b5 = (argb & 0x1F);

            a *= 255;
            a = 255;
            int r8 = (r5 * 527 + 23) >> 6;
            int g8 = (g5 * 527 + 23) >> 6;
            int b8 = (b5 * 527 + 23) >> 6;

            return Color.FromArgb(a, r8, g8, b8);
        }

        public static Color ABGR1555(int argb) {
            int a = (argb & 0x8000) >> 15;
            int b5 = (argb & 0x7C00) >> 10;
            int g5 = (argb & 0x3E0) >> 5;
            int r5 = (argb & 0x1F);

            a *= 255;
            a = 255;
            int r8 = (r5 * 527 + 23) >> 6;
            int g8 = (g5 * 527 + 23) >> 6;
            int b8 = (b5 * 527 + 23) >> 6;

            return Color.FromArgb(a, r8, g8, b8);
        }
    }
}
