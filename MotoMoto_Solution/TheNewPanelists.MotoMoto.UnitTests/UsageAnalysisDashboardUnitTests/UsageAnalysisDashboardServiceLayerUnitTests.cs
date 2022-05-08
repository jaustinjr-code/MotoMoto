using Xunit;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;


namespace TheNewPanelists.MotoMoto.UnitTests.UsageAnalysisDashboardUnitTests
{
    /// <summary>
    /// These unit tests only check for the success outcome, ideally nothing should interrupt each operation
    /// Downside is the response data is not evaluated so there can be false positives
    /// Any exceptions are not tested 
    /// </summary>
    public class UsageAnalysisDashboardServiceLayerUnitTests
    {
        private readonly IFetchBarChartService _fetchBarViewKpiService;
        private readonly IFetchBarChartService _fetchBarFeedKpiService;
        private readonly IFetchTrendChartService _fetchTrendLoginKpiService;
        private readonly IFetchTrendChartService _fetchTrendRegistrationKpiService;
        private readonly IFetchTrendChartService _fetchTrendEventKpiService;

        public UsageAnalysisDashboardServiceLayerUnitTests()
        {
            _fetchBarViewKpiService = new FetchViewBarChartService();
            _fetchBarFeedKpiService = new FetchFeedBarChartService();
            _fetchTrendLoginKpiService = new FetchLoginTrendChartService();
            _fetchTrendRegistrationKpiService = new FetchRegistrationTrendChartService();
            _fetchTrendEventKpiService = new FetchEventTrendChartService();
        }

        [Fact]
        public void IsResponseSuccessForValidRequest_FetchViewBarChartServiceDisplay_ReturnTrue()
        {
            // Arrange
            IUsageAnalyticModel model = new BarChartAnalyticModel("Views", "displayTotal");
            // Act
            IResponseModel result = _fetchBarViewKpiService.FetchBarChartMetrics(model);
            // Assert
            Assert.True(result.isSuccess);
        }

        [Fact]
        public void IsResponseSuccessForValidRequest_FetchViewBarChartServiceDuration_ReturnTrue()
        {
            // Arrange
            IUsageAnalyticModel model = new BarChartAnalyticModel("Views", "durationAvg");
            // Act
            IResponseModel result = _fetchBarViewKpiService.FetchBarChartMetrics(model);
            // Assert
            Assert.True(result.isSuccess);
        }

        [Fact]
        public void IsResponseSuccessForValidRequest_FetchFeedBarChartService_ReturnTrue()
        {
            // Arrange
            IUsageAnalyticModel model = new BarChartAnalyticModel("feedName", "feedPostTotal");
            // Act
            IResponseModel result = _fetchBarFeedKpiService.FetchBarChartMetrics(model);
            // Assert
            Assert.True(result.isSuccess);
        }

        [Fact]
        public void IsResponseSuccessForValidRequest_FetchLoginTrendChartService_ReturnTrue()
        {
            // Arrange
            IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Login");
            // Act
            IResponseModel result = _fetchTrendLoginKpiService.FetchTrendChartMetrics(model);
            // Assert
            Assert.True(result.isSuccess);
        }

        [Fact]
        public void IsResponseSuccessForValidRequest_FetchRegistrationTrendChartService_ReturnTrue()
        {
            // Arrange
            IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Registration");
            // Act
            IResponseModel result = _fetchTrendRegistrationKpiService.FetchTrendChartMetrics(model);
            // Assert
            Assert.True(result.isSuccess);
        }

        [Fact]
        public void IsResponseSuccessForValidRequest_FetchEventTrendChartService_ReturnTrue()
        {
            // Arrange
            IUsageAnalyticModel model = new TrendChartAnalyticModel("Event Date", "Event Total");
            // Act
            IResponseModel result = _fetchTrendEventKpiService.FetchTrendChartMetrics(model);
            // Assert
            Assert.True(result.isSuccess);
        }
    }
}