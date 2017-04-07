using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using GameTools;

namespace Test3ds {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            bool flip = false;
            GTFS fs = new GTFS("bed.3ds");

            while (fs.Position < fs.Length) {
                uint id = GT.ReadUInt16(fs, 2, flip);
                uint next = GT.ReadUInt32(fs, 4, flip);

                if (id == 0x4D4D) {
                    Console.WriteLine("Main Chunk");
                    uint fileSize = next;
                } else if (id == 0x0002) {
                    Console.WriteLine("M3D Version / 3DS-Version");
                    int version = GT.ReadInt32(fs, 4, flip);
                } else if (id == 0x3D33) {
                    Console.WriteLine("Unknown");
                    fs.Position += next;
                } else if (id == 0x3D3D) {
                    Console.WriteLine("3D Editor Chunk");
                } else if (id == 0x3D3E) {
                    int version = GT.ReadInt32(fs, 4, flip);
                    Console.WriteLine("Mesh version");
                } else if (id == 0x4000) {
                    Console.WriteLine("Object Block");
                    string name = GT.ReadASCIItoNull(fs, fs.Position, flip);
                    fs.Position += name.Length + 1;
                } else if (id == 0x4100) {
                    Console.WriteLine("Triangular mesh");
                } else if (id == 0x4110) {
                    Console.WriteLine("Vertices List");
                    int vertexnumber = GT.ReadInt16(fs, 2, flip);
                    for (int i = 0; i < vertexnumber; i++) {
                        float v1 = GT.ReadFloat(fs, 4, flip);
                        float v2 = GT.ReadFloat(fs, 4, flip);
                        float v3 = GT.ReadFloat(fs, 4, flip);
                    }
                } else if (id == 0x4120) {
                    Console.WriteLine("Faces description");
                    int facenumber = GT.ReadInt16(fs, 2, flip);
                    for (int i = 0; i < facenumber; i++) {
                        int vA = GT.ReadInt16(fs, 2, flip);
                        int vB = GT.ReadInt16(fs, 2, flip);
                        int vC = GT.ReadInt16(fs, 2, flip);
                        int faceFlag = GT.ReadInt16(fs, 2, flip);
                    }
                } else if (id == 0x4130) {
                    Console.WriteLine("Faces material list");
                    string name = GT.ReadASCIItoNull(fs, fs.Position, flip);
                    fs.Position += name.Length + 1;
                    int entries = GT.ReadInt16(fs, 2, flip);
                    for (int i = 0; i < entries; i++) {
                        int face = GT.ReadInt16(fs, 2, flip);
                    }
                } else if (id == 0x4140) {
                    Console.WriteLine("Mapping coordinates list for each vertex");
                    int vertnum = GT.ReadInt16(fs, 2, flip);
                    for (int i = 0; i < vertnum; i++) {
                        float uC = GT.ReadFloat(fs, 4, flip);
                        float vC = GT.ReadFloat(fs, 4, flip);
                    }
                } else if (id == 0x4150) {
                    Console.WriteLine("Smoothing groups list");
                    //List of Int, one per face
                    uint _numOfFaces = (next - 4) / 4;
                    for(uint i = 0; i < _numOfFaces; i++) {
                        int s = GT.ReadInt32(fs, 4, flip);
                    }
                } else if (id == 0x4160) {
                    Console.WriteLine("Local coordinate system");
                    float x1x = GT.ReadFloat(fs, 4, flip);
                    float x1y = GT.ReadFloat(fs, 4, flip);
                    float x1z = GT.ReadFloat(fs, 4, flip);

                    float x2x = GT.ReadFloat(fs, 4, flip);
                    float x2y = GT.ReadFloat(fs, 4, flip);
                    float x2z = GT.ReadFloat(fs, 4, flip);

                    float x3x = GT.ReadFloat(fs, 4, flip);
                    float x3y = GT.ReadFloat(fs, 4, flip);
                    float x3z = GT.ReadFloat(fs, 4, flip);

                    float ox = GT.ReadFloat(fs, 4, flip);
                    float oy = GT.ReadFloat(fs, 4, flip);
                    float oz = GT.ReadFloat(fs, 4, flip);

                } else if (id == 0xAFFF) {
                    Console.WriteLine("Material editor chunk");
                } else if (id == 0xA000) {
                    Console.WriteLine("Material name");
                    string name = GT.ReadASCIItoNull(fs, fs.Position, flip);
                    Console.WriteLine("> " + name);
                    fs.Position += name.Length + 1;
                } else if (id == 0xA010) {
                    Console.WriteLine("Material ambient color");
                } else if (id == 0xA020) {
                    Console.WriteLine("Diffuse color");
                } else if (id == 0xA030) {
                    Console.WriteLine("Material specular color");
                } else if (id == 0xA040) {
                    Console.WriteLine("Material shininess percent");
                } else if (id == 0xA041) {
                    Console.WriteLine("Material shininess strength percent");
                } else if (id == 0xA050) {
                    Console.WriteLine("Transparency percent");
                } else if (id == 0xA052) {
                    Console.WriteLine("Transparency falloff percent");
                } else if (id == 0xA053) {
                    Console.WriteLine("Reflection blur percent");
                } else if (id == 0xA084) {
                    Console.WriteLine("Self Illumination");
                    uint u1 = GT.ReadUInt32(fs, 4, flip);
                    uint u2 = GT.ReadUInt32(fs, 4, flip);
                } else if (id == 0xA087) {
                    Console.WriteLine("Wire thickness");
                    float wire = GT.ReadFloat(fs, 4, flip);
                } else if (id == 0xA08A) {
                    Console.WriteLine("In tranc / Transparency fall off IN");
                } else if (id == 0xA100) {
                    Console.WriteLine("Render type");
                    uint type = GT.ReadUInt16(fs, 2, flip);
                    //Unsigned Short [1=flat 2=gour. 3=phong 4=metal]

                } else if (id == 0xA200) {
                    Console.WriteLine("Texture map 1");
                    uint u1 = GT.ReadUInt32(fs, 4, flip);
                    uint u2 = GT.ReadUInt32(fs, 4, flip);
                } else if (id == 0xA300) {
                    Console.WriteLine("Mapping filename");
                    string name = GT.ReadASCIItoNull(fs, fs.Position, flip);
                    Console.WriteLine("> " + name);
                    fs.Position += name.Length + 1;
                } else if (id == 0xA351) {
                    Console.WriteLine("Mapping parameters");
                    uint u1 = GT.ReadUInt16(fs, 2, flip);
                } else if (id == 0xA353) {
                    Console.WriteLine("Mapping parameters");
                    uint u1 = GT.ReadUInt32(fs, 4, flip);
                } else if (id == 0xA354) {
                    Console.WriteLine("V scale");
                    float vS = GT.ReadFloat(fs, 4, flip);
                } else if (id == 0xA356) {
                    Console.WriteLine("U scale");
                    float uS = GT.ReadFloat(fs, 4, flip);

                } else if (id == 0xB000) {
                    Console.WriteLine("Keyframer chunk");
                    fs.Position += next - 4;
                } else if (id == 0x0011) {
                    Console.WriteLine("byte RGB");
                    byte r = GT.ReadByte(fs);
                    byte g = GT.ReadByte(fs);
                    byte b = GT.ReadByte(fs);
                } else if (id == 0x0030) {
                    Console.WriteLine("percent (int format)");
                    uint percent = GT.ReadUInt16(fs, 2, flip);
                } else if (id == 0x0100) {
                    Console.WriteLine("One unit");
                    float f = GT.ReadFloat(fs, 4, flip);
                } else {
                    long pos = fs.Position;
                    uint idd = id;
                    Console.WriteLine();
                }
            }
        }
    }
}
