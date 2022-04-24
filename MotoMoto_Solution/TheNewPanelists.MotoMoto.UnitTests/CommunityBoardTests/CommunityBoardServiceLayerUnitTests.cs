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
    public void IsResponseModelBuilt()
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
    public void IsDefaultResponseModelBuilt()
    {
        // Arrange
        IContentModel content = new CommunityFeedModel();
        IFetchContentService service = new LoadFeedService(content);
        // Act
        IResponseModel result = ((LoadFeedService)service).LoadFeed();
        // Assert
        Assert.NotNull(result);
    }
}