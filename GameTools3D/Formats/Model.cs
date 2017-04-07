using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools3D.Formats {
    public class Model {
        protected string FileName;
        public List<MeshInfo> meshInfo;
        public List<Texture> textures;
        public bool HasNormals;

        /*
        protected List<byte> fsMem1; //Type5 - Joins
        protected List<float> fsMem2; //X,Y,Z - verticies
        protected List<long> fsMem3; //Faces
        protected List<float> fsMem4; //Unknown, whatever is after verticies
        public List<Point3D> Points;
        */

        public string GetFileName { get { return FileName; } }
        public string GetSafeFileName { get { return Path.GetFileName(FileName); } }

        public float boundingXmin, boundingXmax, boundingYmin, boundingYmax, boundingZmin, boundingZmax;

        public Model() {
            boundingXmin = 0.0f;
            boundingXmax = 0.0f;
            boundingYmin = 0.0f;
            boundingYmax = 0.0f;
            boundingZmin = 0.0f;
            boundingZmax = 0.0f;

            HasNormals = false;
        }

        public void GenBoundingBox() {
            foreach (MeshInfo meshInfo in meshInfo) {
                foreach (Mesh mesh in meshInfo.meshTable) {
                    for (int v = 0; v < mesh.vertData.Count; v++) {

                        if (mesh.vertData[v][0] < boundingXmin)
                            boundingXmin = mesh.vertData[v][0];
                        if (mesh.vertData[v][0] > boundingXmax)
                            boundingXmax = mesh.vertData[v][0];

                        if (mesh.vertData[v][1] < boundingYmin)
                            boundingYmin = mesh.vertData[v][1];
                        if (mesh.vertData[v][1] > boundingYmax)
                            boundingYmax = mesh.vertData[v][1];

                        if (mesh.vertData[v][2] < boundingZmin)
                            boundingZmin = mesh.vertData[v][2];
                        if (mesh.vertData[v][2] > boundingZmax)
                            boundingZmax = mesh.vertData[v][2];
                    }
                }
            }
        }

        /*
        public void GenPoints() {
            Points = new List<Point3D>();

            for (int Vertex = 0; Vertex < fsMem2.Count; Vertex++) {
                Point3D p = new Point3D(fsMem2[Vertex++], fsMem2[Vertex++], fsMem2[Vertex]);
                Points.Add(p);
            }
        }
        */

        public void WriteOBJ() {
            string filename = "test";

            //var objContents = "g " + "objModelName" + Environment.NewLine + Environment.NewLine + "# List of vertices" + Environment.NewLine;
            var objContents = "# GT OBJ Export" + Environment.NewLine;
            objContents += "mtllib " + filename + ".mtl" + Environment.NewLine;
            objContents += "g" + Environment.NewLine;

            foreach (MeshInfo meshInfo in meshInfo) {
                foreach (Mesh mesh in meshInfo.meshTable) {
                    foreach (float[] vertices in mesh.vertData) {
                        objContents += "v ";
                        foreach (float vertex in vertices) {
                            objContents += vertex.ToString("F20") + " ";
                        }
                        objContents += Environment.NewLine;
                    }
                }
            }

            objContents += Environment.NewLine;

            foreach (MeshInfo meshInfo in meshInfo) {
                foreach (Mesh mesh in meshInfo.meshTable) {
                    foreach (float[] uv in mesh.uvData) {
                        objContents += "vt ";
                        foreach (float v in uv) {
                            objContents += v.ToString("F20") + " ";
                        }
                        objContents += "0.000000";
                        objContents += Environment.NewLine;
                    }
                }
            }

            objContents += Environment.NewLine;

            foreach (MeshInfo meshInfo in meshInfo) {
                foreach (Mesh mesh in meshInfo.meshTable) {
                    foreach (float[] vn in mesh.normalData) {
                        objContents += "vn ";
                        foreach (float v in vn) {
                            objContents += v.ToString("F20") + " ";
                        }
                        objContents += Environment.NewLine;
                    }
                }
            }

            objContents += Environment.NewLine + "g 0" + Environment.NewLine;
            objContents += "usemtl 1418.tga" + Environment.NewLine;
            foreach (MeshInfo meshInfo in meshInfo) {
                foreach (Mesh mesh in meshInfo.meshTable) {
                    foreach (int[] face in mesh.faceData) {
                        objContents += "f ";
                        foreach (int face_vertex in face) {
                            int fv = face_vertex + 1;
                            //objContents += face_vertex.ToString() + " ";
                            objContents += fv.ToString() + "/";
                            objContents += fv.ToString() + "/";
                            objContents += fv.ToString() + " ";
                        }
                        objContents += Environment.NewLine;
                    }
                }
            }

            System.IO.File.WriteAllText(filename + ".obj", objContents);
        }

        public void Write3DS() {
            return;

            long Snake = meshInfo[0].meshTable[0].vertCount * 12;
            long Sum = (meshInfo[0].meshTable[0].faceData.Count * 8) + Snake;

            FileStream fsname = File.Create(FileName + ".3ds");
            fsname.Write(new byte[] { 77, 77 }, 0, 2); //Main Chunk
            fsname.Write(BitConverter.GetBytes((long)(Sum + 60)), 0, 4); //-next chunk offset-4?

            fsname.Write(new byte[] { 2, 0, //M3D Version
                10, 0, 0, 0, //-next chunk offset-4?
                3, 0, 0, 0, //version
                61, 61 //3D Editor Chunk
            }, 0, 12);
            fsname.Write(BitConverter.GetBytes((long)(Sum + 44)), 0, 4); //-next chunk offset-4?

            fsname.Write(new byte[] { 0, 64 }, 0, 2); //Object Block
            fsname.Write(BitConverter.GetBytes((long)(Sum + 38)), 0, 4);  //-next chunk offset-4?
            fsname.Write(new byte[] { 100, 114, 97, 103, 111, 110, 106, 97, 110, 0, //ASCII null-terminated name for object
                0, 65 //Triangular Mesh
            }, 0, 12);
            fsname.Write(BitConverter.GetBytes((long)(Sum + 22)), 0, 4);

            fsname.Write(new byte[] { 16, 65 }, 0, 2); //Vertices List
            fsname.Write(BitConverter.GetBytes((long)(Snake + 8)), 0, 4);
            fsname.Write(BitConverter.GetBytes((short)meshInfo[0].meshTable[0].vertCount), 0, 2);
            foreach(float[] v in meshInfo[0].meshTable[0].vertData) {
                fsname.Write(BitConverter.GetBytes(v[0]), 0, 4);
                fsname.Write(BitConverter.GetBytes(v[1]), 0, 4);
                fsname.Write(BitConverter.GetBytes(v[2]), 0, 4);
            }

            fsname.Write(new byte[] { 32, 65 }, 0, 2); //Faces Description
            fsname.Write(BitConverter.GetBytes((long)((meshInfo[0].meshTable[0].faceData.Count * 8) + 8)), 0, 4);  //-next chunk offset-4?
            fsname.Write(BitConverter.GetBytes((short)meshInfo[0].meshTable[0].faceData.Count), 0, 2);
            foreach (int[] f in meshInfo[0].meshTable[0].faceData) {
                fsname.Write(BitConverter.GetBytes(f[0]), 0, 2);
                fsname.Write(BitConverter.GetBytes(f[1]), 0, 2);
                fsname.Write(BitConverter.GetBytes(f[2]), 0, 2);
            }

            fsname.Close();
        }
    }
}
