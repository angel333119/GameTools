using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FloatTable {
    class DisplayValue {
        private string text;
        private int color;

        public DisplayValue(string t) {
            text = t;
            color = 0;
        }

        public DisplayValue(string t, int c) {
            text = t;
            color = c;
        }

        public override string ToString() {
            return text;
        }

        public Color GetColor() {
            if (color == 0)
                return Color.White;
            else if (color == -1)
                return Color.Red;
            else if (color == 1)
                return Color.Yellow;
            else if (color == 2)
                return Color.Green;

            return Color.Blue;
        }
    }
}
