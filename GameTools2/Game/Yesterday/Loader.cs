using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2.Game.Yesterday {
    class Loader : Game.Loader {

        public override void Open(OpenFileDialog openFileDialog, bool export = false, bool useGTFSView = false) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "All Yesterday|*.obb;*.*|"
                + "Android Archive (*.obb)|*.obb|"
                + "All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {

                string[] filenameParts = openFileDialog.SafeFileName.Split('.');
                Array.Reverse(filenameParts);

                if (filenameParts[0].ToUpper() == "OBB") {
                    List<string> files = OBB.Open(openFileDialog.FileName, openFileDialog.SafeFileName);
                    FormGameTools2.ListFiles(files);
                } else if (openFileDialog.SafeFileName[0] == 'V' || openFileDialog.SafeFileName == "RESOURCE.B31" || openFileDialog.SafeFileName == "RESOURCE.CRD") {
                    Video.Open(openFileDialog.FileName, openFileDialog.SafeFileName);
                } else if (filenameParts[1].Contains("DATAA") || filenameParts[0].Contains("S0")) {
                    Audio.Open(openFileDialog.FileName, openFileDialog.SafeFileName);
                } else if (filenameParts[1].Contains("RESOURCE") || filenameParts[0].Contains("SP")) {
                    ResourceImage.Open(openFileDialog.FileName, openFileDialog.SafeFileName);
                } else if (filenameParts[1].Contains("RESOURCE") &&
                    filenameParts[0] != "003" &&
                    filenameParts[0] != "TAB" &&
                    filenameParts[0] != "FNT" &&
                    filenameParts[0] != "DIS" &&
                    (filenameParts[0][0] != 'S' && filenameParts[0][0] != 'P') //Not sure what these are at all...
                ) {
                    //Resource
                    ResourceImage.Open(openFileDialog.FileName, openFileDialog.SafeFileName);
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
