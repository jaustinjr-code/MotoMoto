using Xunit;
using TheNewPanelists.ServiceLayer.UsageAnalysisDashboard;

public class AdmissionAnalyticServiceTest
{
    [Fact]
    public void IsGetAnalyticsReturned()
    {
        // Given
        IAnalysisService service = new AdmissionAnalyticService();
        // When
        IList<IList<IDictionary<string, string>>?> refinedData = ((AdmissionAnalyticService)service).GetAnalytics();
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
            ["table"] = "AdmissionAnalytics",
            ["indicator1"] = "loginTotal",
            ["indicator2"] = "registrationTotal",
            ["modifier1"] = "11",
            ["modifier2"] = "20",
            ["title"] = "accessDate",
        };
        IAnalysisService service = new AdmissionAnalyticService(dataInfo);
        // When
        bool result = service.UpdateAnalytics();
        // Then
        Assert.True(result, "Update failed");
    }
}
