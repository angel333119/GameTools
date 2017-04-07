using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameTools {
    public class GTFSSlow : GTFS {
        //Use GTFSSlow for files that are too large to be used with GTFS

        private FileStream fs;

        public new long Position { get { return fs.Position; } set { fs.Position = value; } }
        public new int Length { get { return (int) fs.Length; } }

        public GTFSSlow(string path) : base(path, false) {
            fs = new FileStream(file, FileMode.Open);
        }

        public override byte ReadByte() {
            return (byte)fs.ReadByte();
        }

        public override void Read(byte[] buffer, int bufferoffset, int length) {
            fs.Read(buffer, bufferoffset, length);
        }
    }
}
