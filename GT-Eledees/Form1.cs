using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using GameTools;
using GameTools.GTFSView;

namespace GT_Eledees {
    public partial class Form1 : Form {

        string file = "obj_02_054_02_00.brres";

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            GTFS brres = new GTFS(file);
            bool flip = false;

            //Header
            string fileID = GT.ReadASCII(brres, 4, flip);
            byte[] bBOM = GT.ReadBytes(brres, 2, flip);
            if (bBOM[0] == 0xFE)
                flip = true;
            byte[] bPV = GT.ReadBytes(brres, 2, flip); //Pad/Version
            uint fileLen = GT.ReadUInt32(brres, 4, flip);
            ushort rootOffset = GT.ReadUInt16(brres, 2, flip);
            ushort numSections = GT.ReadUInt16(brres, 2, flip);

            //Root
            brres.Position = rootOffset;
            string rootID = GT.ReadASCII(brres, 4, false);
            uint rootLen = GT.ReadUInt32(brres, 4, flip); ///  <<<<<<<<-------- This might be key, the group is as long as this?
            long positionAfterRoot = brres.Position;

            //First index group
            //long lastPosition = brres.Position;

            //http://wiki.tockdom.com/wiki/BRRES_Index_Group_(File_Format)
            uint groupLen = GT.ReadUInt32(brres, 4, flip);
            uint groupNum = GT.ReadUInt32(brres, 4, flip);

            Console.WriteLine();

            for(uint i = 0; i < groupNum + 1; i++) { //First group is special, doesn't count
                ushort entryID = GT.ReadUInt16(brres, 2, flip);
                ushort unknown = GT.ReadUInt16(brres, 2, flip); //Zero?
                ushort indexLeft = GT.ReadUInt16(brres, 2, flip);
                ushort indexRight = GT.ReadUInt16(brres, 2, flip);
                uint nameOffset = GT.ReadUInt32(brres, 4, flip);
                uint dataOffset = GT.ReadUInt32(brres, 4, flip);

                if (i == 0)
                    continue;

                long posLast = brres.Position;
                //--
                //long lNameOffset = rootOffset + 4 + nameOffset;
                brres.Position = rootOffset + 4 + nameOffset;
                uint nameLen = GT.ReadUInt32(brres, 4, flip);
                if (nameLen > 30)
                    throw new Exception();

                string name = GT.ReadASCII(brres, (int)nameLen, false);
                ushort nameGapZero = GT.ReadUInt16(brres, 2, flip); //Zero?
                uint name2Len = GT.ReadUInt32(brres, 4, flip);
                string name2 = GT.ReadASCII(brres, (int)name2Len, false);
                //Console.WriteLine();
                //I have no idea, skipping
                //--
                brres.Position = posLast;
            }

            /*
            brres.Position = 160;


            //MDL0
            long subfileoffstart = brres.Position;
            string subfileID = GT.ReadASCII(brres, 4, false);
            uint subfileLen = GT.ReadUInt32(brres, 4, flip);
            uint subfileVer = GT.ReadUInt32(brres, 4, flip);

            if (subfileVer != 8)
                throw new Exception(); //Different version, different number of sections

            int outerOffset = GT.ReadInt32(brres, 4, flip); //Negative

            if (subfileVer == 8) { // N * 4
                for(int n = 0; n < subfileVer; n++) {
                    uint n0 = GT.ReadUInt32(brres, 4, flip);
                    uint n1 = GT.ReadUInt32(brres, 4, flip);
                    uint n2 = GT.ReadUInt32(brres, 4, flip);
                    uint n3 = GT.ReadUInt32(brres, 4, flip);
                }
            }

            uint nameOffset2 = GT.ReadUInt32(brres, 4, flip) + (uint)subfileoffstart;

            //brres.Position = lastPosition + groupLen;

            //string nextID = ReadASCII(brres, 4, false);

            long position = brres.Position;
            Console.WriteLine();
            */

            new GTFSView(brres).Show();
        }
    }
}
