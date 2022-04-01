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
using Microsoft.Extensions.Options;

namespace quack_api.Test.UnitTests
{
    [TestClass]
    public class RecommenderUnitTests
    {
        [TestInitialize]
        public void Setup()
        {
            // Runs before each test.
        }

        [TestMethod]
        [TestCategory(nameof(RecommenderService.GetPlaylist))]
        public async Task UTEST()
        {

            var options = Options.Create(new RecommenderSettings());
            RecommenderController recommenderController = new RecommenderController(options, new MockRecommenderService());
            var response = await recommenderController.GetPlaylist("x", Enums.QuackLocationType.beach);
            Assert.IsNotNull(response);
        }
    }
}
