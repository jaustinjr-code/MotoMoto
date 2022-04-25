using Xunit;
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
        public void IsNullOrEmptyRequestCorrectForNullInput()
        {
            // Arrange
            object? input = null;
            IContentManager manager = new FeedManager();
            // Act
            bool result = manager.IsNullOrEmptyRequest(input);
            // Assert
            Assert.True(result);
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
        public void IsNullOrEmptyRequestCorrectForEmptyInput()
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
        public void testName()
        {
            // throw new NotImplementedException();
        }
    }
}