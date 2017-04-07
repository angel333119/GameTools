using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GameTools;
using System.IO;

namespace FloatTable {
    public partial class Form1 : Form {

        int numBlocks = 0;
        List<byte[]> listBytes = new List<byte[]>();
        List<DisplayValue> listDisplay = new List<DisplayValue>();

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            comboWidth.SelectedIndex = 2;
        }

        private void btnLoad_Click(object sender, EventArgs e) {

            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All Files (*.*)|*.*";
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK) {

                numBlocks = 0;
                listBytes = new List<byte[]>();

                string file = openFileDialog1.FileName;
                GTFS fs = new GTFS(file);

                for (int i = 0; i < fs.Length; i += 4) {
                    byte[] bytes = GT.ReadBytes(fs, 4, false);

                    listBytes.Add(bytes);
                    numBlocks++;
                }

                RefreshTable();
            }
        }

        public void RefreshTable() {
            listDisplay = new List<DisplayValue>();
            for (int i = 0; i < numBlocks; i++) {
                byte[] b = new byte[4];

                Array.Copy(listBytes[i], b, 4);
                if (checkFlip.Checked)
                    Array.Reverse(b);

                float f = BitConverter.ToSingle(b, 0);

                if (!float.IsNaN(f) && !f.ToString().Contains('E')) {
                    int c = 0;
                    if (f < -1.0f || f > 1.0f)
                        c = 1;
                    listDisplay.Add(new DisplayValue(f.ToString(), c));
                } else
                    listDisplay.Add(new DisplayValue(GT.ByteArrayToString(b, " "), -1));
            }

            dataGridView1.DataSource = Table();
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            foreach (DataGridViewColumn column in dataGridView1.Columns) {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (dataGridView1.Rows.Count > 1) {
                for (int r = 0; r < dataGridView1.Rows.Count - 1; r++) {
                    for (int c = 0; c < dataGridView1.Rows[r].Cells.Count - 1; c++) {
                        int index = r * (dataGridView1.Rows[r].Cells.Count - 1) + c;
                        dataGridView1.Rows[r].Cells[c+1].Style.BackColor = listDisplay[index].GetColor();
                    }
                }
            }
        }

        public DataTable Table() {
            DataTable returnTable = new DataTable("Float");

            int width = int.Parse(comboWidth.SelectedItem.ToString());

            returnTable.Columns.Add("Offset", typeof(int));
            for (int w = 0; w < width; w++)
                returnTable.Columns.Add(w.ToString(), typeof(string));

            for(int i = 0; i < numBlocks; ) {
                DataRow row = returnTable.NewRow();
                row[0] = i*width;

                for(int c = 0; c < width; c++) {

                    if (i >= numBlocks)
                        break;

                    row[c+1] = listDisplay[i++];
                }

                returnTable.Rows.Add(row);
            }

            return returnTable;
        }

        private void comboWidth_SelectedIndexChanged(object sender, EventArgs e) {
            RefreshTable();
        }

        private void checkFlip_CheckedChanged(object sender, EventArgs e) {
            RefreshTable();
        }

        private void btnUseAsOffset_Click(object sender, EventArgs e) {
            if (dataGridView1.SelectedCells[0].ColumnIndex > 0) {
                int width = int.Parse(comboWidth.SelectedItem.ToString());

                int row = dataGridView1.SelectedCells[0].RowIndex;
                int col = dataGridView1.SelectedCells[0].ColumnIndex - 1;

                int index = row * width + col;

                int offset = BitConverter.ToInt32(listBytes[index], 0);
                offset = offset / width / 4;

                if(dataGridView1.Rows.Count > offset)
                    dataGridView1.CurrentCell = dataGridView1.Rows[offset].Cells[0];
            }
        }
    }
}
