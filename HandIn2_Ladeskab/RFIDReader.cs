using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIn2_Ladeskab
{
    public class RFIDReaderEventArgs : EventArgs
    {
        public int RFID { get; set; }
    }

    public class RFIDReader : IRFIDReader
    {
        public event EventHandler<RFIDReaderEventArgs> RFIDReaderEvent;
        public void OnRfidRead(int id)
        {
            DetectRFID(new RFIDReaderEventArgs{RFID = id});
        }
        protected virtual void DetectRFID(RFIDReaderEventArgs e)
        {
            RFIDReaderEvent?.Invoke(this,e);
        }
    }
}
