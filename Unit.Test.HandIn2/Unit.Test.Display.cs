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
        private Display _uut;


        [SetUp]
        public void Setup()
        {
            _uut = Substitute.For<Display>();
        }


        [Test]
        public void string_check_Tilsluttelefon()
        {
            _uut.Received(1).ShowMessage("Tilslut telefon");
        }


    }
}
