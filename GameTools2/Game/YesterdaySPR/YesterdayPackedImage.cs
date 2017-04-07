using GameTools;

namespace GameTools2.Game.YesterdaySPR {
    class YesterdayPackedImage {
        public int first;
        public int second;
        public byte type;
        public int numValuesBase;
        public int numValuesLen;
        public byte[] values;

        public int derivedfirstadd;
        public int derivedfirstsubtract;

        public YesterdayPackedImage(GTFS fs) {
            first = GT.ReadInt16(fs, 2, false);
            second = GT.ReadInt16(fs, 2, false);
            type = GT.ReadByte(fs);
            numValuesBase = GT.ReadInt16(fs, 2, false);

            if (type == 4) {
                numValuesLen = numValuesBase;
                values = new byte[numValuesLen];
            } else if (type == 6) {
                numValuesLen = numValuesBase * 3;
                values = new byte[numValuesLen];
            } else if(type == 7) {
                numValuesLen = numValuesBase * 4;
                values = new byte[numValuesLen];
            } else if (type == 8) {
                numValuesLen = 0;
                values = new byte[numValuesLen];
            } else {
                throw new System.Exception();
            }

            derivedfirstadd = first + numValuesBase;
            derivedfirstsubtract = first - numValuesBase;

            for (int i = 0; i < numValuesLen; i++)
                values[i] = GT.ReadByte(fs);
        }

        public override string ToString() {
            return first.ToString() + "," + second + " (" + numValuesBase + ")";
        }
    }
}
