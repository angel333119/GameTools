using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiiSpeakExplorer {

    namespace USB {
        class URBWindows : URB {

            public URBWindows(GTFS fs, int remlen = 0) {
                bool flip = false;

                //URB
                int USBPcap_pseudoheader_length = GT.ReadUInt16(fs, 2, flip);
                id = GT.ReadBytes(fs, 8, flip); //ulong
                int IRPstatus = GT.ReadInt32(fs, 4, flip);
                int URBfunction = GT.ReadInt16(fs, 2, flip);
                byte IRPinfo = GT.ReadByte(fs);
                bus_id = GT.ReadUInt16(fs, 2, flip);
                device_address = GT.ReadInt16(fs, 2, flip);
                endpoint_number = GT.ReadByte(fs);
                transfer_type = (TransferType)GT.ReadByte(fs);
                data_len = urb_len = GT.ReadUInt32(fs, 4, flip);
                remlen -= 27;

                if (transfer_type == TransferType.Control) {
                    byte stage = GT.ReadByte(fs);
                    remlen -= 1;
                    if (urb_len == 8) {
                        //URB Setup
                        bmRequestType = GT.ReadByte(fs);
                        bRequest = GT.ReadByte(fs);
                        wValue = GT.ReadUInt16(fs, 2, flip);
                        wIndex = GT.ReadUInt16(fs, 2, flip);
                        wLength = GT.ReadUInt16(fs, 2, flip);
                        remlen -= 8;
                    } else
                        remaining = GT.ReadBytes(fs, remlen, flip);
                } else if(transfer_type == TransferType.Isochronous) {
                    int iso_start = GT.ReadInt32(fs, 4, flip);
                    numISOdesc = GT.ReadInt32(fs, 4, flip);
                    int iso_error = GT.ReadInt32(fs, 4, flip);
                    remlen -= 12;

                    if (numISOdesc > 0) {
                        listIsodesc = new List<isodesc>();
                        for (int i = 0; i < numISOdesc; i++) {
                            isodesc desc = new isodesc(fs, false);
                            listIsodesc.Add(desc);
                        }

                        remlen -= numISOdesc * 12;
                        remaining = GT.ReadBytes(fs, remlen, flip);

                        if (remlen > 0) {
                            for (int i = 0; i < numISOdesc; i++) {
                                listIsodesc[i].ReadData(remaining);
                            }
                        }
                    }
                } else
                    remaining = GT.ReadBytes(fs, remlen, flip); 
            }

        }
    }
}
