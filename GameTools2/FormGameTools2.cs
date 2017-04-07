using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GameTools3D;
using GameTools3D.Formats;
using System.Threading;
using System.IO;
using GameTools2.Game;

namespace GameTools2 {
    public partial class FormGameTools2 : Form {
        private static Viewer viewer;
        private static ListBox _listFiles;

        private static Loader loader;

        public FormGameTools2() {
            InitializeComponent();
            _listFiles = listFiles;
        }

        private void Form1_Load(object sender, EventArgs e) {
            cmbGameSelect.Items.Add("TimeSplitters 2 (GameCube)");
            cmbGameSelect.Items.Add("TimeSplitters 2 (PS2)");
            cmbGameSelect.Items.Add("TimeSplitters 2 (Xbox)");
            cmbGameSelect.Items.Add("Second Sight (GameCube)");
            cmbGameSelect.Items.Add("Second Sight (PC)");
            cmbGameSelect.Items.Add("Hotel Dusk: Room 215");
            cmbGameSelect.Items.Add("Last Window: The Secret of Cape West");
            cmbGameSelect.Items.Add("Runaway: The Road Adventure");
            cmbGameSelect.Items.Add("Runaway: The Dream of the Turtle NDS (Sprites)");
            cmbGameSelect.Items.Add("Yesterday");
            cmbGameSelect.Items.Add("Yesterday (Sprites)");

            cmbGameSelect.SelectedIndex = Properties.Settings.Default.SelectedLoader;
        }

        private void cmbGameSelect_SelectedIndexChanged(object sender, EventArgs e) {
            switch (cmbGameSelect.SelectedIndex) {
                case 0: loader = new Game.TimeSplitters2.LoaderGCN(); break;
                case 1: loader = new Game.TimeSplitters2.LoaderPS2(); break;
                case 2: loader = new Game.TimeSplitters2.LoaderXbox(); break;
                case 3: loader = new Game.SecondSight.LoaderGCN(); break;
                case 4: loader = new Game.SecondSight.LoaderPC(); break;
                case 5: loader = new Game.HotelDusk.Loader(); break;
                case 6: loader = new Game.LastWindow.Loader(); break;
                case 7: loader = new Game.Runaway.Loader(); break;
                case 8: loader = new Game.RunawayNDSSPR.Loader(); break;
                case 9: loader = new Game.Yesterday.Loader(); break;
                case 10: loader = new Game.YesterdaySPR.Loader(); break;
            }

            Properties.Settings.Default.SelectedLoader = cmbGameSelect.SelectedIndex;
            Properties.Settings.Default.Save();
        }

        private void btnLoadAuto_Click(object sender, EventArgs e) {
            loader.Open(openFileDialog1, chkLoadSingleExport.Checked, chkGTFSView.Checked);
        }

        public static void ListFiles(List<string> files) {
            _listFiles.Items.Clear();
            foreach (string f in files)
                _listFiles.Items.Add(f);
        }

        public static void UseViewer(Model model) {
            Thread threadOpenTK = new Thread(new ThreadStart(() => {
                viewer = new Viewer(800, 600);
                viewer.LoadModel(model);
                viewer.Run();
            }));
            threadOpenTK.Start();
        }

        private void btnLoadDir_Click(object sender, EventArgs e) {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All Files (*.*)|*.*";

            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK) {
                List<string> dirfiles = DirectoryToArray(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, ""), loader.PackExtension);
                loader.OpenAllPaks(dirfiles);
            }
        }

        private List<string> DirectoryToArray(string dir, string reqExt="") {
            List<string> dirfiles = Directory.GetFiles(dir).ToList();

            string[] subdirectoryEntries = Directory.GetDirectories(dir);
            foreach (string subdir in subdirectoryEntries) {
                List<string> subdirfiles = DirectoryToArray(subdir, reqExt);
                dirfiles.AddRange(subdirfiles);
            }

            for(int i = dirfiles.Count - 1; i >= 0; i--) {
                if (!dirfiles[i].ToLower().Contains(reqExt))
                    dirfiles.RemoveAt(i);
            }

            return dirfiles;
        }

        private void btnMassConvert_Click(object sender, EventArgs e) {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All Files (*.*)|*.*";

            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK) {
                List<string> dirfiles = DirectoryToArray(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, ""), loader.MassExtension);
                loader.MassConvert(dirfiles);
            }
        }

        private void btnMassConvertTextures_Click(object sender, EventArgs e) {
            if(loader is Game.TimeSplitters2.LoaderXbox) {
                openFileDialog1.FileName = "";
                openFileDialog1.Filter = "All Files (*.*)|*.*";

                DialogResult res = openFileDialog1.ShowDialog();
                if (res == DialogResult.OK) {
                    List<string> dirfiles = DirectoryToArray(openFileDialog1.FileName.Replace(openFileDialog1.SafeFileName, ""), ".xbt");
                    ((Game.TimeSplitters2.LoaderXbox)loader).MassConvertTextures(dirfiles);
                }
            }
        }
    }
}
