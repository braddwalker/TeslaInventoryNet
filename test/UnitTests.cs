using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeslaInventoryNet;

namespace TeslaInventoryNet.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Search_US_New_NotNull()
        {
            Assert.IsNotNull(TeslaInventory.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "new"}));
        }

        [TestMethod]
        public void Search_US_Used_NotNull()
        {
            Assert.IsNotNull(TeslaInventory.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "used"}));
        }

        [TestMethod]
        public void Search_Invalid_Model()
        {
            Assert.IsNotNull(TeslaInventory.Search(Location.US, new SearchCriteria() { Model = "foo", Condition = "used"}));
        }

        [TestMethod]
        public void Search_Invalid_Condition()
        {
            Assert.IsNotNull(TeslaInventory.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "foo"}));
        }
    }
}
