﻿using GameTools;
using GameTools3D.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools2.Game.TimeSplitters2 {
    class ModelXbox : Model {

        public ModelXbox(string filename, string sf, bool useGTFSView = false) : base() {
            FileName = filename;
            string fileDir = filename.Replace(sf, "");

            bool flip = false;
            GTFS fs = new GTFS(FileName);
            meshInfo = new List<MeshInfo>();
            textures = new List<Texture>();

            int texStart = GT.ReadInt32(fs, 4, flip);
            int meshCountOffset = GT.ReadInt32(fs, 4, flip);
            //int unknownOffset1 = GT.ReadInt32(fs, 4, flip);
            //int unknownOffset2 = GT.ReadInt32(fs, 4, flip);

            fs.Position = texStart;

            long texID = 0;
            while (texID != -1) {
                texID = GT.ReadInt32(fs, 4, flip);

                int texUnk1 = GT.ReadInt32(fs, 4, flip); //Guessing X repeat?
                int texUnk2 = GT.ReadInt32(fs, 4, flip); //Guessing Y repeat?
                int texUnk3 = GT.ReadInt32(fs, 4, flip); //Guessing sound related?

                if (texID != -1) {
                    string fileTexture = "textures_";
                    if (texID < 10)
                        fileTexture += "0";
                    if (texID < 100)
                        fileTexture += "0";
                    if (texID < 1000)
                        fileTexture += "0";
                    fileTexture += texID + ".xbt";
                    textures.Add(new TextureXbox(fileDir + fileTexture, fileTexture));
                }
            }

            long meshTableOffset = -1;

            if (texStart == 0xC) {
                fs.Position = meshCountOffset;
                int modelCount = GT.ReadInt32(fs, 4, flip);
                int unk000 = GT.ReadInt32(fs, 4, flip);
                int unk001 = GT.ReadInt32(fs, 4, flip);
                meshTableOffset = (meshCountOffset - (0x9C * modelCount));
                fs.Position = meshTableOffset;
                for (int i = 0; i < modelCount; i++) {
                    byte[] bHead = GT.ReadBytes(fs, 6, flip);
                    fs.Position += 0xE; //14
                    int[] vertOffsets = new int[19];
                    for (int k = 0; k < 19; k++) {
                        vertOffsets[k] = GT.ReadInt32(fs, 4, flip);
                    }
                    int count000 = GT.ReadInt16(fs, 2, flip);
                    int count001 = GT.ReadInt16(fs, 2, flip);
                    fs.Position += 0x28;
                    float float000 = GT.ReadFloat(fs, 4, flip);
                    int[] faceOffsets = new int[3];
                    for (int k = 0; k < 3; k++) {
                        faceOffsets[k] = GT.ReadInt32(fs, 4, flip);
                    }
                    int sections = 1;
                    if (faceOffsets[2] != 0)
                        sections = 3;
                    else if (faceOffsets[1] != 0)
                        sections = 2;

                    if (vertOffsets[0] != 0)
                        meshInfo.Add(new MeshInfo(sections, vertOffsets, faceOffsets, count000, count001));
                }
            } else if (texStart == 0x20) {
                //Level
                long modelCount = (fs.Length - meshCountOffset) / 0xB0;
                for(long i = 0; i < modelCount; i++) {
                    fs.Position = meshCountOffset + (i * 0xB0);
                    long x = GT.ReadInt32(fs, 4, flip);
                    meshTableOffset = x - 0x9C; //Not sure why I had 0x90
                    if (meshTableOffset > 0) {
                        fs.Position = meshTableOffset;
                        //<<--
                        byte[] bHead = GT.ReadBytes(fs, 6, flip);
                        fs.Position += 0xE; //14
                        int[] vertOffsets = new int[19];
                        for (int k = 0; k < 19; k++) {
                            vertOffsets[k] = GT.ReadInt32(fs, 4, flip);
                        }
                        int count000 = GT.ReadInt16(fs, 2, flip);
                        int count001 = GT.ReadInt16(fs, 2, flip);
                        fs.Position += 0x28;
                        float float000 = GT.ReadFloat(fs, 4, flip);
                        int[] faceOffsets = new int[3];
                        for (int k = 0; k < 3; k++) {
                            faceOffsets[k] = GT.ReadInt32(fs, 4, flip);
                        }
                        int sections = 1;
                        if (faceOffsets[2] != 0)
                            sections = 3;
                        else if (faceOffsets[1] != 0)
                            sections = 2;

                        if (vertOffsets[0] != 0)
                            meshInfo.Add(new MeshInfo(sections, vertOffsets, faceOffsets, count000, count001));
                        //<<--
                    }
                }
            }

            foreach (MeshInfo meshinfo in meshInfo) {
                int objCount = 0;
                if (texStart == 0xC) {
                    objCount = (((meshinfo.vertOffsets[4] - meshinfo.vertOffsets[0]) - 2) / 10);
                    fs.Position = meshinfo.vertOffsets[0];
                } else {
                    objCount = (((meshinfo.vertOffsets[7] - meshinfo.vertOffsets[4])) / 0x10);
                    fs.Position = meshinfo.vertOffsets[0];
                }

                for (int a = 0; a < objCount; a++) {
                    uint texid = GT.ReadUInt16(fs, 2, flip);
                    uint meshId = GT.ReadUInt16(fs, 2, flip);
                    uint vertStart = GT.ReadUInt16(fs, 2, flip);
                    uint vertCount = GT.ReadUInt16(fs, 2, flip);
                    uint unk003 = GT.ReadUInt16(fs, 2, flip);

                    //if(unk003 != 0) // Have yet to see anything else (okay mapboy ph had some)
                        //Console.WriteLine("Mesh " + meshId + " Tex " + texid + " Unk003: " + unk003);

                    if (meshId >= meshinfo.meshTable.Count)
                        meshinfo.meshTable.Add(new Mesh(meshId, vertStart, vertCount, texid, meshinfo.vertOffsets[5], meshinfo.vertOffsets[6], meshinfo.vertOffsets[7], meshinfo.vertOffsets[8]));
                }

                if (meshinfo.count000 >= 2) {
                    if (texStart == 0xC) {
                        objCount = (((meshinfo.vertOffsets[9] - meshinfo.vertOffsets[5]) - 2) / 10);
                        fs.Position = meshinfo.vertOffsets[0];
                    } else {
                        objCount = (((meshinfo.vertOffsets[12] - meshinfo.vertOffsets[9])) / 0x10);
                        fs.Position = meshinfo.vertOffsets[1];
                        for (int a = 0; a < objCount; a++) {
                            uint texid = GT.ReadUInt16(fs, 2, flip);
                            uint meshId = GT.ReadUInt16(fs, 2, flip);
                            uint vertStart = GT.ReadUInt16(fs, 2, flip);
                            uint vertCount = GT.ReadUInt16(fs, 2, flip);
                            uint unk003 = GT.ReadUInt16(fs, 2, flip);

                            //if (unk003 != 0) // Have yet to see anything else
                                //Console.WriteLine("Mesh " + meshId + " Tex " + texid + " Unk003: " + unk003);

                            if (vertCount != 0xFFFF)
                                meshinfo.meshTable.Add(new Mesh(meshId, vertStart, vertCount, texid, meshinfo.vertOffsets[10], meshinfo.vertOffsets[11], meshinfo.vertOffsets[12], meshinfo.vertOffsets[13]));
                        }
                    }
                } else if (meshinfo.count000 >= 3) {
                    if (texStart == 0xC) {
                        objCount = (((meshinfo.vertOffsets[14] - meshinfo.vertOffsets[10]) - 2) / 10);
                        fs.Position = meshinfo.vertOffsets[2];
                    } else {
                        objCount = (((meshinfo.vertOffsets[17] - meshinfo.vertOffsets[14])) / 0x10);
                        fs.Position = meshinfo.vertOffsets[2];
                        for (int a = 0; a < objCount; a++) {
                            uint texid = GT.ReadUInt16(fs, 2, flip);
                            uint meshId = GT.ReadUInt16(fs, 2, flip);
                            uint vertStart = GT.ReadUInt16(fs, 2, flip);
                            uint vertCount = GT.ReadUInt16(fs, 2, flip);
                            uint unk003 = GT.ReadUInt16(fs, 2, flip);

                            //if (unk003 != 0) // Have yet to see anything else
                                //Console.WriteLine("Mesh " + meshId + " Tex " + texid + " Unk003: " + unk003);

                            if (vertCount != 0xFFFF)
                                meshinfo.meshTable.Add(new Mesh(meshId, vertStart, vertCount, texid, meshinfo.vertOffsets[15], meshinfo.vertOffsets[16], meshinfo.vertOffsets[17], meshinfo.vertOffsets[18]));
                        }
                    }
                }

                foreach (Mesh mesh in meshinfo.meshTable) {
                    int facedir = 0;
                    fs.Position = mesh.off5 + (0x10 * mesh.vertStart);
                    //setName
                    //setMaterial
                    for (int b = 0; b < mesh.vertCount; b++) {
                        float vx = GT.ReadFloat(fs, 4, flip);
                        float vy = GT.ReadFloat(fs, 4, flip);
                        float vz = GT.ReadFloat(fs, 4, flip);
                        mesh.vertData.Add(new float[] { vx, vy, vz });

                        byte wind = GT.ReadByte(fs);
                        byte flag = GT.ReadByte(fs);
                        ushort scale = GT.ReadUInt16(fs, 2, flip);

                        //if (scale != 16256 || (flag != 0 && flag != 128))
                            //Console.WriteLine("Mesh " + mesh.meshId + " wind: " + wind + " flag: " + flag + " scale: " + scale);

                        if (flag == 0x00) {
                            if ((facedir ^ wind) == 0) {
                                mesh.faceData.Add(new int[] { b - 2, b - 1, b });
                            } else {
                                mesh.faceData.Add(new int[] { b - 1, b - 2, b });
                            }
                        } else
                            facedir = 1;
                        facedir = 1 - facedir;
                    }
                    fs.Position = mesh.off6 + (0xC * mesh.vertStart);
                    for (int b = 0; b < mesh.vertCount; b++) {
                        float tu = GT.ReadFloat(fs, 4, flip);
                        float tv = GT.ReadFloat(fs, 4, flip);
                        float tw = GT.ReadFloat(fs, 4, flip);
                        mesh.uvData.Add(new float[] { tu * tw, tv * tw });
                        //Console.WriteLine(fs.Position);
                    }
                    if (texStart == 0xC) {
                        fs.Position = mesh.off8 + (0x10 * mesh.vertStart);
                        for (int b = 0; b < mesh.vertCount; b++) {
                            float nx = GT.ReadFloat(fs, 4, flip);
                            float ny = GT.ReadFloat(fs, 4, flip);
                            float nz = GT.ReadFloat(fs, 4, flip);
                            float nw = GT.ReadFloat(fs, 4, flip);
                            mesh.normalData.Add(new float[] { nx, ny, nz });
                            HasNormals = true;
                        }
                    } else
                        fs.Position = mesh.off7 + (0x10 * mesh.vertStart);

                }
            }

            if(useGTFSView)
                new GTFSView(fs).Show();
        }
    }
}
