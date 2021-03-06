﻿using System;
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

        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            _UsbCharger = usbCharger;
            usbCharger.CurrentValueEvent += HandleCurrentValueEvent;
            _display = display;
        }

        public void HandleCurrentValueEvent(object obj, CurrentEventArgs e)
        {
            if (e.Current == 0)
            {
                _display.ShowMessage("---");
            }

            else if (e.Current > 0 && e.Current <= 5)
            {
                _UsbCharger.StopCharge();
                _display.ShowMessage("Telefonen er fuldt opladet");
            }

            else if (e.Current > 5 && e.Current <= 500)
            {
                _display.ShowMessage("Telefon lader op");
            }

            else if (e.Current > 500)
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
