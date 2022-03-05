using Xunit;
using TheNewPanelists.ServiceLayer.UsageAnalysisDashboard;

public class EventListAnalyticServiceTest
{
    [Fact]
    public void IsGetAnalyticsReturned()
    {
        // Given
        IAnalysisService service = new EventListAnalyticService();
        // When
        IList<IList<IDictionary<string, string>>?> refinedData = service.GetAnalytics();
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
            ["table"] = "EventListAnalytics",
            ["indicator"] = "eventRegistrationTotal",
            ["modifier"] = "25",
            ["title"] = "eventAccountUsername",
            ["titleName"] = "jacobCrib"
        };
        IAnalysisService service = new EventListAnalyticService(dataInfo);
        // When
        bool result = service.UpdateAnalytics();
        // Then
        Assert.True(result, "Update failed");
    }
}
