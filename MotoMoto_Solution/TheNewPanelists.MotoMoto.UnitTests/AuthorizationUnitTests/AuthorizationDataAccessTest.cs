using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using Xunit;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    /// <summary>
    /// Contains unit testing for part flagging data access layer.
    /// </summary>
    public class AuthorizationDataAccessTest
    {

        /// <summary>
        /// Entity containing data store access for authorization functionality
        /// </summary>
        private readonly AuthorizationDataAccess authorizationDataAccess;

        /// <summary>
        /// Default constructor. Initializes data access for authorization
        /// </summary>
        public AuthorizationDataAccessTest()
        {
            authorizationDataAccess = new AuthorizationDataAccess();
        }

        /// <summary>
        /// Uses data access layer to get matching authorization result
        /// Test passes if count is 0.
        /// </summary>
        [Fact]
        public void CheckValidAccessResult()
        {
            AuthorizationLevel authLevel = authorizationDataAccess.GetAuthorizationLevel("usageAnalysisDashboard", "ADMIN");
            Assert.True(authLevel.FeatureFound);
        }

        /// <summary>
        /// Uses data access layer to get nonexistent authorization result
        /// Test passes if count is 0.
        /// </summary>
        [Fact]
        public void CheckInvalidAccessResult()
        {
            AuthorizationLevel authLevel = authorizationDataAccess.GetAuthorizationLevel("usageAnalysisDashboard", "REGISTERED");
            Assert.False(authLevel.FeatureFound);
        }
    }
}