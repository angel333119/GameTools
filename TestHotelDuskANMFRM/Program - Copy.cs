using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameTools;
using System.IO;

namespace TestHotelDuskANMFRM {
    class Program {
        static void Main(string[] args) {

            bool flip = false;
            GTFS fs = new GTFS("Kaban3-F0.frm"); //Kaban3-F0.frm
            int zero = GT.ReadInt32(fs, 4, flip);
            int len = GT.ReadInt32(fs, 4, flip);
            int sixtyfour = GT.ReadInt32(fs, 4, flip);
            int next = GT.ReadInt32(fs, 4, flip);

            byte previousQ = 0;
            int previousAbove = 0;

            List<int[]> idea = new List<int[]>();
            idea.Add( Enumerable.Repeat(0xFF, 127).ToArray() );

            while(fs.Position - 16 < len) {
                byte b = GT.ReadByte(fs);

                if(b < 0x80) {
                    if (previousAbove > 0) {
                        //Console.WriteLine();
                        Console.Write("= " + previousAbove.ToString().PadLeft(3, ' '));
                        if (previousQ == previousAbove)
                            Console.Write(" *");

                        previousAbove = 0;
                        idea.Add(Enumerable.Repeat(0xFF, 127).ToArray());
                    }

                    previousQ = b;

                    //Console.WriteLine();
                    Console.Write("\r\n" + b.ToString().PadLeft(5, ' ') + " ( " + Convert.ToString(b, 2).PadLeft(8, '0') + " ) ");
                    //Console.WriteLine();
                } else {
                    idea.Last()[previousAbove] = b;

                    previousAbove++;
                    Console.Write(b.ToString("X2") + ", ");
                }
            }
            Console.Write("= " + previousAbove.ToString().PadLeft(3, ' ') + " (last)");

            Console.WriteLine("\r\n");

            FileStream nf = new FileStream("Test-HTD-ANMFRM.bin", FileMode.Create);
            foreach (int[] line in idea) {
                for(int i = 0; i < line.Length; i++) {
                    nf.WriteByte( (byte)line[i] );
                }
            }
            nf.Close();

        }
    }
}
