using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USBIPExplorer {
    class Device {

        public Device(GTFS fs) {
            bool flip = false;
            string DevicePath = GT.ReadASCII(fs, 256, flip);
            string DeviceBusID = GT.ReadASCII(fs, 32, flip);
            int busnum = GT.ReadInt32(fs, 4, flip);
            int devnum = GT.ReadInt32(fs, 4, flip);
            int speed = GT.ReadInt32(fs, 4, flip);
            int idVendor = GT.ReadInt16(fs, 2, flip);
            int idProduct = GT.ReadInt16(fs, 2, flip);
            int bcdDevice = GT.ReadInt16(fs, 2, flip);
            int bDeviceClass = GT.ReadByte(fs);
            int bDeviceSubClass = GT.ReadByte(fs);
            int bDeviceProtocol = GT.ReadByte(fs);
            int bConfigurationValue = GT.ReadByte(fs);
            int bNumConfigurations = GT.ReadByte(fs);
            int bNumInterfaces = GT.ReadByte(fs);
        }

    }
}
