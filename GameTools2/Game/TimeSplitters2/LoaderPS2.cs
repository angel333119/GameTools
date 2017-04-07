using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2.Game.TimeSplitters2 {
    class LoaderPS2 : Loader {

        public LoaderPS2() {
            PackExtension = ".pak";
        }

        public override void Open(OpenFileDialog openFileDialog, bool export = false, bool useGTFSView = false) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "All PS2|*.pak;*.raw|"
                + "PS2 Raw|*.raw|"
                + "Pak|*.pak|"
                + "All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {

                string[] filenameParts = openFileDialog.SafeFileName.Split('.');
                Array.Reverse(filenameParts);

                if (filenameParts[0].ToUpper() == "PAK") {
                    List<string> files = Pak.Open(openFileDialog.FileName, openFileDialog.SafeFileName);
                    FormGameTools2.ListFiles(files);
                } else if (filenameParts[0].ToUpper() == "RAW") {
                    GTFS fs = new GTFS(openFileDialog.FileName);
                    byte[] bHeader = GT.ReadBytes(fs, 4, false);

                    if ((bHeader[0] == 0x10 && bHeader[1] == 0x00 && bHeader[2] == 0x00 && bHeader[3] == 0x00) || //PS2 object
                        (bHeader[0] == 0x20 && bHeader[1] == 0x00 && bHeader[2] == 0x00 && bHeader[3] == 0x00)) { //PS2 level
                        ModelPS2 model = new ModelPS2(openFileDialog.FileName, openFileDialog.SafeFileName, useGTFSView);
                        FormGameTools2.UseViewer(model);
                    } else if ((bHeader[0] == 0x01 && bHeader[1] == 0x00 && bHeader[2] == 0x00 && bHeader[3] == 0x00) ||
                        (bHeader[0] == 0x03 && bHeader[1] == 0x00 && bHeader[2] == 0x00 && bHeader[3] == 0x00)) {
                        TexturePS2 texture = new TexturePS2(openFileDialog.FileName, openFileDialog.SafeFileName, useGTFSView);
                        new GameTools3D.FormTextureView(texture).Show();
                    } else {
                        MessageBox.Show("Unknown header (hex): " + GT.ByteArrayToString(bHeader, " "));
                    }
                } else {
                    MessageBox.Show("Unexpected file extension: " + filenameParts[0]);
                }
            }
        }

        public override void OpenAllPaks(List<string> dirfiles) {
            Pak.OpenAllPaks(dirfiles);
        }

        public override void MassConvert(List<string> dirfiles) {
            throw new NotImplementedException();
        }
    }
}
