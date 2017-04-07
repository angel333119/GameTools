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

namespace IntList {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e) {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All Files (*.*)|*.*";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK) {
                listBox1.Items.Clear();

                string file = openFileDialog1.FileName;
                GTFS fs = new GTFS(file);
                fs.Position = Int32.Parse(txtSkipBytes.Text);

                int num = Int32.Parse(txtFor.Text);

                for (int i = 0; i < num; i++) {
                    int read = GT.ReadInt32(fs, 4, false);
                    listBox1.Items.Add(read);
                }
            }
        }

        private void btnSelectedSum_Click(object sender, EventArgs e) {
            int total = 0;

            foreach(var item in listBox1.SelectedItems) {
                int num = Int32.Parse(item.ToString());
                total += num;
            }

            txtSum.Text = total.ToString();
        }
    }
}
