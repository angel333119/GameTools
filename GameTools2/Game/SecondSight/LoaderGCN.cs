using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2.Game.SecondSight {
    class LoaderGCN : Loader {

        public override void Open(OpenFileDialog openFileDialog, bool export = false, bool useGTFSView = false) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "All GameCube|*.pak;*.gcr;*.gct|"
                + "GameCube Resource|*.gcr|"
                + "GameCube Texture|*.gct|"
                + "Pak|*.pak|"
                + "All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {

                string[] filenameParts = openFileDialog.SafeFileName.Split('.');
                Array.Reverse(filenameParts);

                if (filenameParts[0].ToUpper() == "PAK") {
                    List<string> files = Pak.Open(openFileDialog.FileName, openFileDialog.SafeFileName);
                    FormGameTools2.ListFiles(files);
                } else if (filenameParts[0].ToUpper() == "GCR") {
                    //ModelGCN model = new ModelGCN(openFileDialog.FileName, openFileDialog.SafeFileName);
                    //FormGameTools2.UseViewer(model);
                } else if (filenameParts[0].ToUpper() == "GCT") {
                    //TextureGCN texture = new TextureGCN(openFileDialog.FileName, openFileDialog.SafeFileName);
                    //new GameTools3D.FormTextureView(texture).Show();
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
