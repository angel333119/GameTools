using GameTools;

namespace GameTools2.Game.RunawayNDSSPR {
    public class RunawayPackedImage {
        public int first;
        public int second;
        public int numValues;
        public byte[] values;

        public int derivedfirstadd;
        public int derivedfirstsubtract;

        public RunawayPackedImage(GTFS fs) {
            first = GT.ReadInt16(fs, 2, false);
            second = GT.ReadInt16(fs, 2, false);
            numValues = GT.ReadInt16(fs, 2, false);
            values = new byte[numValues];

            derivedfirstadd = first + numValues;
            derivedfirstsubtract = first - numValues;

            for (int i = 0; i < numValues; i++)
                values[i] = GT.ReadByte(fs);
        }

        public override string ToString() {
            return first.ToString() + "," + second + " (" + numValues + ")";
        }
    }
}