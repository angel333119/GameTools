using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools2.Game.Yesterday {
    class ResourceSprite {

        public static void Open(string file, string outdir) {
            throw new NotImplementedException();

            bool flip = false;
            GTFS fs = new GTFS(file);
            int count = GT.ReadInt32(fs, 4, flip) / 4;

            for(int i = 0; i < count; i++) {

            }

        }
    }
}
