using GameTools;
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
    public partial class FormGT : Form
    {
        public FormGT()
        {
            InitializeComponent();
        }

        private void FormGT_DragDrop(object sender, DragEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void FormGT_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void btnLoadAuto_Click(object sender, EventArgs e) {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All Files (*.*)|*.*";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK) {

                string[] filenameParts = openFileDialog1.SafeFileName.Split('.');
                Array.Reverse(filenameParts);

                if (filenameParts[0].ToUpper() == "PACK") {
                    LastWindowPack.OpenPack(openFileDialog1);
                } else if (filenameParts[0].ToUpper() == "BRA") {
                    LastWindowBRA.Open(openFileDialog1);
                } else if (filenameParts[0].ToUpper() == "ANM") {
                    HotelDuskANM.OpenANM(openFileDialog1);
                } else if (filenameParts[0].ToUpper() == "WPF") {
                    HotelDuskWPF.OpenWPF(openFileDialog1);
                } else if (filenameParts[0].ToUpper() == "WPFBIN" || filenameParts[0].ToUpper() == "BIN") {
                    MessageBox.Show("Use the old form interface.");
                } else {
                    MessageBox.Show("Unexpected file extension: " + filenameParts[0]);
                }

            }
        }

        private void btnOpenOldInterface_Click(object sender, EventArgs e) {
            new FormHotelDuskStatic().Show();
        }
    }
}
