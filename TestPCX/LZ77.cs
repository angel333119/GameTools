// https://raw.githubusercontent.com/jeffman/MOTHER-3-Funland/master/LZ77.cs

namespace GBA {
    class LZ77 {
        public static byte[] Decompress(byte[] data, int length) {
            byte[] output = new byte[length];
            int address = 0;

            int bPos = 0;
            while (bPos < length) {
                byte ch = data[address++];
                for (int i = 0; i < 8; i++) {
                    switch ((ch >> (7 - i)) & 1) {
                        case 0:

                            // Direct copy
                            if (bPos >= length) break;
                            output[bPos++] = data[address++];
                            break;

                        case 1:

                            // Compression magic
                            int t = (data[address++] << 8);
                            t += data[address++];
                            int n = ((t >> 12) & 0xF) + 3;    // Number of bytes to copy
                            int o = (t & 0xFFF);

                            // Copy n bytes from bPos-o to the output
                            for (int j = 0; j < n; j++) {
                                if (bPos >= length) break;
                                if (bPos - o - 1 < 0) output[bPos] = 0xFF;
                                else output[bPos] = output[bPos - o - 1];
                                bPos++;
                            }

                            break;

                        default:
                            break;
                    }
                }
            }

            return output;
        }
    }
}