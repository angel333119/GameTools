using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameTools2.Game.LastWindow {
    static class LWBRA {

        public static void Open(OpenFileDialog openFileDialog) {
            bool flip = false;
            GTFS fs = new GTFS(openFileDialog.FileName);

            int numFrames = GT.ReadInt32(fs, 4, flip);

            //int[] remaining = new int[22];
            //for(int i = 0; i < 22; i++)
                //remaining[i] = GT.ReadInt32(fs, 4, flip);
            //fs.Position = 0x5C;

            int[] offsetCoTable = new int[numFrames];
            int[] lenCoTable = new int[numFrames];
            int[] lenBuffer = new int[numFrames]; //This doesn't seem to be at all related to the length of the CoTable...

            for (int i = 0; i < numFrames; i++) {
                offsetCoTable[i] = GT.ReadInt32(fs, 4, flip);
                lenCoTable[i] = GT.ReadInt32(fs, 4, flip);
                lenBuffer[i] = GT.ReadInt32(fs, 4, flip);
            }

            //--

            for (int i = 0; i < numFrames; i++) {
                bool[,] coord = new bool[16, 16];

                fs.Position = offsetCoTable[i];
                //Console.WriteLine("Pos: " + fs.Position);
                for (int k = 0; k < lenCoTable[i]; k+=2) {
                    int x = GT.ReadByte(fs);
                    int y = GT.ReadByte(fs);

                    coord[x,y] = true;
                }

                GT.WriteSubFile(fs, "test-" + i + ".brf", lenBuffer[i]);

                /*
                for(int x = 0; x < 16; x++) {
                    for(int y = 0; y < 16; y++) {
                        if(coord[x,y])
                            Console.Write("X");
                        else
                            Console.Write(".");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\n");
                */
            }

            Console.WriteLine();
        }

    }
}
