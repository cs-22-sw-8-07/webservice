using Microsoft.VisualStudio.TestTools.UnitTesting;
using quack_api.RecommenderAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using quack_api.Models;
using quack_api.Controllers;
using Microsoft.Extensions.Options;
using quack_api.Enums;
using Microsoft.Extensions.Configuration;

namespace quack_api.Test.UnitTests
{
    [TestClass]
    public class RecommenderControllerUnitTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
               .Build();
            return config;
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_Success()
        {
            //Arrange
            var config = InitConfiguration();
            var settings = config.GetSection("RecommenderSettings").Get<RecommenderSettings>();
            var options = Options.Create(settings);
            RecommenderController recommenderController = new RecommenderController(options);

            //Act
            var result = await recommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsTrue(result.Value.IsSuccessful);
            Assert.IsInstanceOfType(result.Value.Result, typeof(PlaylistDTO));
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_PathToPythonExeNotFound()
        {
            //Arrange
            var config = InitConfiguration();
            var settings = config.GetSection("RecommenderSettings").Get<RecommenderSettings>();
            var options = Options.Create(settings);
            options.Value.RecommenderPath = "Wrong_path";
            RecommenderController recommenderController = new RecommenderController(options);
            int errorNo = (int)ResponseErrors.SomethingWentWrongInTheRecommender;

            //Act
            var result = await recommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }
    }
}
