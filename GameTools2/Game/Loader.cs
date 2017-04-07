using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GameTools2.Game {
    public abstract class Loader {
        public string PackExtension;
        public string MassExtension;

        public abstract void Open(OpenFileDialog openFileDialog1, bool export = false, bool useGTFSView = false);

        public abstract void OpenAllPaks(List<string> dirfiles);
        public abstract void MassConvert(List<string> dirfiles);
    }
}