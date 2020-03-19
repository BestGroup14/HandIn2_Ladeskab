using System;
using System.Text;
using System.Collections.Generic;
using HandIn2_Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Unit.Test.HandIn2
{
    [TestFixture]
    public class TestChargeControl
    {
        private ChargeControl _uut;
        private UsbChargerSimulator _usbCharger;
        private Display _display;

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<UsbChargerSimulator>();
            _display = Substitute.For<Display>();
            _uut = new ChargeControl(_usbCharger,_display);
        }

        [Test]
        public void TestIsConnectedMethod()
        {
            Assert.That(_uut.IsConnected, Is.True);
        }

        [Test]
        public void TestStartChargeMethod()
        {
            _uut.StartCharge();
            _usbCharger.Received(1);
        }

        [Test]
        public void TestStopChargeMethod()
        {
            _uut.StopCharge();
            _usbCharger.Received(1);
        }


    }
}
