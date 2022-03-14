using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quack_api;
using System.Net.Http;

namespace quack_api.Test.IntegrationTests
{
    [TestClass]
    public class IntegrationTests
    {
        /// <summary>
        /// Sets up a local test server where the configuration is giving through the appsettings file.
        /// </summary>
        /// <returns>a test server</returns>
        private TestServer GetTestServer()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            var webHostBuilder =
                  new WebHostBuilder()
                  .UseConfiguration(configurationBuilder)
                        .UseEnvironment("production") // You can set the environment you want (development, staging, production)
                        .UseStartup<Startup>(); // Startup class of your web app project
            // Return test server
            return new TestServer(webHostBuilder);
        }

        [TestMethod]
        public async Task IssueController_GetIssueDetails_CorrectJSONResponse()
        {
            // Arrange
            using (var server = GetTestServer())
            using (var client = server.CreateClient())
            {
                // Act
                HttpResponseMessage responseMessage = await client.GetAsync("/Quack/Issue/GetIssueDetails?issueId=1");
                string content = await responseMessage.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.OK, responseMessage.StatusCode);
                Assert.AreEqual("{\"Result\":{\"Id\":1,\"CitizenId\":1,\"Description\":\"Der er en løs flise\",\"DateCreated\":\"2021-10-21T13:44:15\",\"Location\":{\"Latitude\":57.012218,\"Longitude\":9.99433},\"Address\":\"Alfred Nobels Vej 27, 9200 Aalborg, Danmark\",\"Category\":{\"Id\":3,\"Name\":\"Fortov\"},\"SubCategory\":{\"Id\":11,\"CategoryId\":3,\"Name\":\"Løse fliser\"},\"Municipality\":{\"Id\":1,\"Name\":\"Aalborg\"},\"IssueState\":{\"Id\":1,\"Name\":\"Oprettet\"},\"MunicipalityResponses\":[],\"IssueVerificationCitizenIds\":[2,7]},\"IsSuccessful\":true,\"ErrorNo\":0}",
                                 content);
            }
        }


    }
}
