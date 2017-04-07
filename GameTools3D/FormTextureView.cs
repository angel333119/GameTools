using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using GameTools3D.Formats;

namespace GameTools3D {
    public partial class FormTextureView : Form {
        private string file;
        private string safefilename;
        private string format;

        Texture texture;

        public FormTextureView(Texture texture) {
            InitializeComponent();
            LoadTexture(texture);
        }

        public void LoadTexture(Texture texture) {
            pictureBox1.Width = texture.Width;
            pictureBox1.Height = texture.Height;
            pictureBox1.BackColor = Color.Black;

            this.Size = new Size(pictureBox1.Width + 40, pictureBox1.Height + 60);

            Image old = pictureBox1.Image;
            pictureBox1.Image = texture.Bitmap;
            if (old != null)
                old.Dispose();
        }
    }
}
