using GameTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2.Game.RunawayNDSSPR {
    class Loader : Game.Loader {

        public override void Open(OpenFileDialog openFileDialog, bool export = false, bool useGTFSView = false) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {
                string filePal = openFileDialog.FileName.Replace(openFileDialog.SafeFileName, "") + "..\\sprite.pal";
                GTFS fsPal = new GTFS(filePal);

                List<Color> listPal = new List<Color>();
                while (fsPal.Position < fsPal.Length - 1) {
                    int input = GT.ReadInt16(fsPal, 2, false);
                    listPal.Add(Colour.ABGR1555(input));
                }


                //-------

                GTFS fs = new GTFS(openFileDialog.FileName);

                List<RunawayPackedImage> listImage = new List<RunawayPackedImage>();
                while (fs.Position < fs.Length - 1) {
                    listImage.Add(new RunawayPackedImage(fs));
                }

                int maxFirst = listImage.Max(x => x.derivedfirstadd);
                int maxSecond = listImage.Max(x => x.second);

                Bitmap bmp = new Bitmap(maxFirst, maxSecond + 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                foreach (RunawayPackedImage rpi in listImage) {
                    for (int i = 0; i < rpi.numValues; i++) {
                        bmp.SetPixel(rpi.first + i, rpi.second, listPal[rpi.values[i]]);
                    }
                }

                new FormImage(bmp).Show();
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
