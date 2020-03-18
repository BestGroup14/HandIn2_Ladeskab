using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HandIn2_Ladeskab
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };


        // Her mangler flere member variable
        private LadeskabState _state;
        private IUsbCharger _charger;
        private int _oldId;
        private IDoor _door;
        private IRFIDReader _rfidReader;
        private IDisplay _display;
        private ILogFile _log;
        private int CurrentID { get; set; }
        

        // Her mangler constructor
        public StationControl(IDoor door, IRFIDReader rfidReader, IDisplay display, IUsbCharger charger, ILogFile log)
        {
            _door = door;
            _display = display;
            _charger = charger;
            _log = log;
            _door.DoorOpenedEvent += DoorOpened;
            _door.DoorClosedEvent += DoorClosed;
            _rfidReader = rfidReader;
            _rfidReader.RFIDReaderEvent += RfidDetected;
        }


        private void RfidDetected(Object obj, RFIDReaderEventArgs e)
        {
            CurrentID = e.RFID;
            RfidDetected(CurrentID);
        }

        //Skal kun starte og stoppe ladning gennem denne stationControl



        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        // Måske lave om, så vi kalder interface i stedet for 
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        
                        _log.LogDoorLocked(_oldId);

                        _display.ShowMessage("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.ShowMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                       
                        _log.LogDoorUnlocked(_oldId);

                        _display.ShowMessage("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.ShowMessage("Forkert RFID tag");
                    }

                    break;
            }
        }



        public void DoorOpened(Object obj, EventArgs e)
        {
            //switch states

            switch (_state)
            {
                case LadeskabState.Available:
                // tilslut telefon + doorOpen
                
                _display.ShowMessage("Tilslut telefon");
                _state = LadeskabState.DoorOpen;

                break;

                case LadeskabState.DoorOpen:
                    // ignore
                    break;

                case LadeskabState.Locked:
                    // ignore

                    break;

            }
        }


        public void DoorClosed(Object obj, EventArgs e)
        {
            //Switch states
            switch (_state)
            {
                case LadeskabState.Available:
                    // Ignore
                    break;

                case LadeskabState.DoorOpen: 
                    
                    _display.ShowMessage("Indlæs RFID");
                    _state = LadeskabState.Available;
                    break;

                case LadeskabState.Locked:
                    // Ignore
                    break;
            }
        }
    }
}
