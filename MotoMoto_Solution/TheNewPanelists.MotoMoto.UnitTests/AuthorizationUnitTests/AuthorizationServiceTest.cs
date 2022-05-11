using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using Xunit;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    /// <summary>
    /// Contains unit testing for part flagging data access layer.
    /// </summary>
    public class AuthorizationServiceTest
    {

        /// <summary>
        /// Entity containing data store access for authorization functionality
        /// </summary>
        private readonly AuthorizationService authorizationService;

        /// <summary>
        /// Default constructor. Initializes data access for authorization
        /// </summary>
        public AuthorizationServiceTest()
        {
            authorizationService = new AuthorizationService();
        }

        /// <summary>
        /// Uses data access layer to get matching authorization result
        /// Test passes if count is 0.
        /// </summary>
        [Fact]
        public void CheckValidAuthorization()
        {
            bool result = authorizationService.CheckAuthorized("ROOT", "usageAnalysisDashboard");
            Assert.True(result);
        }

        /// <summary>
        /// Uses data access layer to get matching authorization result
        /// Test passes if count is 0.
        /// </summary>
        [Fact]
        public void CheckInvalidAuthorization()
        {
            bool result = authorizationService.CheckAuthorized("user1", "usageAnalysisDashboard");
            Assert.False(result);
        }
    }
}