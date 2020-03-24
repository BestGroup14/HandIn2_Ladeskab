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
        private IUsbCharger _usbCharger;
        private IDisplay _display;

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_usbCharger,_display);
        }

        [Test]
        public void TestIsConnectedMethodTrue()
        {
            _usbCharger.Connected.Returns(true);
            Assert.That(_uut.IsConnected(), Is.True);
        }

        [Test]
        public void TestStartChargeMethod()
        {
            _uut.StartCharge();
            _usbCharger.Received(1).StartCharge();
        }

        [Test]
        public void TestStopChargeMethod()
        {
            _uut.StopCharge();
            _usbCharger.Received(1).StopCharge();
        }

        [Test]
        public void TestHandleCurrentValueEventMethodCurrent0ShowMessage()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() {Current = 0});
            _display.Received(1).ShowMessage("---");
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        public void TestHandleCurrentValueEventMethodCurrent1StopCharge(int current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = current });
            _usbCharger.Received(1).StopCharge();
        }

        [TestCase(1)]
        [TestCase(4)]
        [TestCase(5)]
        public void TestHandleCurrentValueEventMethodCurrent1ShowMessage(int current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = current });
            _display.Received(1).ShowMessage("Telefonen er fuldt opladet");
        }

        [TestCase(6)]
        [TestCase(499)]
        [TestCase(500)]
        public void TestHandleCurrentValueEventMethodCurrent6499500ShowMessage(int current)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = current });
            _display.Received(1).ShowMessage("Telefon lader op");
        }

        [Test]
        public void TestHandleCurrentValueEventMethodCurrent501ShowMessage()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 501 });
            _display.Received(1).ShowMessage("Fjern straks telefonen");
        }

        [Test]
        public void TestHandleCurrentValueEventMethodCurrent501StopCharge()
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(this, new CurrentEventArgs() { Current = 501 });
            _usbCharger.Received(1).StopCharge();
        }


    }
}
