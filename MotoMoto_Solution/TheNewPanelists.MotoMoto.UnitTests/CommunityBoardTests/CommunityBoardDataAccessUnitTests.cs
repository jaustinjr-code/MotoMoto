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
        public void IsLoadFeedDBConnectionEstablished_ReturnTrue()
        {
            // Arrange
            IDataAccess feedDataAccess = new LoadFeedDataAccess();
            // Act
            bool result = feedDataAccess.EstablishMariaDBConnection();
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsPostContentDBConnectionEstablished_ReturnTrue()
        {
            // Arrange
            IDataAccess postDataAccess = new PostContentDataAccess();
            // Act
            bool result = postDataAccess.EstablishMariaDBConnection();
            // Assert
            Assert.True(result);
        }

        //[Fact]
        //public void IsPuttingPost_ReturnTrue()
        //{
        //// Arrange
        //IPostModel model = new FeedPostModel("Third", "test", "testuser", "This is my third post!");
        //IDataAccess postDataAccess = new PostContentDataAccess();
        //// Act
        //bool result = ((PostContentDataAccess)postDataAccess).PutPost(model);
        //// Assert
        //Assert.True(result);
        //}

        //[Fact]
        //public void IsPuttingUpvotePost_ReturnTrue()
        //{
        //// Given
        //IInteractionModel model = new UpvotePostModel(4, "Third", "testuser");
        //IDataAccess postDataAccess = new PostContentDataAccess();
        //// When
        //bool result = ((PostContentDataAccess)postDataAccess).PutUpvotePost(model);
        //// Then
        //Assert.True(result);
        //}

        //[Fact]
        //public void IsPuttingComment_ReturnTrue()
        //{
        //// Arrange
        //IPostModel model = new CommentPostModel(1, null, "testuser", "This is my fourth comment!");
        //IDataAccess commentDataAccess = new CommentContentDataAccess();
        //// Act
        //bool result = ((CommentContentDataAccess)commentDataAccess).PutCommentPost(model);
        //// Assert
        //Assert.True(result);
        //}

        //[Fact]
        //public void IsPuttingUpvoteComment_ReturnTrue()
        //{
        //// Given
        //IInteractionModel model = new UpvoteCommentModel(1, 1, null, "testuser");
        //IDataAccess commentDataAccess = new CommentContentDataAccess();
        //// When
        //bool result = ((CommentContentDataAccess)commentDataAccess).PutUpvoteComment(model);
        //// Then
        //Assert.True(result);
        //}

        // Not a useful test as it uses mock data
        //[Fact]
        //public void IsLoadFeedFetchingAllPosts()
        //{
        //// Given
        //IDataAccess feedDataAccess = new LoadFeedDataAccess();
        //IFeedModel model = new CommunityFeedModel();
        //model.feedName = "test";
        //// When
        //IFeedEntity? entity = ((LoadFeedDataAccess)feedDataAccess).FetchAllPosts(model);
        //// Then
        ////Assert.Throws(((LoadFeedDataAccess)feedDataAccess).FetchAllPosts(model));
        //// Not Null fails a successful query with no recent posts so the IFeedEntity will be null
        //Assert.NotNull(entity);
        //}

        //[Fact]
        //public void IsLoadFeedFetchingPost()
        //{
        //// Given
        //IDataAccess postDataAccess = new LoadFeedDataAccess();
        //IPostModel model = new FeedPostModel(1);
        //// When
        //IPostEntity? entity = ((LoadFeedDataAccess)postDataAccess).FetchPost(model);
        //// Then
        //Assert.NotNull(entity);
        //}
    }
}