using Xunit;
using TheNewPanelists.ServiceLayer.UsageAnalysisDashboard;

public class CommunityBoardAnalyticServiceTest
{
    [Fact]
    public void IsGetAnalyticsReturned()
    {
        // Given
        IAnalysisService service = new CommunityBoardAnalyticService();
        // When
        IList<IList<IDictionary<string, string>>?> refinedData = ((CommunityBoardAnalyticService)service).GetAnalytics();
        // Then
        Assert.NotNull(refinedData);
        // Assert.True(!refinedData.Contains(null));
        // Assert.NotEmpty(refinedData);
        // Assert.NotNull(refinedData.ElementAt(0));
        // Assert.NotNull(refinedData.ElementAt(0).ElementAt(0));
    }

    [Fact]
    public void IsUpdateAnalyticsReturned()
    {
        // Given
        IDictionary<string, string> dataInfo = new Dictionary<string, string>()
        {
            ["table"] = "CommunityBoardAnalytics",
            ["indicator"] = "feedPostTotal",
            ["modifier"] = "25",
            ["title"] = "feedTitle",
            ["titleName"] = "Supercar"
        };
        IAnalysisService service = new CommunityBoardAnalyticService(dataInfo);
        // When
        bool result = service.UpdateAnalytics();
        // Then
        Assert.True(result, "Update failed");
    }
}
