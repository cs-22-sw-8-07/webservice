using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using quack_api.Test.Utilities;
using System;
using quack_api.RecommenderAccessLayer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using quack_api.Test.Mocks;
using quack_api.Controllers;
using quack_api.Models;
using quack_api.Enums;
using Microsoft.Extensions.Options;

namespace quack_api.Test.UnitTests
{
    [TestClass]
    public class RecommenderUnitTests
    {
        [TestMethod]
        [TestCategory(nameof(RecommenderService.GetPlaylist))]
        public async Task RecommenderController_GetPlaylist_Success()
        {
            //Arrange
            var options = Options.Create(new RecommenderSettings());
            RecommenderController recommenderController = new RecommenderController(options, new MockRecommenderService());

            //Act
            var result = await recommenderController.GetPlaylist("test", QuackLocationType.beach);

            //Assert
            Assert.IsTrue(result.Value.IsSuccessful);
            Assert.IsInstanceOfType(result.Value.Result, typeof(PlaylistDTO));
        }
    }
}
