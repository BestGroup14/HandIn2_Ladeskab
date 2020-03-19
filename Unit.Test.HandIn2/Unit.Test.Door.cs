using System;
using System.Text;
using System.Collections.Generic;
using HandIn2_Ladeskab;
using NSubstitute;
using NUnit.Framework;

namespace Unit.Test.HandIn2
{

    [TestFixture]
    public class TestDoor
    {
        private Door _uut;
        private Display _display;


        [SetUp]
        public void Setup()
        {
            _display = Substitute.For<Display>();
            _uut = new Door(_display);
        }


        [Test]
        public void TestLockDoorMethod()
        {
            _uut.LockDoor();
            _display.Received(1);
        }

        [Test]
        public void TestUnLockDoorMethod()
        {
            _uut.UnlockDoor();
            _display.Received(1);
        }
    }
}
