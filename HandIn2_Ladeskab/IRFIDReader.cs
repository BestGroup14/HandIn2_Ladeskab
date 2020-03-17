using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIn2_Ladeskab
{
    public interface IRFIDReader
    {
        //int RFIDNumber { get; set; }

        void OnRfidRead(int id);

        event EventHandler<RFIDReaderEventArgs> RFIDReaderEvent;
    }
}
