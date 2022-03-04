using Xunit;
using TheNewPanelists.ServiceLayer.UsageAnalysisDashboard;

public class ViewAnalyticServiceTest
{
    [Fact]
    public void IsGetAnalyticsReturned()
    {
        // Given
        IAnalysisService service = new ViewAnalyticService();
        // When
        IList<IList<IDictionary<string, string>>?> refinedData = ((ViewAnalyticService)service).GetAnalytics();
        // Then
        Assert.NotNull(refinedData);
        // Assert.NotEmpty(refinedData);
    }

    [Fact]
    public void IsUpdateAnalyticsReturned()
    {
        // Given
        IDictionary<string, string> dataInfo = new Dictionary<string, string>()
        {
            ["table"] = "ViewAnalytics",
            ["indicator1"] = "displayTotal",
            ["indicator2"] = "durationAvg",
            ["modifier1"] = "1",
            ["modifier2"] = "20",
            ["title"] = "viewTitle",
            ["titleName"] = "View2"
        };
        IAnalysisService service = new ViewAnalyticService(dataInfo);
        // When
        bool result = ((ViewAnalyticService)service).UpdateAnalytics();
        // Then
        Assert.True(result, "Update failed");
    }
}
