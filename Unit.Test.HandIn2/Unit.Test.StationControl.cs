using System;
using System.Text;
using System.Collections.Generic;
using HandIn2_Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Unit.Test.HandIn2
{
    
    [TestFixture]
    public class TestStationControl
    {
        private IDoor _door;
        private IRFIDReader _rfidReader;
        private IDisplay _display;
        private IUsbCharger _usbCharger;
        private ILogFile _logFile;
        private StationControl _uut;

        [SetUp]
        public void SetUp()
        {
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRFIDReader>();
            _display = Substitute.For<IDisplay>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _logFile = Substitute.For<ILogFile>();
            _uut = new StationControl(_door,_rfidReader,_display,_usbCharger,_logFile);
        }

        [Test]
        public void TestStationControlAvailableConnectedLockDoor()
        {
            _usbCharger.Connected.Returns(true);
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _door.Received(1).LockDoor();
        }

        [Test]
        public void TestStationControlAvailableConnectedStartCharge()
        {
            _usbCharger.Connected.Returns(true);
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _usbCharger.Received(1).StartCharge();
        }

        [Test]
        public void TestStationControlAvailableConnectedLogDoorLocked()
        {
            _usbCharger.Connected.Returns(true);
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _logFile.Received(1).LogDoorLocked(12);
        }

        [Test]
        public void TestStationControlAvailableConnectedShowMessage()
        {
            _usbCharger.Connected.Returns(true);
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _display.Received(1).ShowMessage("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        [Test]
        public void TestStationControlAvailableNotConnectedShowMessage()
        {
            _usbCharger.Connected.Returns(false);
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _display.Received(1).ShowMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
        }

        [Test]
        public void TestStationControlLockedIdIdStopCharge()
        {
            _usbCharger.Connected.Returns(true);
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _usbCharger.Received(1).StopCharge();
        }

        [Test]
        public void TestStationControlLockedIdIdUnlockDoor()
        {
            _usbCharger.Connected.Returns(true);
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _door.Received(1).UnlockDoor();
        }

        [Test]
        public void TestStationControlLockedIdIdLogDoorUnlocked()
        {
            _usbCharger.Connected.Returns(true);
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _logFile.Received(1).LogDoorUnlocked(12);
        }

        [Test]
        public void TestStationControlLockedIdIdShowMessage()
        {
            _usbCharger.Connected.Returns(true);
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _display.Received(1).ShowMessage("Tag din telefon ud af skabet og luk døren");
        }

        [Test]
        public void TestStationControlLockedIdNotIdShowMessage()
        {
            _usbCharger.Connected.Returns(true);
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 12 });
            _rfidReader.RFIDReaderEvent += Raise.EventWith(this, new RFIDReaderEventArgs() { RFID = 14 });
            _display.Received(1).ShowMessage("Forkert RFID tag");
        }

        [Test]
        public void TestStationControlDoorOpenAvailableShowMessage()
        {
            _door.DoorOpenedEvent += Raise.EventWith(this, new EventArgs());
            _display.Received(1).ShowMessage("Tilslut telefon");
        }

        [Test]
        public void TestStationControlDoorClosedDoorOpenShowMessage()
        {
            _door.DoorOpenedEvent += Raise.EventWith(this, new EventArgs());
            _door.DoorClosedEvent += Raise.EventWith(this, new EventArgs());
            _display.Received(1).ShowMessage("Indlæs RFID");
        }
    }
}
