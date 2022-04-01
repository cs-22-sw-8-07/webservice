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
using quack_api.Interfaces;

namespace quack_api.Test.UnitTests
{
    [TestClass]
    public class RecommenderCntIntegrationTest
    {
        IConfiguration Configuration { get; set; }
        RecommenderController RecommenderController { get; set; }

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
            // Initialize instance of RecommenderController
            var settings = Configuration.GetSection("RecommenderSettings").Get<RecommenderSettings>();
            var options = Options.Create(settings);
            RecommenderController = new RecommenderController(options, new RecommenderService());
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_Success()
        {
            //Act
            var result = await RecommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsTrue(result.Value.IsSuccessful);
            Assert.IsInstanceOfType(result.Value.Result, typeof(PlaylistDTO));
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_WrongRecommenderPath_RecommenderPathWrong()
        {
            //Arrange
            RecommenderController.Options.Value.RecommenderPath = "Wrong_path";             
            int errorNo = (int)ResponseErrors.RecommenderPathWrong;

            //Act
            var result = await RecommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_WrongPythonPath_PythonPathNotFound()
        {
            //Arrange
            RecommenderController.Options.Value.PythonPath = "Wrong_path";
            int errorNo = (int)ResponseErrors.PathNotFound;

            //Act
            var result = await RecommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_NullPythonPath_PythonPathNull()
        {
            //Arrange
            RecommenderController.Options.Value.PythonPath = null;
            int errorNo = (int)ResponseErrors.PathNull;

            //Act
            var result = await RecommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_PythonScriptReturnsAnEmptyString_ResultFromCommandlineEmpty()
        {
            //Arrange
            RecommenderController.Options.Value.RecommenderPath = ChangeFilenameInPath(RecommenderController.Options.Value.RecommenderPath, "mainEmpty.py");            
            int errorNo = (int)ResponseErrors.ResultFromCommandlineEmpty;

            //Act
            var result = await RecommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }

        [TestMethod]
        public async Task RecommenderController_GetPlaylist_PythonScriptReturnsAnExitCode_SomethingWentWrongInTheRecommender()
        {
            //Arrange
            RecommenderController.Options.Value.RecommenderPath = ChangeFilenameInPath(RecommenderController.Options.Value.RecommenderPath, "mainExitcode2.py");            
            int errorNo = (int)ResponseErrors.SomethingWentWrongInTheRecommender;

            //Act
            var result = await RecommenderController.GetPlaylist("test", QuackLocationType.unknown);

            //Assert
            Assert.IsFalse(result.Value.IsSuccessful);
            Assert.AreEqual(result.Value.ErrorNo, errorNo);
        }
    }
}
