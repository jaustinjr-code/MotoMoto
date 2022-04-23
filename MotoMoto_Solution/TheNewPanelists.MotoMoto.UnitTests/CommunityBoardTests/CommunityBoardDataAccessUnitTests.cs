using Xunit;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.UnitTests.CommunityBoardTests
{
    /** NOTE:
    *   These unit tests do not test the state of the database
    *   Thus the functions worth testing are SqlGenerator and
    *   EstablishMariaDBConnection. These tests are not effective
    *   for testing the integrity of data within the database,
    *   for that we need to use DBUnit or IKVM or integration
    *   testing tools.
    */
    public class CommunityBoardDataAccessUnitTests
    {
        public CommunityBoardDataAccessUnitTests()
        {

        }

        [Fact]
        public void IsLoadFeedDBConnectionEstablished()
        {
            // Arrange
            IContentDataAccess feedDataAccess = new LoadFeedDataAccess();
            // Act
            bool result = feedDataAccess.EstablishMariaDBConnection();
            // Assert
            Assert.True(result);
        }

        // Not a useful test as it uses mock data
        //[Fact]
        //public void IsLoadFeedFetchingPosts()
        //{
        //    // Given
        //IContentDataAccess feedDataAccess = new LoadFeedDataAccess();
        //IFeedModel model = new CommunityFeedModel();
        //model.feedName = "test";
        //    // When
        //IFeedEntity? entity = ((LoadFeedDataAccess)feedDataAccess).FetchAllPosts(model);
        //    // Then
        ////Assert.Throws(((LoadFeedDataAccess)feedDataAccess).FetchAllPosts(model));
        // Not Null fails a successful query with no recent posts so the IFeedEntity will be null
        //Assert.NotNull(entity);
        //}
    }
}