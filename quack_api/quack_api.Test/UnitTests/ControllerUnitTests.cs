using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
