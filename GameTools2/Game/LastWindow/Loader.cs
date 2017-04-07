using GameTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2.Game.LastWindow {
    class Loader : Game.Loader {

        public Loader() {
            PackExtension = ".pack";
        }

        public override void Open(OpenFileDialog openFileDialog, bool export = false, bool useGTFSView = false) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "All Last Window|*.pack;*.bra;*.bin;*.bpg;*.ebp|"
                + "All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {

                string[] filenameParts = openFileDialog.SafeFileName.Split('.');
                Array.Reverse(filenameParts);

                if (filenameParts[0].ToUpper() == "BPG") {
                    BPG bpg = new BPG(Decompress(openFileDialog.FileName));
                    new FormImage(bpg.bitmap).Show();
                } else if (filenameParts[0].ToUpper() == "EBP") {
                    GTFS fs = Decompress(openFileDialog.FileName);
                    fs.WriteBytesToFile(openFileDialog.SafeFileName + ".gtbin");

                    EBP ebp = new EBP(fs);

                    if(ebp.bitmap != null)
                        new FormImage(ebp.bitmap).Show();

                } else if (filenameParts[0].ToUpper() == "BIN") {
                    GTFS fs = Decompress(openFileDialog.FileName);
                    fs.WriteBytesToFile("GT-KH-ZL.out");
                } else if (filenameParts[0].ToUpper() == "BRA") {
                    LWBRA.Open(openFileDialog);
                } else if (filenameParts[0].ToUpper() == "PACK") {
                    LWPack.OpenPack(openFileDialog.FileName, openFileDialog.SafeFileName);
                } else {
                    MessageBox.Show("Unexpected file extension: " + filenameParts[0]);
                }
            }
        }

        public override void OpenAllPaks(List<string> dirfiles) {
            foreach(string file in dirfiles) {
                LWPack.OpenPack(file, Path.GetFileName(file));
            }
        }

        private static GTFS Decompress(string file) {
            //First 4 bytes is uncompressed size
            //Next 2 bytes should be 78 9C

            GTFS fs = null;

            FileStream fsComp = new FileStream(file, FileMode.Open);
            int uncomLen = GT.ReadInt32(fsComp, 4, false);
            fsComp.Position += 2;
            using (DeflateStream decompressionStream = new DeflateStream(fsComp, CompressionMode.Decompress)) {
                byte[] raw = new byte[uncomLen];
                decompressionStream.Read(raw, 0, uncomLen);
                fs = new GTFS(raw);
            }
            fsComp.Close();

            return fs;
        }

        public override void MassConvert(List<string> dirfiles) {
            throw new NotImplementedException();
        }
    }
}
