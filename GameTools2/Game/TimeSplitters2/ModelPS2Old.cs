using GameTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools2.Game.TimeSplitters2 {
    class ModelPS2Old : Model {

        public ModelPS2Old(string filename) : base() {
            FileName = filename;
            fsMem1 = new List<byte>(); //Type5 - Joins
            fsMem2 = new List<float>(); //X,Y,Z - Vertexes
            fsMem3 = new List<long>(); //Faces
            fsMem4 = new List<float>();

            //--

            bool flip = false;
            GTFS fs0 = new GTFS(FileName);

            int iHeader = GT.ReadInt32(fs0, 4, flip);
            int _base = GT.ReadInt32(fs0, 4, flip);
            fs0.Position = _base;
            int meshnumber = GT.ReadInt16(fs0, 2, flip);

            //Meshes = new Mesh3D[meshnumber];

            StringBuilder csv = new StringBuilder();

            for (int mesh = 1; mesh <= meshnumber; mesh++) {
                //Meshes[mesh] = new Mesh3D(mesh);
                //textBox1.AppendText("Mesh " + mesh + "\r\n");
                long wolf = _base + (-144 * mesh);

                fs0.Position = wolf + 96; //98
                int numvertex = GT.ReadInt16(fs0, 2, flip);
                //The value at +98 might happen to be numfaces/2 ?

                fs0.Position = wolf + 40;
                int tovertexes = GT.ReadInt32(fs0, 4, flip);

                fs0.Position = tovertexes;
                for (int v = 0; v < numvertex; v++) {
                    float Type1 = GT.ReadFloat(fs0, 4, flip); //fVX
                    float Type2 = GT.ReadFloat(fs0, 4, flip); //fVY
                    float Type3 = GT.ReadFloat(fs0, 4, flip); //fVZ
                    byte Type4 = GT.ReadByte(fs0); //fVC
                    byte Type5 = GT.ReadByte(fs0);
                    byte Type6 = GT.ReadByte(fs0);
                    byte Type7 = GT.ReadByte(fs0);

                    fsMem2.Add(Type1);
                    fsMem2.Add(Type2);
                    fsMem2.Add(Type3);

                    fsMem1.Add(Type5);

                    //csv.AppendLine(Type1 + "," + Type2 + "," + Type3);

                    //Meshes[mesh].Points.Add(new Point3D(fVX, fVY, fVZ, fVC));
                    //textBox1.AppendText(fVX + "," + fVY + "," + fVZ + "," + fVC + "\r\n");
                }
                // The stuff after the verticies
                for (int v = 0; v < numvertex; v++) {
                    float Type1 = GT.ReadFloat(fs0, 4, flip);
                    float Type2 = GT.ReadFloat(fs0, 4, flip);
                    float Type3 = GT.ReadFloat(fs0, 4, flip);

                    fsMem4.Add(Type1);
                    fsMem4.Add(Type2);
                    fsMem4.Add(Type3);

                    csv.AppendLine(Type1 + "," + Type2 + "," + Type3);
                }

                //textBox1.AppendText("\r\n");
            }

            //foreach(Mesh3D mesh in Meshes) { checklistMeshes.Items.Add("Mesh " + mesh.ID, true); }

            for (int FFF = 2; FFF < fsMem1.Count; FFF++) {
                byte cool1 = fsMem1[FFF - 2];
                byte cool2 = fsMem1[FFF - 1];
                byte cool3 = fsMem1[FFF];

                if (cool3 != 128) {
                    fsMem3.Add((short)(FFF - 2));
                    fsMem3.Add((short)(FFF - 1));
                    fsMem3.Add((short)(FFF));
                    fsMem3.Add((short)(0));
                    fsMem3.Add((short)(FFF - 1));
                    fsMem3.Add((short)(FFF - 2));
                    fsMem3.Add((short)(FFF));
                    fsMem3.Add((short)(0));
                }
            }

            if (File.Exists("test.csv"))
                File.Delete("test.csv");
            File.WriteAllText("test.csv", csv.ToString());
        }

    }
}
