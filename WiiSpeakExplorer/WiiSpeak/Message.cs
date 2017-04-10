using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiiSpeakExplorer.WiiSpeak {
    class Message {

        public const byte Magic = 0x9A;
        public enum MessageType {
            SAMPLER_STATE = 0,
            SAMPLER_FREQ_A = 2,
            SAMPLER_FREQ_B = 3,
            SAMPLER_GAIN = 4,
            SAMPLER_MUTE = 12,
            EC_STATE = 20,
            SP_STATE = 56//,
            //__UNKNOWN__
        };

        public MessageType Type;
        public int Value1;
        public int Value2;
        public int SeqNum;

        public Message(byte[] data) {
            if (data.Length == 10) {
                if (Enum.IsDefined(typeof(MessageType), (int)data[1]))
                    Type = (MessageType)data[1];

                Value1 = BitConverter.ToUInt16(data, 2);
                Value2 = BitConverter.ToUInt16(data, 4);
                SeqNum = BitConverter.ToUInt16(data, 6);
            }
        }

    }
}
