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
            var result = await recommenderController.GetPlaylist("test", QuackLocationType.beach, Array.Empty<int>());

            //Assert
            Assert.IsTrue(result.Value.IsSuccessful);
            Assert.IsInstanceOfType(result.Value.Result, typeof(PlaylistDTO));
            Assert.AreEqual("placeholder", result.Value.Result.Id);
            Assert.AreEqual("placeholder", result.Value.Result.LocationType);
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_Error_ProcessCouldNotStart()
        {
            //Arrange
            var options = Options.Create(new RecommenderSettings());
            RecommenderController recommenderController = new RecommenderController(options, new MockRecommenderService());
            int errorNo = (int)ResponseErrors.ProcessCouldNotStart;

            //Act
            var result = await recommenderController.GetPlaylist("ProcessCouldNotStart", QuackLocationType.unknown, Array.Empty<int>());

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(errorNo, result.Value.ErrorNo);
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_Error_AnExceptionOccurredInTheSAL()
        {
            //Arrange
            var options = Options.Create(new RecommenderSettings());
            RecommenderController recommenderController = new RecommenderController(options, new MockRecommenderService());
            int errorNo = (int)ResponseErrors.AnExceptionOccurredInTheSAL;

            //Act
            var result = await recommenderController.GetPlaylist("AnExceptionOccurredInTheSAL", QuackLocationType.unknown, Array.Empty<int>());

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(errorNo, result.Value.ErrorNo);
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_Error_AnExceptionOccurredInAController()
        {
            //Arrange
            var options = Options.Create(new RecommenderSettings());
            RecommenderController recommenderController = new RecommenderController(options, new MockRecommenderService());
            int errorNo = (int)ResponseErrors.AnExceptionOccurredInAController;

            //Act
            var result = await recommenderController.GetPlaylist("AnExceptionOccurredInAController", QuackLocationType.unknown, Array.Empty<int>());

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(errorNo, result.Value.ErrorNo);
        }
    }
}
