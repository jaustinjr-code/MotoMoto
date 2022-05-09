using Xunit;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.UnitTests.CommunityBoardTests
{
    public class CommunityBoardServiceLayerUnitTests
    {
        public CommunityBoardServiceLayerUnitTests()
        {

        }

        // This test verifies the correct response exists
        // I cannot test if the response model contains correct output
        // because that would require comparing against dummy data, instead
        // I used System.Diagnostics to write the contents with Debug.WriteLine
        [Fact]
        public void IsResponseModelBuilt_LoadFeed_NotNull()
        {
            // Arrange
            IContentModel content = new CommunityFeedModel();
            ((CommunityFeedModel)content).feedName = "test";
            IFetchContentService service = new LoadFeedService(content);
            // Act
            IResponseModel result = ((LoadFeedService)service).LoadFeed();
            // Assert
            Assert.NotNull(result);
        }

        // This test verifies that exceptions are being handled and return default objects
        // I cannot test if the response model contains correct output
        // because that would require comparing against dummy data, instead
        // I used System.Diagnostics to write the contents with Debug.WriteLine
        [Fact]
        public void IsDefaultResponseModelBuilt_LoadFeed_NotNull()
        {
            // Arrange
            IContentModel content = new CommunityFeedModel();
            IFetchContentService service = new LoadFeedService(content);
            // Act
            IResponseModel result = ((LoadFeedService)service).LoadFeed();
            // Assert
            Assert.NotNull(result);
        }

        // This test checks if the BuildExceptionResponse method builds the response
        // Useful for analyzing and creating client-friendly exceptions
        // Exception should come from the specific database operation, for example, FetchAllPosts
        // If the exception originates from a different operation then something is wrong
        // NOTE: this test does not check the origin of the Exception
        [Fact]
        public void IsExceptionResponseModelBuilt_LoadFeed_NotNull()
        {
            // Arrange
            IContentModel content = new CommunityFeedModel();
            ((CommunityFeedModel)content).feedName = "unknown";
            IFetchContentService service = new LoadFeedService(content);
            // Act
            IResponseModel result = ((LoadFeedService)service).LoadFeed();
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IsResponseModelBuilt_FetchUpvotePost_NotNull()
        {
            // Arrange
            IRequestModel post = new FetchPostDetailsRequestModel(1);
            IResponseService service = new FetchUpvotesService(post);
            // Act
            IResponseModel result = ((FetchUpvotesService)service).FetchPostUpvotes();
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IsDefaultResponseModelBuilt_FetchUpvotePost_NotNull()
        {
            // Arrange
            IRequestModel post = new FetchPostDetailsRequestModel(0); // Invalid Input
            IResponseService service = new FetchUpvotesService(post);
            // Act
            IResponseModel result = ((FetchUpvotesService)service).FetchPostUpvotes();
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IsExceptionResponseModelBuilt_FetchUpvotePost_NotNull()
        {
            // Arrange
            IRequestModel post = new FetchPostDetailsRequestModel(20); // Assumes Post id 20 doesn't exist
            IResponseService service = new FetchUpvotesService(post);
            // Act
            IResponseModel result = ((FetchUpvotesService)service).FetchPostUpvotes();
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IsResponseModelBuilt_FetchUpvoteComment_NotNull()
        {
            // Arrange
            IRequestModel comment = new FetchPostDetailsRequestModel(1);
            IResponseService service = new FetchUpvotesService(comment);
            // Act
            IResponseModel result = ((FetchUpvotesService)service).FetchCommentUpvotes();
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IsDefaultResponseModelBuilt_FetchUpvoteComment_NotNull()
        {
            // Arrange
            IRequestModel comment = new FetchPostDetailsRequestModel(0); // Invalid Input
            IResponseService service = new FetchUpvotesService(comment);
            // Act
            IResponseModel result = ((FetchUpvotesService)service).FetchCommentUpvotes();
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void IsExceptionResponseModelBuilt_FetchUpvoteComment_NotNull()
        {
            // Arrange
            IRequestModel comment = new FetchPostDetailsRequestModel(20); // Assumes Post id 20 doesn't exist
            IResponseService service = new FetchUpvotesService(comment);
            // Act
            IResponseModel result = ((FetchUpvotesService)service).FetchCommentUpvotes();
            // Assert
            Assert.NotNull(result);
        }
    }
}