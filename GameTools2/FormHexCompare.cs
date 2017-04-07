using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2 {
    public partial class FormHexCompare : Form {
        byte[] left, right;
        MemoryStream msLeft, msRight;

        public FormHexCompare() {
            InitializeComponent();

            left = new byte[0];
            right = new byte[0];
        }

        private void hexBox1_CurrentLineChanged(object sender, EventArgs e) {
            hexBox2.SelectionStart = hexBox1.SelectionStart;
            UpdateStatusBar();
        }

        private void hexBox1_CurrentPositionInLineChanged(object sender, EventArgs e) {
            hexBox2.SelectionStart = hexBox1.SelectionStart;
            UpdateStatusBar();
        }

        private void hexBox1_SelectionLengthChanged(object sender, EventArgs e) {
            hexBox2.SelectionLength = hexBox1.SelectionLength;
            UpdateStatusBar();
        }

        private void UpdateStatusBar() {
            if (hexBox1.SelectionStart + 1 < hexBox1.ByteProvider.Length) {

                int left = hexBox1.ByteProvider.ReadByte(hexBox1.SelectionStart);
                int right = hexBox1.ByteProvider.ReadByte(hexBox1.SelectionStart + 1);

                right = right << 8;
                int both = left + right;
                short sboth = (short)both;

                toolStripStatusLabel1.Text = "@" + hexBox1.SelectionStart.ToString() + " || " + hexBox1.SelectionLength.ToString() + " || " + both.ToString() + " or " + sboth.ToString();
            }
        }

        private void hexBox2_CurrentLineChanged(object sender, EventArgs e) {
            hexBox1.SelectionStart = hexBox2.SelectionStart;
            UpdateStatusBar();
        }

        private void hexBox2_CurrentPositionInLineChanged(object sender, EventArgs e) {
            hexBox1.SelectionStart = hexBox2.SelectionStart;
            UpdateStatusBar();
        }

        private void hexBox2_SelectionLengthChanged(object sender, EventArgs e) {
            hexBox1.SelectionLength = hexBox2.SelectionLength;
            UpdateStatusBar();
        }

        public FormHexCompare(byte[] left, byte[] right) {
            InitializeComponent();

            this.left = left;
            this.right = right;
        }

        private void FormHexCompare_Load(object sender, EventArgs e) {
            msLeft = new MemoryStream(left);
            msRight = new MemoryStream(right);

            DynamicFileByteProvider dynamicLeft = new DynamicFileByteProvider(msLeft);
            DynamicFileByteProvider dynamicRight = new DynamicFileByteProvider(msRight);

            hexBox1.ByteProvider = dynamicLeft;
            hexBox2.ByteProvider = dynamicRight;
        }
    }
}
