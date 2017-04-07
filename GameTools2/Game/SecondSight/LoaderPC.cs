using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2.Game.SecondSight {
    class LoaderPC : Loader {

        public override void Open(OpenFileDialog openFileDialog, bool export = false, bool useGTFSView = false) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "All PC|*.pak;*.raw|"
                + "PC Raw|*.raw|"
                + "Pak|*.pak|"
                + "All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {

                string[] filenameParts = openFileDialog.SafeFileName.Split('.');
                Array.Reverse(filenameParts);

                if (filenameParts[0].ToUpper() == "PAK") {
                    List<string> files = Pak.Open(openFileDialog.FileName, openFileDialog.SafeFileName);
                    FormGameTools2.ListFiles(files);
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
