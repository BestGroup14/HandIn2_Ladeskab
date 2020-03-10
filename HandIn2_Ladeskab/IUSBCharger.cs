using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIn2_Ladeskab
{
    public interface IUSBCharger
    {
        event EventHandler<CurrentEventArgs> CurrentValueEven;

        double CurrentValue { get; set; }

        bool Connected { get; set; }

        void StartCharge();

        void StopCharge();
    }
}
