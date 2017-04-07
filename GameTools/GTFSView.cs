using Be.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GameTools {
    public partial class GTFSView : Form {

        // Future: I'd like to be able to see blocks that haven't been tracked

        GTFS fs;
        List<Track> list;
        List<Track> listGaps;

        public GTFSView(GTFS fs) {
            this.fs = fs;
            InitializeComponent();
        }

        private void GTFSView_Load(object sender, EventArgs e) {
            DynamicFileByteProvider dynamicFileByteProvider = new DynamicFileByteProvider(fs.FilePath);
            hexBox1.ByteProvider = dynamicFileByteProvider;

            //list = fs.GetTracking();
            list = fs.GetTrackingSections();
            list = list.OrderBy(o => o.Offset).ToList(); //By Offset
            listGaps = fs.GetTrackingGaps();

            listboxTracking.Items.AddRange(list.ToArray());
            listboxTrackingGaps.Items.AddRange(listGaps.ToArray());
        }

        private void listboxTracking_SelectedIndexChanged(object sender, EventArgs e) {
            if (listboxTracking.SelectedIndex > -1) {
                Track t = list[listboxTracking.SelectedIndex];
                hexBox1.Select(t.Offset, t.Length);
                hexBox1.ScrollByteIntoView();
            }
        }

        private void listboxTrackingGaps_SelectedIndexChanged(object sender, EventArgs e) {
            if (listboxTrackingGaps.SelectedIndex > -1) {
                Track t = listGaps[listboxTrackingGaps.SelectedIndex];
                hexBox1.Select(t.Offset, t.Length);
                hexBox1.ScrollByteIntoView();
            }
        }

        private void btnByID_Click(object sender, EventArgs e) {
            list = list.OrderBy(o => o.ID).ToList();
            listboxTracking.Items.Clear();
            listboxTracking.Items.AddRange(list.ToArray());
        }

        private void btnByOffset_Click(object sender, EventArgs e) {
            list = list.OrderBy(o => o.Offset).ToList();
            listboxTracking.Items.Clear();
            listboxTracking.Items.AddRange(list.ToArray());
        }

        private void hexBox1_SelectionStartChanged(object sender, EventArgs e) {
            CalcStuff();
        }

        private void CalcStuff() {
            long s = hexBox1.SelectionStart;
            if (s < hexBox1.ByteProvider.Length - 3) {

                txtSelectedOffset.Text = s.ToString();

                byte[] b4 = new byte[] { hexBox1.ByteProvider.ReadByte(s), hexBox1.ByteProvider.ReadByte(s + 1), hexBox1.ByteProvider.ReadByte(s + 2), hexBox1.ByteProvider.ReadByte(s + 3) };

                if (checkEndian.Checked)
                    Array.Reverse(b4);

                txtSelectedUInt16.Text = BitConverter.ToUInt16(b4, 0).ToString();
                txtSelectedUInt32.Text = BitConverter.ToUInt32(b4, 0).ToString();
                txtSelectedFloat.Text = BitConverter.ToSingle(b4, 0).ToString();
            }
        }

        private void txtSelectedOffset_TextChanged(object sender, EventArgs e) {
            //
        }

        private void btnGotoUInt32_Click(object sender, EventArgs e) {
            txtSelectedOffset.Text = txtSelectedUInt32.Text;
        }

        private void btnGotoUInt16_Click(object sender, EventArgs e) {
            txtSelectedOffset.Text = txtSelectedUInt16.Text;
        }

        private void checkEndian_CheckedChanged(object sender, EventArgs e) {
            CalcStuff();
        }

        private void txtSelectedOffset_Leave(object sender, EventArgs e) {
            if (txtSelectedOffset.Text.Length > 0) {
                long s = -1;
                if (long.TryParse(txtSelectedOffset.Text, out s) && s > 0 && s < hexBox1.ByteProvider.Length) {
                    hexBox1.SelectionStart = s;
                    hexBox1.Select(s, 1);
                    hexBox1.ScrollByteIntoView();
                }
            }
        }
    }
}
