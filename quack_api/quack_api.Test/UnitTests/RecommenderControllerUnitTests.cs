using Microsoft.VisualStudio.TestTools.UnitTesting;
using quack_api.RecommenderAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using quack_api.Models;
using quack_api.Controllers;
using Microsoft.Extensions.Options;
using quack_api.Enums;
using quack_api.Test.Utilities;
using Microsoft.Extensions.Configuration;

namespace quack_api.Test.UnitTests
{
    [TestClass]
    public class RecommenderControllerUnitTests
    {
        IConfiguration Configuration { get; set; }

        public string ChangeFilenameInPath(string path, string filename)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            path = directoryInfo.Parent.FullName;

            return Path.Combine(path, filename);
        }

        [TestInitialize]
        public void Setup()
        {
            // Runs before each test.
            Configuration = GeneralUtil.InitConfiguration();

        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_Success()
        {
            //Arrange
            var settings = Configuration.GetSection("RecommenderSettings").Get<RecommenderSettings>();
            var options = Options.Create(settings);
            RecommenderController recommenderController = new RecommenderController(options);

            //Act
            var result = await recommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsTrue(result.Value.IsSuccessful);
            Assert.IsInstanceOfType(result.Value.Result, typeof(PlaylistDTO));
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_WrongRecommenderPath_RecommenderPathWrong()
        {
            //Arrange
            var settings = Configuration.GetSection("RecommenderSettings").Get<RecommenderSettings>();
            var options = Options.Create(settings);
            options.Value.RecommenderPath = "Wrong_path";
            RecommenderController recommenderController = new RecommenderController(options);
            int errorNo = (int)ResponseErrors.RecommenderPathWrong;

            //Act
            var result = await recommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_WrongPythonPath_PythonPathNotFound()
        {
            //Arrange
            var settings = Configuration.GetSection("RecommenderSettings").Get<RecommenderSettings>();
            var options = Options.Create(settings);
            options.Value.PythonPath = "Wrong_path";
            RecommenderController recommenderController = new RecommenderController(options);
            int errorNo = (int)ResponseErrors.PathNotFound;

            //Act
            var result = await recommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_NullPythonPath_PythonPathNull()
        {
            //Arrange
            var settings = Configuration.GetSection("RecommenderSettings").Get<RecommenderSettings>();
            var options = Options.Create(settings);
            options.Value.PythonPath = null;
            RecommenderController recommenderController = new RecommenderController(options);
            int errorNo = (int)ResponseErrors.PathNull;

            //Act
            var result = await recommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_PythonScriptReturnsAnEmptyString_ResultFromCommandlineEmpty()
        {
            //Arrange
            var settings = Configuration.GetSection("RecommenderSettings").Get<RecommenderSettings>();
            var options = Options.Create(settings);
            options.Value.RecommenderPath = ChangeFilenameInPath(options.Value.RecommenderPath, "mainEmpty.py");
            RecommenderController recommenderController = new RecommenderController(options);
            int errorNo = (int)ResponseErrors.ResultFromCommandlineEmpty;

            //Act
            var result = await recommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_PythonScriptReturnsAnExitCode_SomethingWentWrongInTheRecommender()
        {
            //Arrange
            var settings = Configuration.GetSection("RecommenderSettings").Get<RecommenderSettings>();
            var options = Options.Create(settings);
            options.Value.RecommenderPath = ChangeFilenameInPath(options.Value.RecommenderPath, "mainExitcode2.py");
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
