using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2.Game.TimeSplitters2 {
    class LoaderXbox : Loader {

        public LoaderXbox() {
            PackExtension = ".pak";
            MassExtension = ".xbr";
        }

        public override void Open(OpenFileDialog openFileDialog, bool export = false, bool useGTFSView = false) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "All Xbox|*.pak;*.xbr;*.xbt|"
                + "Xbox Resource|*.xbr|"
                + "Xbox Texture|*.xbt|"
                + "Pak|*.pak|"
                + "All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {

                string[] filenameParts = openFileDialog.SafeFileName.Split('.');
                Array.Reverse(filenameParts);

                if (filenameParts[0].ToUpper() == "PAK") {
                    List<string> files = Pak.Open(openFileDialog.FileName, openFileDialog.SafeFileName);
                    FormGameTools2.ListFiles(files);
                } else if (filenameParts[0].ToUpper() == "XBR") {
                    ModelXbox model = new ModelXbox(openFileDialog.FileName, openFileDialog.SafeFileName, useGTFSView);
                    if(export)
                        new GameTools3D.Formats.ColladaExporter(model);
                    FormGameTools2.UseViewer(model);
                } else if (filenameParts[0].ToUpper() == "XBT") {
                    TextureXbox texture = new TextureXbox(openFileDialog.FileName, openFileDialog.SafeFileName, useGTFSView);
                    new GameTools3D.FormTextureView(texture).Show();
                } else {
                    MessageBox.Show("Unexpected file extension: " + filenameParts[0]);
                }
            }
        }

        public override void OpenAllPaks(List<string> dirfiles) {
            Pak.OpenAllPaks(dirfiles);
        }

        public override void MassConvert(List<string> dirfiles) {
            foreach (string file in dirfiles) {
                ModelXbox model = new ModelXbox(file, Path.GetFileName(file), false);
                new GameTools3D.Formats.ColladaExporter(model);
            }
        }

        public void MassConvertTextures(List<string> dirfiles) {
            foreach (string file in dirfiles) {
                TextureXbox texture = new TextureXbox(file, Path.GetFileName(file), false);
                texture.SavePNG("GT-TS2-Export/Textures/");
            }
        }
    }
}
