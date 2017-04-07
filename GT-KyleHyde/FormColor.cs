using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GT_KyleHyde
{
    public partial class FormColor : Form
    {
        uint R5 = 0;
        uint G5 = 0;
        uint B5 = 0;

        uint R8 = 0;
        uint G8 = 0;
        uint B8 = 0;

        public FormColor()
        {
            InitializeComponent();
        }

        private void btnConvertFromFiveBit_Click(object sender, EventArgs e)
        {
            try
            {
                R5 = UInt32.Parse(txtRFive.Text);
                G5 = UInt32.Parse(txtGFive.Text);
                B5 = UInt32.Parse(txtBFive.Text);
            }
            catch (Exception ex)
            {
                // Don't care... The textbox will update anyway
                Console.WriteLine(ex.ToString());
            }

            R5 = LimitNumber(R5, 31);
            G5 = LimitNumber(G5, 31);
            B5 = LimitNumber(B5, 31);

            RGB5to8();

            UpdateAllText();
        }

        private void btnConvertFromEightBit_Click(object sender, EventArgs e)
        {
            try
            {
                R8 = UInt32.Parse(txtREight.Text);
                G8 = UInt32.Parse(txtGEight.Text);
                B8 = UInt32.Parse(txtBEight.Text);
            }
            catch (Exception ex)
            {
                // Don't care... The textbox will update anyway
                Console.WriteLine(ex.ToString());
            }

            R8 = LimitNumber(R8, 255);
            G8 = LimitNumber(G8, 255);
            B8 = LimitNumber(B8, 255);

            RGB8to5();

            UpdateAllText();
        }

        private void btnConvertFromBytes_Click(object sender, EventArgs e)
        {
            ushort hex = Convert.ToUInt16(txtBytes.Text, 16);

            hex = Tools.SwapBytes(hex);
            R5 = (uint)hex & 0x1F;
            G5 = (uint)hex >> 5 & 0x1F;
            B5 = (uint)hex >> 10 & 0x1F;

            RGB5to8();

            UpdateAllText();
        }

        private void btnConvertFromHex_Click(object sender, EventArgs e)
        {
            //6808D8
            uint hex = Convert.ToUInt32(txtHex.Text, 16);

            R8 = (uint)hex >> 16 & 0xFF;
            G8 = (uint)hex >> 8 & 0xFF;
            B8 = (uint)hex & 0xFF;

            RGB8to5();

            UpdateAllText();
        }

        private uint LimitNumber(uint input, uint highest)
        {
            if (input < 0)
                input = 0;
            else if (input > highest)
                input = highest;

            return input;
        }

        private void RGB5to8()
        {
            R8 = R5 * 8;
            G8 = G5 * 8;
            B8 = B5 * 8;
        }

        private void RGB8to5()
        {
            R5 = (uint)(R8 / 8.0);
            G5 = (uint)(G8 / 8.0);
            B5 = (uint)(B8 / 8.0);
        }

        private void RGB8toHex()
        {
            txtHex.Text = R8.ToString("X2") + G8.ToString("X2") + B8.ToString("X2");
        }

        private void RGB5toBytes()
        {
            // What's missing is the alpha at << 11
            ushort rgba5551 = (ushort)((R5 & 0xFF) | (G5 & 0xFF) << 5 | (B5 & 0xFF) << 10);
            rgba5551 = Tools.SwapBytes(rgba5551);

            txtBytes.Text = rgba5551.ToString("X2");
        }

        private void UpdateAllText()
        {
            txtRFive.Text = R5.ToString();
            txtGFive.Text = G5.ToString();
            txtBFive.Text = B5.ToString();

            txtREight.Text = R8.ToString();
            txtGEight.Text = G8.ToString();
            txtBEight.Text = B8.ToString();

            RGB8toHex();
            RGB5toBytes();

            panel1.BackColor = Color.FromArgb((int)R8, (int)G8, (int)B8);
        }
    }
}
