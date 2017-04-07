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

namespace GameTools2.Game.Runaway {
    class Loader : Game.Loader {

        public override void Open(OpenFileDialog openFileDialog, bool export = false, bool useGTFSView = false) {
            openFileDialog.FileName = "";
            openFileDialog.Filter = "All Files (*.*)|*.*";

            DialogResult res = openFileDialog.ShowDialog();
            if (res == DialogResult.OK) {
                //--
                string[] filenameParts = openFileDialog.SafeFileName.Split('.');
                Array.Reverse(filenameParts);

                bool flip = false;
                GTFS fs = new GTFS(openFileDialog.FileName);

                // I'm not convinced ANY of this is working correctly.

                if (!Directory.Exists("out"))
                    Directory.CreateDirectory("out");

                if (openFileDialog.SafeFileName == "RESOURCE.004") {
                    #region Runaway: A Road Adventure 004 File
                    //This only applies to the file in Runaway: A Road Adventure
                    //The version of file used in The Dream of the Turtle is 7 bytes per line instead, unsure what it means though.

                    DialogResult dialogResult = MessageBox.Show("This file is huge and not interesting, extarct it anyway?", "Runaway: A Road Adventure 004 File", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dialogResult == DialogResult.Yes) {
                        List<long> listOffsets = new List<long>();
                        List<int> listLengths = new List<int>();

                        while (fs.Position < 36000) {
                            long off = GT.ReadInt32(fs, 4, flip);
                            int len = GT.ReadInt16(fs, 2, flip);

                            if (off > 0 && len > 0) {
                                listOffsets.Add(off);
                                listLengths.Add(len);
                            }
                        }

                        for (int i = 0; i < listOffsets.Count; i++) {
                            string newfile = "out\\" + openFileDialog.SafeFileName + "-" + i + ".bin";
                            if (!File.Exists(newfile))
                                GT.WriteSubFile(fs, newfile, listLengths[i], listOffsets[i]);
                        }
                    }
                    #endregion
                } else if (filenameParts[0].ToUpper()[0] == 'M') {
                    List<long> listOffsets = ReadOffsetsToPos(fs, 400, flip);

                    for (int i = 0; i < listOffsets.Count - 1; i++) {
                        string newfile = "out\\" + openFileDialog.SafeFileName + "-" + i + ".bin";
                        if (!File.Exists(newfile))
                            GT.WriteSubFile(fs, newfile, listOffsets[i + 1] - listOffsets[i], listOffsets[i]);
                    }

                    Console.WriteLine();
                } else if (filenameParts[0].ToUpper()[0] == 'S') {
                    List<long> listOffsets = ReadOffsetsToPos(fs, 4000, flip);

                    for (int i = 0; i < listOffsets.Count - 1; i++) {
                        string newfile = "out\\" + openFileDialog.SafeFileName + "-" + i + ".bin";
                        if (!File.Exists(newfile))
                            GT.WriteSubFile(fs, newfile, listOffsets[i + 1] - listOffsets[i], listOffsets[i]);
                    }

                    Console.WriteLine();
                } else {
                    //Treat as 000
                }
                //--
            }
        }

        public override void OpenAllPaks(List<string> dirfiles) {
            throw new NotImplementedException();
        }

        private List<long> ReadOffsetsToPos(GTFS fs, int maxpos, bool flip) {
            List<long> listOffsets = new List<long>();
            while (fs.Position < maxpos) {
                int off = GT.ReadInt32(fs, 4, flip);

                if (off > 0 && !listOffsets.Contains(off))
                    listOffsets.Add(off);
            }

            return listOffsets;
        }

        public override void MassConvert(List<string> dirfiles) {
            throw new NotImplementedException();
        }
    }
}
