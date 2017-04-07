using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2 {
    public partial class FormImageAnimated : Form {

        private Bitmap[] bmps;
        private int indexLast;

        public FormImageAnimated() {
            InitializeComponent();
        }

        public FormImageAnimated(Bitmap[] bmps) {
            InitializeComponent();
            this.bmps = bmps;
            indexLast = 0;
        }

        private void FormImageAnimated_Load(object sender, EventArgs e) {
            pictureBox1.Image = bmps[indexLast++];
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            this.Size = new Size(pictureBox1.Width + 60, pictureBox1.Height + 60);

            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e) {
            if (indexLast >= bmps.Length)
                indexLast = 0;

            pictureBox1.Image = bmps[indexLast++];
        }
    }
}
