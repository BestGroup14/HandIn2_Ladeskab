using System;
using System.Text;
using System.Collections.Generic;
using HandIn2_Ladeskab;
using NSubstitute;
using NUnit.Framework;


namespace Unit.Test.HandIn2
{
    
    [TestFixture]
    public class TestDisplay
    {
        private StationControl _uut;
        private IDisplay _display;
        private IDoor _door;
        private IRFIDReader _rfidReader;
        private IUsbCharger _usbCharger;
        private ILogFile _logFile;

        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRFIDReader>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _logFile = Substitute.For<ILogFile>();

            _uut = new StationControl(_door,_rfidReader,_display,_usbCharger,_logFile);
        }


        //[Test]
        //public void TestMethod1()
        //{
        //    string testString = "Ladeskab låst";
        //    _door.LockDoor();

        //    Assert.That(_display.ShowMessage(testString));

            
        //    _display.ShowMessage(testString);

        //    Assert.That(testString, Is.EqualTo("Tilslut telefon"));

        //}


    }
}
