using GameTools;
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

namespace TestPCX {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void btnLoadFile_Click(object sender, EventArgs e) {
            GTFS fs = new GTFS(@"D:\ExtractedGames\NDS_UNPACK_HDR215\data\data\system\note\notememoedit_wpf\eraser.bin");

            byte[] header = GT.ReadBytes(fs, 4, false);
            int uncompressed = GT.ReadInt32(fs, 4, false);
            int compressed = GT.ReadInt32(fs, 4, false);
            int zero = GT.ReadInt32(fs, 4, false);
            byte[] data = GT.ReadBytes(fs, compressed, false);

            byte[] undata = GBA.LZ77.Decompress(data, uncompressed);

            FileStream nf = new FileStream("TestPCXOut.bin", FileMode.Create);
            nf.Write(undata, 0, uncompressed);
            nf.Close();
        }
    }
}
