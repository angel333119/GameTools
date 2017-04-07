using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2.Game.HotelDusk {
    class Loader : Game.Loader {

        public override void Open(OpenFileDialog openFileDialog, bool export = false, bool useGTFSView = false) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "All Hotel Dusk|*.anm;*.frm;*.wpf;*.bin;*.txt;*.dtx|"
                + "All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {

                string[] filenameParts = openFileDialog.SafeFileName.Split('.');
                Array.Reverse(filenameParts);

                if (filenameParts[0].ToUpper() == "FRM") {
                    GTFS fs = new GTFS(openFileDialog.FileName);
                    FRM frm = new FRM(fs);

                    new FormImage(frm.bitmap).Show();
                } else if (filenameParts[0].ToUpper() == "ANM") {
                    GTFS fs = new GTFS(openFileDialog.FileName);
                    ANM anm = new ANM(fs);

                    new FormImageAnimated(anm.BitmapsBlended()).Show();

                } else if (filenameParts[0].ToUpper() == "WPF") {
                    WPF.Open(openFileDialog);
                } else if (filenameParts[0].ToUpper() == "WPFBIN" || filenameParts[0].ToUpper() == "BIN") {
                    GTFS fs = new GTFS(openFileDialog.FileName);

                    byte[] magic = GT.ReadBytes(fs, 4, false);
                    fs.Position = 0;
                    if(magic.SequenceEqual(LRIM.Magic)) {
                        LRIM lrim = new LRIM(fs);
                    } else {
                        WPFBIN wpfbin = new WPFBIN(fs);
                        new FormImage(wpfbin.bitmap).Show();
                    }
                } else if (filenameParts[0].ToUpper() == "DTX") {
                    GTFS fs = new GTFS(openFileDialog.FileName);
                    WPFBIN wpfbin = new WPFBIN(fs);
                    new FormImage(wpfbin.bitmap).Show();
                    //GTFS compressed = new GTFS(openFileDialog.FileName);
                    //GTFS decompressed = Decompress.ToGTFS(compressed);
                    //decompressed.WriteBytesToFile("GT-KH-DecompDTX.gtbin");
                } else if (filenameParts[0].ToUpper() == "TXT") {
                    GTFS compressed = new GTFS(openFileDialog.FileName);
                    GTFS decompressed = Decompress.ToGTFS(compressed);
                    //decompressed.WriteBytesToFile("GT-KH-DecompTXT.gtbin");

                    byte[] test = GT.ReadBytes(decompressed, 4, false);
                    decompressed.Position = 0;

                    if (test[0] == 0 || test[1] == 0 || test[2] == 0 || test[3] == 3) {
                        int total = GT.ReadInt32(decompressed, 4, false);
                        for(int i = 0; i < total; i++) {
                            int offset = GT.ReadInt32(decompressed, 4, false);
                        }

                        long baseOffset = decompressed.Position;

                        string input = GT.ReadASCII(decompressed, (int)(decompressed.Length - 1 - baseOffset), false);
                        input = input.Replace((char)0x00, (char)0x20);

                        new FormText(input).Show();

                    } else {
                        string input = GT.ReadASCII(decompressed, decompressed.Length - 1, false);
                        input = input.Replace("\n", "\r\n");

                        new FormText(input).Show();
                    }
                } else {
                    MessageBox.Show("Unexpected file extension: " + filenameParts[0]);
                }
            }
        }

        public override void OpenAllPaks(List<string> dirfiles) {
            throw new NotImplementedException();
        }

        public override void MassConvert(List<string> dirfiles) {
            throw new NotImplementedException();
        }
    }
}
