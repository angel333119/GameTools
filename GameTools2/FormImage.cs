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
    public partial class FormImage : Form {
        private Bitmap bmp;

        public FormImage() {
            InitializeComponent();
        }

        public FormImage(Bitmap bmp) {
            InitializeComponent();
            this.bmp = bmp;
        }

        private void FormImage_Load(object sender, EventArgs e) {
            pictureBox1.Image = bmp;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            this.Size = new Size(pictureBox1.Width + 60, pictureBox1.Height + 60);
        }
    }
}
