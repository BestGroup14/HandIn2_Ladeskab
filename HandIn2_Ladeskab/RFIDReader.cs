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
        private int _oldRFIDNumber; 

        public event EventHandler<RFIDReaderEventArgs> RFIDReaderEvent;

        public void OnRfidRead(int id)
        {
            if (id != _oldRFIDNumber)
            {
                DetectRFID(new RFIDReaderEventArgs{RFID = id});
                _oldRFIDNumber = id;
            }
        }

        protected virtual void DetectRFID(RFIDReaderEventArgs e)
        {
            RFIDReaderEvent?.Invoke(this,e);
        }
    }
}
