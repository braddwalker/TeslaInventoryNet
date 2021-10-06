using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Net;
using Newtonsoft.Json;

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
        [ExpectedException(typeof(Exception))]
        public void Search_Invalid_Location()
        {
            Assert.IsNotNull(tesla.Search(new Location()
            {
                Country = "foo",
                Language = "foo",
                Market = "foo",
                Region = "foo"
            }, new SearchCriteria() { Model = "m3", Condition = "new"}));
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

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Search_Null_Response()
        {
            var client = new Mock<IRestClient>();
            client.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns<IRestResponse>(null);

            tesla = new TeslaInventory(CreateLogger(), client.Object);   
            tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "foo"});
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Search_Invalid_HTTP_Response()
        {
            var response = new Mock<IRestResponse>();
            response.Setup(x => x.StatusCode).Returns(HttpStatusCode.NotFound);

            var client = new Mock<IRestClient>();
            client.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(response.Object);

            tesla = new TeslaInventory(CreateLogger(), client.Object);   
            tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "foo"});
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Search_Invalid_API_Error()
        {
            var response = new Mock<IRestResponse>();
            response.Setup(x => x.StatusCode).Returns(HttpStatusCode.OK);
            response.Setup(x => x.Content).Returns(JsonConvert.SerializeObject(new { Error = "Test error", Code = 100}));

            var client = new Mock<IRestClient>();
            client.Setup(x => x.Execute(It.IsAny<IRestRequest>())).Returns(response.Object);

            tesla = new TeslaInventory(CreateLogger(), client.Object);   
            tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "foo"});
        }
    }
}
