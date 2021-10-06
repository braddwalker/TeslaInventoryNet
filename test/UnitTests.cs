using Microsoft.VisualStudio.TestTools.UnitTesting;
using TeslaInventoryNet;
using Moq;
using Microsoft.Extensions.Logging;

namespace TeslaInventoryNet.Test
{
    [TestClass]
    public class UnitTest1
    {
        private TeslaInventory tesla = null;

        private ILogger<TeslaInventory> CreateLogger()
        {
            return new Mock<ILogger<TeslaInventory>>().Object;
        }

        [TestInitialize]
        public void Initialize()
        {
            tesla = new TeslaInventory(CreateLogger());
        }

        [TestMethod]
        public void Search_US_New_NotNull()
        {
            Assert.IsNotNull(tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "new"}));
        }

        [TestMethod]
        public void Search_US_Used_NotNull()
        {
            Assert.IsNotNull(tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "used"}));
        }

        [TestMethod]
        public void Search_US_New_Delegate_NotNull()
        {
            tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "new"}, (results) => {
                Assert.IsNotNull(results);
            });
        }

        [TestMethod]
        public void Search_Invalid_Model()
        {
            Assert.IsNotNull(tesla.Search(Location.US, new SearchCriteria() { Model = "foo", Condition = "used"}));
        }

        [TestMethod]
        public void Search_Invalid_Condition()
        {
            Assert.IsNotNull(tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "foo"}));
        }
    }
}
