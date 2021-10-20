using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeslaInventoryNet;
using System;

namespace TeslaInventoryNet.Test
{
    [TestClass]
    public class LocationUnitTests
    {
        [TestMethod]
        public void Location_US()
        {
            Assert.AreEqual(Location.US, Location.Parse("US"));
        }

        [TestMethod]
        public void Location_CA()
        {
            Assert.AreEqual(Location.CA, Location.Parse("CA"));
        }

        [TestMethod]
        public void Location_FR()
        {
            Assert.AreEqual(Location.FR, Location.Parse("FR"));
        }

        [TestMethod]
        public void Location_ES()
        {
            Assert.AreEqual(Location.ES, Location.Parse("ES"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Location_Invalid()
        {
            Location.Parse("foo");
        }
    }
}