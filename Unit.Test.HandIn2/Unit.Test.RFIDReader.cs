using System;
using System.Text;
using System.Collections.Generic;
using HandIn2_Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Unit.Test.HandIn2
{
    
    [TestFixture]
    public class TestRFIDReader
    {
        private RFIDReader _uut;
        private RFIDReaderEventArgs _revievedEventArgs;

        [SetUp]
        public void Setup()
        {
            _revievedEventArgs = null;
            _uut = new RFIDReader();
            _uut.OnRfidRead(10);

            _uut.RFIDReaderEvent += (o, args) => { _revievedEventArgs = args; };
        }


        [Test]
        public void Set_recievedRFID_20()
        {
            _uut.OnRfidRead(20);
            Assert.That(_revievedEventArgs.RFID, Is.EqualTo(20));
        }

        [Test]
        public void Set_recievedRFID_notnull()
        {
            _uut.OnRfidRead(20);
            Assert.That(_revievedEventArgs, Is.Not.Null);
        }
    }
}
