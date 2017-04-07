using GameTools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2.Game.YesterdaySPR {
    class Loader : Game.Loader {

        public override void Open(OpenFileDialog openFileDialog, bool export = false, bool useGTFSView = false) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {
                GTFS fs = new GTFS(openFileDialog.FileName);

                List<YesterdayPackedImage> listImage = new List<YesterdayPackedImage>();
                while (fs.Position < fs.Length - 1) {
                    listImage.Add(new YesterdayPackedImage(fs));
                }

                int maxFirst = listImage.Max(x => x.derivedfirstadd);
                int maxSecond = listImage.Max(x => x.second);

                List<Bitmap> listBmps = new List<Bitmap>();

                int lastY = -1;

                //StreamWriter sw = new StreamWriter("YPI.txt", false);

                Bitmap bmp = new Bitmap(maxFirst, maxSecond + 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                foreach (YesterdayPackedImage ypi in listImage) {

                    if (ypi.type != 8 && ypi.type != 4) {
                        if (ypi.second < lastY) {
                            lastY = ypi.second;
                            listBmps.Add(bmp);
                            bmp = new Bitmap(maxFirst, maxSecond + 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        }
                        lastY = ypi.second;
                        //sw.WriteLine(ypi.ToString());
                    }

                    if (ypi.type == 4) {
                        /*
                        // Feet section... No idea why
                        for (int i = 0; i < ypi.numValuesBase; i++) {
                            //Color c = Color.FromArgb(ypi.values[i], ypi.values[i], ypi.values[i]);
                            Color c = Color.Red;
                            bmp.SetPixel(ypi.first + i, ypi.second, c);
                        }
                        */
                    } else if (ypi.type == 6) {
                        for (int i = 0; i < ypi.numValuesBase; i++) {
                            Color c = Color.FromArgb(ypi.values[i * 3 + 2], ypi.values[i * 3 + 1], ypi.values[i * 3]);
                            bmp.SetPixel(ypi.first + i, ypi.second, c);
                        }
                    } else if (ypi.type == 7) {
                        for (int i = 0; i < ypi.numValuesBase; i++) {
                            Color c = Color.FromArgb(ypi.values[i * 4 + 3], ypi.values[i * 4 + 2], ypi.values[i * 4 + 1], ypi.values[i * 4 + 0]);
                            bmp.SetPixel(ypi.first + i, ypi.second, c);
                        }
                    } else if (ypi.type == 8) {
                        //Flags the next frame?
                        //if (listBmps.Count >= 30)
                            //break;
                    }
                }
                listBmps.Add(bmp);

                /*
                int k = 0;
                foreach (Bitmap b in listBmps)
                    b.Save("GT-Y-SP-" + (k++) + ".png", System.Drawing.Imaging.ImageFormat.Png);
                */

                //sw.Close();

                new FormImageAnimated(listBmps.ToArray()).Show();
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
