using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTools3D.Formats {
    public class Mesh {
        public uint meshId;
        public uint vertStart;
        public uint vertCount;
        public uint texid;

        public int off5;
        public int off6;
        public int off7;
        public int off8;

        public List<float[]> vertData;
        public List<float[]> normalData;
        public List<float[]> uvData;
        public List<int[]> faceData;

        public Mesh(uint meshId, uint vertStart, uint vertCount, uint texid, int off5, int off6, int off7, int off8) {
            this.meshId = meshId;
            this.vertStart = vertStart;
            this.vertCount = vertCount;
            this.texid = texid;
            this.off5 = off5;
            this.off6 = off6;
            this.off7 = off7;
            this.off8 = off8;

            vertData = new List<float[]>();
            normalData = new List<float[]>();
            uvData = new List<float[]>();
            faceData = new List<int[]>();
        }
    }

    public class MeshInfo {
        public int count000;
        public int count001;
        public int[] faceOffsets;
        public int sections;
        public int[] vertOffsets;

        public List<Mesh> meshTable;

        public MeshInfo(int sections, int[] vertOffsets, int[] faceOffsets, int count000, int count001) {
            this.sections = sections;
            this.vertOffsets = vertOffsets;
            this.faceOffsets = faceOffsets;
            this.count000 = count000;
            this.count001 = count001;

            meshTable = new List<Mesh>();
        }
    }
}
