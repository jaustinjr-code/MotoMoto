using Xunit;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;


namespace TheNewPanelists.MotoMoto.UnitTests.UsageAnalysisDashboardTests
{
    public class UsageAnalysisDashboardDataAccessUnitTests
    {
        private readonly IFetchKpiDataAccess _fetchBarViewKpiDataAccess;
        private readonly IFetchKpiDataAccess _fetchBarFeedKpiDataAccess;
        private readonly IFetchKpiDataAccess _fetchTrendLoginKpiDataAccess;
        private readonly IFetchKpiDataAccess _fetchTrendRegistrationKpiDataAccess;
        private readonly IFetchKpiDataAccess _fetchTrendEventKpiDataAccess;
        // private readonly IFetchKpiDataAccess _submitKpiDataAccess;
        // private readonly IFetchKpiDataAccess _submitKpiDataAccess;
        public UsageAnalysisDashboardDataAccessUnitTests()
        {
            _fetchBarViewKpiDataAccess = new FetchBarKpiViewDataAccess();
            _fetchBarFeedKpiDataAccess = new FetchBarKpiFeedDataAccess();
            _fetchTrendLoginKpiDataAccess = new FetchTrendKpiLoginDataAccess();
            _fetchTrendRegistrationKpiDataAccess = new FetchTrendKpiRegistrationDataAccess();
            _fetchTrendEventKpiDataAccess = new FetchTrendKpiEventDataAccess();
            // _submitKpiDataAccess = new SubmitKpiDataAccess()
            // _submitKpiDataAccess = new SubmitKpiDataAccess()
        }


        // Tests are too shallow and are prone to false positives

        [Fact]
        public void IsFetchChartDataValid_FetchBarViewKpiDataAccess_DisplayTotalNotNull()
        {
            // Arrange
            IUsageAnalyticModel model = new BarChartAnalyticModel("Views", "displayTotal");
            // Act
            IUsageAnalyticEntity result = _fetchBarViewKpiDataAccess.FetchChartMetrics(model);
            // Assert
            Assert.NotNull(result.metricList);
        }

        [Fact]
        public void IsFetchChartDataValid_FetchBarFeedKpiDataAccess_FeedPostTotalNotNull()
        {
            // Arrange
            IUsageAnalyticModel model = new BarChartAnalyticModel("feedName", "feedPostTotal");
            // Act
            IUsageAnalyticEntity result = _fetchBarFeedKpiDataAccess.FetchChartMetrics(model);
            // Assert
            Assert.NotNull(result.metricList);
        }

        [Fact]
        public void IsFetchChartDataValid_FetchTrendKpiLoginDataAccess_LoginTotalNotNull()
        {
            // Arrange
            IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Login");
            // Act
            IUsageAnalyticEntity result = _fetchTrendLoginKpiDataAccess.FetchChartMetrics(model);
            // Assert
            Assert.NotNull(result.metricList);
        }

        [Fact]
        public void IsFetchChartDataValid_FetchTrendKpiRegistrationDataAccess_RegistrationTotalNotNull()
        {
            // Arrange
            IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Registration");
            // Act
            IUsageAnalyticEntity result = _fetchTrendRegistrationKpiDataAccess.FetchChartMetrics(model);
            // Assert
            Assert.NotNull(result.metricList);
        }
    }
}