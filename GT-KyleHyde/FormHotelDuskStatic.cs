using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Be.Windows.Forms;

using GameTools;

namespace GT_KyleHyde
{
    public partial class FormHotelDuskStatic : Form
    {
        //string base_path = @"C:\Users\jas2o\Documents\Visual Studio 2015\Projects\GT-HotelDusk-WPF\Extract";

        private string lastFile;

        public FormHotelDuskStatic()
        {
            InitializeComponent();
        }

        private void OpenHotelDuskImageStatic(string file)
        {
            lastFile = file;

            GTFS fs = new GTFS(file);

            int header = GT.ReadInt32(fs, 4, false);

            if (header == 0) {
                fs.Seek(16, SeekOrigin.Begin);
            } else if (header == 14302482) {
                //123DDA00
                fs.Seek(32, SeekOrigin.Begin);
            } else if (header == 31079698) {
                //123DDA01
                fs = Decompress.ToGTFS(fs, Int32.Parse(txtNeg.Text));
                fs.WriteBytesToFile("GT-KH-Decomp.bin");

                header = GT.ReadInt32(fs, 4, false);

                if (header == 0) {
                    fs.Seek(16, SeekOrigin.Begin);
                } else {
                    Console.WriteLine();
                }
            } else
                throw new Exception();

            bool flip = false;

            int height = GT.ReadUInt16(fs, 2, flip);
            int width = GT.ReadUInt16(fs, 2, flip);
            int paletteLen = GT.ReadUInt16(fs, 2, flip);

            int headerLen = (int)fs.Position + 10;
            paletteLen = paletteLen * 2;

            pictureBox1.Width = width;
            pictureBox1.Height = height;

            long offsetStart = headerLen + paletteLen; //560
            offsetStart = 544; //copy.bin
            long offsetLast;

            fs.Position = offsetStart;

            if (width <= 0 || height <= 0)
                throw new Exception();

            Bitmap bmp = new Bitmap(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int look = fs.ReadByte();
                    offsetLast = fs.Position;

                    fs.Seek(look * 2 + headerLen, SeekOrigin.Begin); //+48 for header
                    byte left = (byte)fs.ReadByte();
                    byte right = (byte)fs.ReadByte();
                    //ushort palette = (ushort)(left | right << 8);
                    ushort palette = (ushort)(right | left << 8);

                    bmp.SetPixel(x, height - y - 1, Palette2Color(palette));

                    //bmp.SetPixel(x, y, Color.FromArgb(look, look, look));

                    fs.Seek(offsetLast, SeekOrigin.Begin);
                }
            }

            //Graphics graphics = Graphics.FromImage(bmp);
            pictureBox1.Image = bmp;
        }

        private Color Palette2Color(ushort palette)
        {
            ushort hex = Tools.SwapBytes(palette);
            int R = (hex & 0x1F) * 8;
            int G = (hex >> 5 & 0x1F) * 8;
            int B = (hex >> 10 & 0x1F) * 8;

            return Color.FromArgb(R, G, B);
        }

        private void hexBox_SelectionStartChanged(object sender, EventArgs e)
        {
            long s = hexBox.SelectionStart;

            txtSelectedOffset.Text = s.ToString();

            byte b1 = hexBox.ByteProvider.ReadByte(s);
            byte[] b2 = new byte[] { hexBox.ByteProvider.ReadByte(s), hexBox.ByteProvider.ReadByte(s + 1) };
            byte[] b4 = new byte[] { hexBox.ByteProvider.ReadByte(s), hexBox.ByteProvider.ReadByte(s + 1), hexBox.ByteProvider.ReadByte(s + 2), hexBox.ByteProvider.ReadByte(s + 3) };

            txtSelectedBytes1.Text = b1.ToString();
            txtSelectedBytes2.Text = ((ulong)Tools.ByteArrayToLong(b2, false)).ToString();
            txtSelectedBytes4.Text = ((ulong)Tools.ByteArrayToLong(b4, false)).ToString();
        }

        private void btnColorCalc_Click(object sender, EventArgs e)
        {
            FormColor formColor = new FormColor();
            formColor.ShowDialog();
            formColor.Close();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OpenHotelDuskImageStatic(openFileDialog1.FileName);
                this.Text = "GT-HotelDusk-Static: " + openFileDialog1.FileName;

                DynamicFileByteProvider dynamicFileByteProvider = new DynamicFileByteProvider(openFileDialog1.FileName, true);
                hexBox.ByteProvider = dynamicFileByteProvider;
            }

