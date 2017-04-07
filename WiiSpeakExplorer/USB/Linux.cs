using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiiSpeakExplorer {

    namespace USB {
        class URBLinux : URB {

            public URBLinux(GTFS fs, int remlen = 0) {
                bool flip = false;

                //URB
                id = GT.ReadBytes(fs, 8, flip); //ulong
                event_type = GT.ReadByte(fs);
                transfer_type = (TransferType)GT.ReadByte(fs);
                endpoint_number = GT.ReadByte(fs);
                device_address = GT.ReadByte(fs);
                bus_id = GT.ReadUInt16(fs, 2, flip);
                setup_flag = GT.ReadByte(fs); //char
                data_flag = GT.ReadByte(fs); //char
                ts_sec = GT.ReadBytes(fs, 8, flip); //long
                ts_usec = GT.ReadInt32(fs, 4, flip);
                status = GT.ReadInt32(fs, 4, flip);
                urb_len = GT.ReadUInt32(fs, 4, flip);
                data_len = GT.ReadUInt32(fs, 4, flip);
                remlen -= 40;

                //URB Setup
                bmRequestType = GT.ReadByte(fs);
                bRequest = GT.ReadByte(fs);
                wValue = GT.ReadUInt16(fs, 2, flip);
                wIndex = GT.ReadUInt16(fs, 2, flip);
                wLength = GT.ReadUInt16(fs, 2, flip);
                remlen -= 8;

                interval = GT.ReadInt32(fs, 4, flip);
                startFrame = GT.ReadInt32(fs, 4, flip);
                copyTransferFlags = GT.ReadBytes(fs, 4, flip);
                numISOdesc = GT.ReadInt32(fs, 4, flip);
                remlen -= 16;

                if (numISOdesc > 0) {
                    listIsodesc = new List<isodesc>();
                    for (int i = 0; i < numISOdesc; i++) {
                        isodesc desc = new isodesc(fs, true);
                        listIsodesc.Add(desc);
                    }

                    remlen -= numISOdesc * 16;
                    remaining = GT.ReadBytes(fs, remlen, flip);

                    if (remlen > 0) {
                        for (int i = 0; i < numISOdesc; i++) {
                            listIsodesc[i].ReadData(remaining);
                        }
                    }
                } else
                    remaining = GT.ReadBytes(fs, remlen, flip);
            }

        }
    }
}
