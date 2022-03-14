using Microsoft.VisualStudio.TestTools.UnitTesting;
using quack_api.RecommenderAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using quack_api.Models;

namespace quack_api.Test.UnitTests
{
    [TestClass]
    public class ControllerUnitTests
    {
        /*[TestMethod]
        [TestCategory(nameof(CitizenController.GetListOfCitizens))]
        public async Task IssueController_GetListOfCitizens_Success()
        {
            // Arrange
            int municipalityId = 1;
            var contextFactory = new MockHiveContextFactory();
            CitizenController controller = new(contextFactory);

            // Act            
            var result = await controller.GetListOfCitizens(municipalityId, true);

            // Assert                
            using (var context = contextFactory.CreateDbContext())
            {
                Assert.IsTrue(result.Value.IsSuccessful);
                Assert.IsInstanceOfType(result.Value.Result, typeof(IEnumerable<CitizenDTO>));
            }
        }*/

        [TestMethod]
        public async Task RecommenderService_TestJson()
        {
           
            string result = @"{" +
                "\"result\": {" +
                    "\"id\": \"placeholder\"," +
                    "\"location_type\": \"placeholder\"," +
                    "\"tracks\": [" +
                        "{" +
                            "\"id\": \"1bBGFHJHsdra2aHGsm8xUA\"," +
                            "\"name\": \"Forest Sleep and Relaxing Sounds, Pt. 01\"," +
                            "\"artist\": \"Sleepy Times, Natural Sound Makers, Nature Recordings\"," +
                            "\"image\": \"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"" +
                        "}," +
                        "{" +
                            "\"id\": \"03ufJ9eNwb42y1DwT7GsPG\"," +
                            "\"name\": \"Forest Sleep and Relaxing Sounds, Pt. 02\"," +
                            "\"artist\": \"Sleepy Times, Natural Sound Makers, Nature Recordings\"," +
                            "\"image\": \"https://i.scdn.co/image/ab67616d00004851ab5198a87e702f0b2d6a6263\"" +
                        "}" +
                    "]" +
                "}," +
                "\"is_successful\": \"placeholder\"," +
                "\"error_no\": 0" +
            "}";

            var response = JsonSerializer.Deserialize<RecommenderResponse>(result);


        }
    }
}
