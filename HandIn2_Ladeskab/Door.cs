using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandIn2_Ladeskab
{
    public class Door : IDoor
    {
        private IDisplay _display;

        public event EventHandler DoorOpenedEvent;
        public event EventHandler DoorClosedEvent;
        public Door(IDisplay display)
        {
            _display = display;
        }
        public void CloseDoor()
        { 
            DoorClosedEvent?.Invoke(this,new EventArgs());
        }

        public void LockDoor()
        {
            _display.ShowMessage("Ladeskab låst");
        }

        public void OpenDoor()
        {
            DoorOpenedEvent?.Invoke(this,new EventArgs());

        }
        public void UnlockDoor()
        {
            _display.ShowMessage("Døren er låst op");
        }
    }
}
