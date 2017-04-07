using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GameTools;

namespace USBIPExplorer {
    public static class Protocol {

        public static int thing = 0;

        public static object Get(GTFS fs) {

            bool flip = true;
            int first = GT.ReadUInt16(fs, 2, flip);
            int second = GT.ReadUInt16(fs, 2, flip);

            if (second == 32773) {
                OP_REQ_DEVLIST op = new OP_REQ_DEVLIST();
                int status = GT.ReadInt32(fs, 4, flip);

                return op;
            } else if (second == 5) {
                OP_REP_DEVLIST op = new OP_REP_DEVLIST();
                int status = GT.ReadInt32(fs, 4, flip);
                int NumExport = GT.ReadInt32(fs, 4, flip);

                Device dev = new Device(fs);                
                for (int i = 0; i < 1; i++) { //dev.bNumInterfaces
                    byte bInterfaceClass = GT.ReadByte(fs);
                    byte bInterfaceSubClass = GT.ReadByte(fs);
                    byte bInterfaceProtocol = GT.ReadByte(fs);
                    byte padding = GT.ReadByte(fs);
                }

                return op;
            } else if (second == 32771) {
                OP_REQ_IMPORT op = new OP_REQ_IMPORT();
                int status = GT.ReadInt32(fs, 4, flip);
                string busid = GT.ReadASCII(fs, 32, flip);
                return op;
            } else if (second == 3) {
                OP_REP_IMPORT op = new OP_REP_IMPORT();
                int status = GT.ReadInt32(fs, 4, flip);
                Device dev = new Device(fs);
                return op;
            } else if(second == 1) {
                USBIP_CMD_SUBMIT op = new USBIP_CMD_SUBMIT();
                int seqnum = GT.ReadInt32(fs, 4, flip);
                int devid = GT.ReadInt32(fs, 4, flip);
                int direction = GT.ReadInt32(fs, 4, flip);
                int ep = GT.ReadInt32(fs, 4, flip);
                byte[] transfer_flags = GT.ReadBytes(fs, 4, flip);
                byte[] transfer_buffer_length = GT.ReadBytes(fs, 4, flip);
                byte[] start_frame = GT.ReadBytes(fs, 4, flip);
                int number_of_packets = GT.ReadInt32(fs, 4, flip);
                int interval = GT.ReadInt32(fs, 4, flip);

                //URB Setup
                byte bmRequestType = GT.ReadByte(fs);
                byte bRequest = GT.ReadByte(fs);
                int wValue = GT.ReadUInt16(fs, 2, false);
                int wIndex = GT.ReadUInt16(fs, 2, false);
                int wLength = GT.ReadUInt16(fs, 2, false);

                byte[] _remainingURB;
                if (fs.Position < fs.Length)
                    _remainingURB = GT.ReadBytes(fs, (int)(fs.Length - fs.Position), flip);

                return op;
            } else if (second == 3) {
                USBIP_RET_SUBMIT op = new USBIP_RET_SUBMIT();
                int seqnum = GT.ReadInt32(fs, 4, flip);
                int devid = GT.ReadInt32(fs, 4, flip);
                int direction = GT.ReadInt32(fs, 4, flip);
                int ep = GT.ReadInt32(fs, 4, flip);
                int status = GT.ReadInt32(fs, 4, flip);
                int actual_length = GT.ReadInt32(fs, 4, flip);
                byte[] start_frame = GT.ReadBytes(fs, 4, flip);
                int number_of_packets = GT.ReadInt32(fs, 4, flip);
                int error_count = GT.ReadInt32(fs, 4, flip);
                byte[] setup = GT.ReadBytes(fs, 8, flip);
                byte[] _data = GT.ReadBytes(fs, actual_length, flip);

                return op;
            }

            return null;
        }

    }
}
