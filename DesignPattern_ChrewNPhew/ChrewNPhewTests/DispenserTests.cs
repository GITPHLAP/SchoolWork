using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChrewNPhew;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChrewNPhew.Tests
{
    [TestClass()]
    public class DispenserTests
    {
        [TestMethod()]
        public void FillAllTest()
        {
            //arrange
            Dispenser d = new Dispenser();

            //act
            d.FillAll();


            int blueberrymin = (int)Math.Floor((d.GumList.Count / 100.0) * (int)GumColor.Blue);

            int blueberrymax = (int)Math.Ceiling((d.GumList.Count / 100.0) * (int)GumColor.Blue);

            Assert.AreEqual(55, d.GumList.Count);

            Assert.AreEqual(blueberrymin, 13);

            Assert.AreEqual(blueberrymax, 14);

        }

        [TestMethod()]
        public void FillTest()
        {

            //arrange 
            Dispenser d = new Dispenser();

            //act
            d.Fill(GumColor.Blue);

            Assert.IsTrue(d.GumList.Count > 0);

        }
    }
}