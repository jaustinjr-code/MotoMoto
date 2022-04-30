using Xunit;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;

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
}