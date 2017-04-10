using GameTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WiiSpeakExplorer {

    namespace USB {
        public abstract class URB {
            public enum TransferType { Isochronous = 0x00, Control = 0x02, Bulk = 0x03 };
            public enum EndpointDirection { Out = 0, In = 1 };

            //URB
            public byte[] id, ts_sec;
            public TransferType transfer_type;
            public int device_address;
            public byte event_type, endpoint_number, setup_flag, data_flag;
            public uint bus_id, urb_len, data_len;
            public int ts_usec, status;
            //Extra
            public int interval, startFrame, numISOdesc;
            public byte[] copyTransferFlags;

            //URB Setup
            public byte bmRequestType, bRequest;
            public int wValue, wIndex, wLength;

            //Remaining
            public byte[] remaining;
            public List<isodesc> listIsodesc;

            public EndpointDirection GetEndpointDirection() {
                if (endpoint_number >= 0x80)
                    return EndpointDirection.In;
                return EndpointDirection.Out;
            }

            public override string ToString() {
                return string.Format("{0,11} {1,-3} bmReq:{2:X2} bReq:{3:X2} wVal:{4:X4} wInd:{5:X4} wLen:{6:X4}",
                    transfer_type.ToString(),
                    GetEndpointDirection().ToString(),
                    bmRequestType, bRequest, wValue, wIndex, wLength
                );
            }

        }
    }
}
