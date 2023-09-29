using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;
using RichardSzalay.MockHttp;

namespace TeslaInventoryNet.Test
{
    [TestClass]
    public class UnitTest1
    {
        private TeslaInventory tesla = null;
        private MockHttpMessageHandler httpHandler = null;

        private static ILogger<TeslaInventory> CreateLogger()
        {
            return new Mock<ILogger<TeslaInventory>>().Object;
        }

        [TestInitialize]
        public void Initialize()
        {
            httpHandler = new MockHttpMessageHandler();
            var httpClient = httpHandler.ToHttpClient();
            httpClient.BaseAddress = new Uri("https://foo");

            tesla = new TeslaInventory(CreateLogger(), httpClient);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task Search_Invalid_Location()
        {
            await tesla.Search(new Location()
            {
                Language = "foo",
                Market = "foo"
            }, new SearchCriteria() { Model = "m3", Condition = "new"});
        }

        [TestMethod]
        public async Task Search_CA_Used_NotNull()
        {
            Assert.IsNotNull(await tesla.Search(Location.CA, new SearchCriteria() { Model = "m3", Condition = "used"}));
        }

        [TestMethod]
        public async Task Search_FR_Used_NotNull()
        {
            Assert.IsNotNull(await tesla.Search(Location.FR, new SearchCriteria() { Model = "m3", Condition = "used"}));
        }

        [TestMethod]
        public async Task Search_UK_Used_NotNull()
        {
            Assert.IsNotNull(await tesla.Search(Location.UK, new SearchCriteria() { Model = "m3", Condition = "used"}));
        }

        [TestMethod]
        public async Task Search_Invalid_Count()
        {
            Assert.IsNotNull(await tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "new", Count = 10000000}));
        }

        [TestMethod]
        public async Task Search_US_New_NotNull()
        {
            Assert.IsNotNull(await tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "new"}));
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
        public async Task Search_Invalid_Model()
        {
            Assert.IsNotNull(await tesla.Search(Location.US, new SearchCriteria() { Model = "foo", Condition = "used"}));
        }

        [TestMethod]
        public async Task Search_Invalid_Condition()
        {
            Assert.IsNotNull(await tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "foo"}));
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public async Task Search_Null_Response()
        {
            httpHandler.AddBackendDefinition(
                httpHandler.Fallback.Throw(new NotImplementedException()));

            await tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "foo"});
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task Search_Invalid_HTTP_Response()
        {
            httpHandler.AddBackendDefinition(
                httpHandler.Fallback.Respond(req => new HttpResponseMessage(HttpStatusCode.NotFound)));

            await tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "foo"});
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task Search_Invalid_API_Error()
        {
            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(new { Error = "Test error", Code = 100 }))
            };

            httpHandler.AddBackendDefinition(
                httpHandler.Fallback.Respond(req => resp));

            await tesla.Search(Location.US, new SearchCriteria() { Model = "m3", Condition = "foo"});
        }
    }
}