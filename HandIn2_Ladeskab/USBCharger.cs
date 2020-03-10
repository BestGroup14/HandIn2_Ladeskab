using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIn2_Ladeskab
{
    public class USBCharger : IUSBCharger
    {
        public double CurrentValue { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool Connected { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler<CurrentEventArgs> CurrentValueEven;

        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }

        public void SimulateConnected(bool connected)
        {

        }

        public void SimulateOverload(bool overload)
        {

        }
    }
}
