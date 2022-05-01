using Xunit;
using System;
using System.IO;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.UnitTests.CommunityBoardTests
{
    // This does not test Business Logic for Response methods
    public class CommunityBoardBusinessLayerUnitTests
    {
        public CommunityBoardBusinessLayerUnitTests()
        {

        }

        [Fact]
        public void IsNullOrEmptyRequestCorrectForNullInput_LoadFeed_ReturnTrue()
        {
            // Arrange
            object? input = null;
            IContentManager manager = new FeedManager();
            // Act
            bool result = manager.IsNullOrEmptyRequest(input);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmptyRequestCorrectForEmptyInput_LoadFeed_ReturnTrue()
        {
            // Arrange
            // This should be a request model, not a response
            IContentModel model = new CommunityFeedModel();
            ((CommunityFeedModel)((IFeedModel)model)).feedName = "";
            object? input = new LoadFeedRequestModel(((IFeedModel)model));
            IContentManager manager = new FeedManager();
            // Act
            bool result = manager.IsNullOrEmptyRequest(input);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsNullOrEmptyRequestCorrectForNullInput_Post_ReturnTrue()
        {
            // Arrange
            object? input = null;
            IContentManager manager = new PostManager();
            // Act
            bool result = manager.IsNullOrEmptyRequest(input);
            // Assert
            Assert.True(result);
        }

        // Commented this test because of the warnings
        // [Fact]
        // public void IsRequestInputCorrectForNullInput_Post_ReturnFalse()
        // {
        //     // Arrange
        //     IContentModel input = new FeedPostModel(null, null, null, null); // Null inputs give warnings
        //     IContentManager manager = new PostManager();
        //     // Act
        //     bool result = ((PostManager)manager).IsValidRequestForm(input);
        //     // Assert
        //     Assert.False(result);
        // }

        [Fact]
        public void IsRequestInputCorrectForEmptyInput_Post_ReturnFalse()
        {
            // Arrange
            IContentModel input = new FeedPostModel("", "", "", "");
            IContentManager manager = new PostManager();
            // Act
            bool result = ((PostManager)manager).IsValidRequestForm(input);
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsNullOrEmptyRequestCorrectForNullInput_Comment_ReturnTrue()
        {
            // Arrange
            object? input = null;
            IContentManager manager = new CommentManager();
            // Act
            bool result = manager.IsNullOrEmptyRequest(input);
            // Assert
            Assert.True(result);
        }

        // Commented this test because of the warnings
        // [Fact]
        // public void IsRequestInputCorrectForNullInput_Comment_ReturnFalse()
        // {
        //     // Arrange
        //     IContentModel input = new CommentPostModel(0, null, null); // Null inputs give warnings
        //     IContentManager manager = new CommentManager();
        //     // Act
        //     bool result = ((CommentManager)manager).IsValidRequestForm(input);
        //     // Assert
        //     Assert.False(result);
        // }

        [Fact]
        public void IsRequestInputCorrectForEmptyInput_Comment_ReturnFalse()
        {
            // Arrange
            IContentModel input = new CommentPostModel(0, "", "");
            IContentManager manager = new CommentManager();
            // Act
            bool result = ((CommentManager)manager).IsValidRequestForm(input);
            // Assert
            Assert.False(result);
        }

        // Not allowed to define null objects for Request model otherwise a warning is thrown
        //[Fact]
        //public void IsNullOrEmptyRequestCorrectForNoInput()
        //{
        //// Arrange
        //// This should be a request model, not a response
        //IContentModel model = new CommunityFeedModel();
        //// ((IFeedModel)model).feedName = "";
        //object? input = new LoadFeedRequestModel(((IFeedModel)model));
        //IContentManager manager = new FeedManager();
        //// Act
        //Action act = () => manager.IsNullOrEmptyRequest(input);
        //// Assert
        //Exception e = Assert.Throws<Exception>(act);
        //Assert.Equal("No Model Found", e.Message);
        //}



        [Fact]
        public void IsNullOrEmptyRequestCorrectForNullInput_PostDetails_ReturnTrue()
        {
            // Arrange
            IRequestModel input = new FetchPostDetailsRequestModel(0);
            IContentManager manager = new PostDetailsManager();

            // Act
            bool result = ((PostDetailsManager)manager).IsNullOrEmptyRequest(input);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsRequestCorrectForIncorrectInput_PostDetails_ThrowsInvalidDataException()
        {
            // Given
            IRequestModel input = new FetchPostDetailsRequestModel(0);
            IContentManager manager = new PostDetailsManager();
            // When
            Exception result = Assert.Throws<InvalidDataException>(() => ((PostDetailsManager)manager).IsRequestValid(input));
            // Then
            Assert.NotNull(result);
        }
    }
}