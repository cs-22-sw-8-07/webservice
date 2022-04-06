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
using quack_api.Models;

namespace quack_api.Test.IntegrationTests
{
    [TestClass]
    public class IntegrationTests
    {
        public IConfiguration Configuration { get; set; }
        /// <summary>
        /// Sets up a local test server where the configuration is giving through the appsettings file.
        /// </summary>
        /// <returns>a test server</returns>
        private TestServer GetTestServer()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            var webHostBuilder =
                  new WebHostBuilder()
                  .UseConfiguration(Configuration)
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
                HttpResponseMessage responseMessage = await client.GetAsync("/Quack/Recommender/GetPlaylist");
                string content = await responseMessage.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.OK, responseMessage.StatusCode);
                Assert.IsTrue(content.StartsWith("{\"is_successful\":true,\"error_no\":0}"));
            }
        }


    }
}
