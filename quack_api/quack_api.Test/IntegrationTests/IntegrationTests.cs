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
using System.Net.Http.Json;

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
                .AddJsonFile("appsettings.test.json")
                .Build();
            
            var webHostBuilder =
                  new WebHostBuilder()
                  .UseConfiguration(Configuration)
                        .UseEnvironment("development") // You can set the environment you want (development, staging, production)
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
                int[] offset = new int[] { 1, 2, 3 };
                JsonContent jsonContent = JsonContent.Create(offset);

                // Act
                HttpResponseMessage responseMessage = await client.PostAsync("/Quack/Recommender/GetPlaylist?accessToken=test&location=0", jsonContent);
                string content = await responseMessage.Content.ReadAsStringAsync();

                // Assert
                Assert.AreEqual(System.Net.HttpStatusCode.OK, responseMessage.StatusCode);
                Assert.IsTrue(content.StartsWith("{\"result\":{\"id\":\"placeholder\",\"location_type\":1,\"tracks\":[{\"id\":\"1bBGFHJHsdra2aHGsm8xUA\",\"name\":\"Forest Sleep and Relaxing Sounds, Pt. 01\",\"artist\":\"Sleepy Times, Natural Sound Makers, Nature Recordings\",\"image\":\"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"},{\"id\":\"03ufJ9eNwb42y1DwT7GsPG\",\"name\":\"Forest Sleep and Relaxing Sounds, Pt. 02\",\"artist\":\"Sleepy Times, Natural Sound Makers, Nature Recordings\",\"image\":\"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"},{\"id\":\"0CCwHttXxDVYqVNz9mpu1F\",\"name\":\"Forest Sleep and Relaxing Sounds, Pt. 03\",\"artist\":\"Sleepy Times, Natural Sound Makers, Nature Recordings\",\"image\":\"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"},{\"id\":\"2luBogOgg1wldL8RY3JLy9\",\"name\":\"Forest Sleep and Relaxing Sounds, Pt. 04\",\"artist\":\"Sleepy Times, Natural Sound Makers, Nature Recordings\",\"image\":\"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"},{\"id\":\"71f3qaOe4qiGzZlAijDLmZ\",\"name\":\"Forest Sleep and Relaxing Sounds, Pt. 05\",\"artist\":\"Sleepy Times, Natural Sound Makers, Nature Recordings\",\"image\":\"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"},{\"id\":\"6jTdKPbzzW5ijCxkNfF2sV\",\"name\":\"Forest Sleep and Relaxing Sounds, Pt. 06\",\"artist\":\"Sleepy Times, Natural Sound Makers, Nature Recordings\",\"image\":\"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"},{\"id\":\"15sjWmSubcmIqpyb10d6GW\",\"name\":\"Forest Sleep and Relaxing Sounds, Pt. 07\",\"artist\":\"Sleepy Times, Natural Sound Makers, Nature Recordings\",\"image\":\"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"},{\"id\":\"70OqgjeXXuEr2miAVvzAB1\",\"name\":\"Forest Sleep and Relaxing Sounds, Pt. 08\",\"artist\":\"Sleepy Times, Natural Sound Makers, Nature Recordings\",\"image\":\"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"},{\"id\":\"2SBZynW1qWonWmwNuXupge\",\"name\":\"Forest Sleep and Relaxing Sounds, Pt. 09\",\"artist\":\"Sleepy Times, Natural Sound Makers, Nature Recordings\",\"image\":\"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"},{\"id\":\"0XTvIIjRHzdQfiZhta2FUN\",\"name\":\"Forest Sleep and Relaxing Sounds, Pt. 10\",\"artist\":\"Sleepy Times, Natural Sound Makers, Nature Recordings\",\"image\":\"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"}],\"offset\":2},\"is_successful\":true,\"error_no\":0}"));
            }
        }
    }
}