            //this.Activate();
        }

        private void btnSetHeight_Click(object sender, EventArgs e)
        {
            txtPixelHeight.Text = txtSelectedBytes2.Text;
            hexBox.SelectionStart += 8;
        }

        private void btnSetWidth_Click(object sender, EventArgs e)
        {
            txtPixelWidth.Text = txtSelectedBytes2.Text;
            hexBox.SelectionStart += 2;
        }

        private void btnTestPixels_Click(object sender, EventArgs e)
        {
            pictureBox1.Width = Int32.Parse(txtPixelWidth.Text);
            pictureBox1.Height = Int32.Parse(txtPixelHeight.Text);

            long offsetStart = Int32.Parse(txtPixelStart.Text);

            long size = (offsetStart + pictureBox1.Width * pictureBox1.Height);
            if (size > hexBox.ByteProvider.Length)
                return;

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int x = 0; x < pictureBox1.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Height; y++)
                {
                    byte input = hexBox.ByteProvider.ReadByte(offsetStart + x + (y * pictureBox1.Width));
                    bmp.SetPixel(x, y, Color.FromArgb(input, input, input));
                }
            }

            Image old = pictureBox1.Image;
            pictureBox1.Image = bmp;
            if (old != null)
                old.Dispose();
        }

        private void btnTestPalette_Click(object sender, EventArgs e)
        {
            int paletteSize = Int32.Parse(txtPaletteSize.Text);

            pictureBox1.Width = (int)Math.Sqrt(paletteSize);
            pictureBox1.Height = (int)Math.Sqrt(paletteSize);

            long offsetStart = Int32.Parse(txtPaletteStart.Text);

            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();

            long size = (offsetStart + ((pictureBox1.Width - 1) * pictureBox1.Width) + pictureBox1.Height - 1);
            if (size > hexBox.ByteProvider.Length)
                return;

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int x = 0; x < pictureBox1.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Height; y++)
                {
                    byte left = hexBox.ByteProvider.ReadByte(offsetStart + (x + (y * pictureBox1.Width)) * 2);
                    byte right = hexBox.ByteProvider.ReadByte(offsetStart + (x + (y * pictureBox1.Width)) * 2 + 1);
                    ushort palette = (ushort)(right | left << 8);
                    bmp.SetPixel(x, y, Palette2Color(palette));
                }
            }

            pictureBox1.Image = bmp;
        }

        private void btnSetPaletteSize_Click(object sender, EventArgs e)
        {
            txtPaletteSize.Text = txtSelectedBytes2.Text;
            hexBox.SelectionStart += 6;
            txtPaletteStart.Text = txtSelectedOffset.Text;
        }

        private void btnSetPixelStart_Click(object sender, EventArgs e)
        {
            txtPixelStart.Text = txtSelectedOffset.Text;
        }

        private void btnSetPaletteStart_Click(object sender, EventArgs e)
        {
            txtPaletteStart.Text = txtSelectedOffset.Text;
        }

        private void btnPixelAfterPalette_Click(object sender, EventArgs e)
        {
            long paletteStart = Int32.Parse(txtPaletteStart.Text);
            long paletteLength = Int32.Parse(txtPaletteSize.Text);
            long pixelStart = paletteStart + paletteLength * 2;
            txtPixelStart.Text = pixelStart.ToString();
        }

        private void btnViewImage_Click(object sender, EventArgs e)
        {
            pictureBox1.Width = Int32.Parse(txtPixelWidth.Text);
            pictureBox1.Height = Int32.Parse(txtPixelHeight.Text);

            long paletteOffset = Int32.Parse(txtPaletteStart.Text);
            long offsetStart = Int32.Parse(txtPixelStart.Text);

            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();

            long size = (offsetStart + pictureBox1.Width * pictureBox1.Height);
            if (size > hexBox.ByteProvider.Length)
                return;

            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            for (int x = 0; x < pictureBox1.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Height; y++)
                {
                    byte look = hexBox.ByteProvider.ReadByte(offsetStart + x + (y * pictureBox1.Width));
                    //--
                    byte left = hexBox.ByteProvider.ReadByte(look * 2 + paletteOffset);
                    byte right = hexBox.ByteProvider.ReadByte(look * 2 + paletteOffset + 1);
                    ushort palette = (ushort)(right | left << 8);
                    //--
                    bmp.SetPixel(x, y, Palette2Color(palette));
                }
            }

            pictureBox1.Image = bmp;

            /*
                    int look = fs.ReadByte();
                    offsetLast = fs.Position;

                    fs.Seek(look * 2 + headerLen, SeekOrigin.Begin); //+48 for header
                    byte left = (byte)fs.ReadByte();
                    byte right = (byte)fs.ReadByte();
                    //ushort palette = (ushort)(left | right << 8);
                    ushort palette = (ushort)(right | left << 8);

                    bmp.SetPixel(x, height - y - 1, Palette2Color(palette));
            */
        }

        private void btnNegSub_Click(object sender, EventArgs e) {
            int neg = Int32.Parse(txtNeg.Text);
            neg--;
            txtNeg.Text = neg.ToString();

            OpenHotelDuskImageStatic(lastFile);
        }

        private void btnNegAdd_Click(object sender, EventArgs e) {
            int neg = Int32.Parse(txtNeg.Text);
            neg++;
            txtNeg.Text = neg.ToString();

            OpenHotelDuskImageStatic(lastFile);
        }
    }
}
