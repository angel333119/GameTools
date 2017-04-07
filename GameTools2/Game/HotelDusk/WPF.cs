using GameTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2.Game.HotelDusk {
    static class WPF {
        //public static string base_path = @"C:\Users\jas2o\Desktop\NDS\NDS_UNPACK_HDR215\datadata";
        public static string extract_path = @"C:\Users\jas2o\Desktop\GT-HotelDusk\Extract";

        public static void Open(OpenFileDialog openFileDialog) {
            bool flip = false;
            GTFS fs = new GTFS(openFileDialog.FileName);

            List<Pack> listPack = new List<Pack>();

            while (fs.Position < fs.Length - 40) {
                byte[] nameArray = new byte[24];
                for (int k = 0; k < 24; k++)
                    nameArray[k] = (byte)fs.ReadByte();

                long fileLen = GT.ReadUInt32(fs, 4, flip);
                long nextFileOffset = GT.ReadUInt32(fs, 4, flip);
                long currentOffset = fs.Position;

                string name = Encoding.Default.GetString(nameArray);
                name = name.Substring(1).Replace("\0", "").Replace(".bin", ".wpfbin");

                listPack.Add(new Pack(name, currentOffset, fileLen));

                fs.Seek(nextFileOffset, SeekOrigin.Begin);
            }

            string toFolder = openFileDialog.SafeFileName.Replace('.', '_');

            if (!Directory.Exists(extract_path))
                Directory.CreateDirectory(extract_path);

            if (!Directory.Exists(extract_path + "\\" + toFolder))
                Directory.CreateDirectory(extract_path + "\\" + toFolder);

            foreach (Pack pack in listPack) {
                string newfile = extract_path + "\\" + toFolder + "\\" + pack.Filename;
                GT.WriteSubFile(fs, newfile, pack.Size, pack.Offset);
            }
        }
    }
}
