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

namespace quack_api.Test.UnitTests
{
    [TestClass]
    public class RecommenderControllerUnitTests
    {


        class TestOptions : RecommenderSettings
        {

        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_Success()
        {
            //Arrange
            IOptions<TestOptions> testOptions = Options.Create(new TestOptions()
            {
                RecommenderConnection = @"C:\Users\Jeppe\Documents\P8\webservice\quack_api\quack_api.Test\Resources\Recommender_Test"
            });
            RecommenderController recommenderController = new RecommenderController(testOptions);

            //Act
            var result = await recommenderController.GetPlaylist("test", "test");

            //Assert
            Assert.IsTrue(result.Value.IsSuccessful);
            Assert.IsInstanceOfType(result.Value.Result, typeof(PlaylistDTO));
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_PathToPythonExeNotFound()
        {
            //Arrange
            IOptions<TestOptions> testOptions = Options.Create(new TestOptions()
            {
                RecommenderConnection = @"C:\Users\Jeppe\Documents\P8\wrong\"
            });
            RecommenderController recommenderController = new RecommenderController(testOptions);
            int errorNo = (int)ResponseErrors.PathToPythonExeNotFound;

            //Act
            var result = await recommenderController.GetPlaylist("test", "test");

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }
    }
}
