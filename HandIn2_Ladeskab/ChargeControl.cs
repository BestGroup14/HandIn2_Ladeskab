using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIn2_Ladeskab
{
    public class ChargeControl : IChargeControl
    {
        private IUsbCharger _UsbCharger;
        private IDisplay _display;


        // tilføj eventhandler som tjekker opladning - får besked fra station control om at starte med at lade. Tjekekr for ny strømmåling. Eventhandler skla have if-sætning med værdier i tabellen. Hent CurrrentValue fra CurrentEventArgs inde i eventhandleren. 



        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            _UsbCharger = usbCharger;
            usbCharger.CurrentValueEvent += HandleCurrentValueEvent;
            _display = display;
        }


        public void HandleCurrentValueEvent(object obj, CurrentEventArgs e)
        {
            if (_UsbCharger.CurrentValue == 0)
            {
                _display.ShowMessage("---");
            }

            if (_UsbCharger.CurrentValue > 0 && _UsbCharger.CurrentValue <= 5)
            {
                _UsbCharger.StopCharge();
                _display.ShowMessage("Telefonen er fuldt opladet");
            }

            if (_UsbCharger.CurrentValue > 5 && _UsbCharger.CurrentValue <= 500)
            {
                _display.ShowMessage("Telefon lader op");
            }

            if (_UsbCharger.CurrentValue > 500)
            {
                _UsbCharger.StopCharge();
                _display.ShowMessage("Fjern straks telefonen");
            }

        }


        public bool IsConnected()
        {
            return _UsbCharger.Connected;
        }

        public void StartCharge()
        {
            _UsbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _UsbCharger.StopCharge();
        }
    }
}
