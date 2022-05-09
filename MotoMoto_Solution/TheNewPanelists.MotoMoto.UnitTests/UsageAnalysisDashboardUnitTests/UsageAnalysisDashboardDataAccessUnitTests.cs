using Xunit;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;


namespace TheNewPanelists.MotoMoto.UnitTests.UsageAnalysisDashboardTests
{
    /// <summary>
    /// There is no intergration testing here, Xunit can be used for it but it's very complicated
    /// Source: https://jimmybogard.com/integration-testing-with-xunit/
    /// </summary>
    public class UsageAnalysisDashboardDataAccessUnitTests
    {
        private readonly IFetchKpiDataAccess _fetchBarViewKpiDataAccess;
        private readonly IFetchKpiDataAccess _fetchBarFeedKpiDataAccess;
        private readonly IFetchKpiDataAccess _fetchTrendLoginKpiDataAccess;
        private readonly IFetchKpiDataAccess _fetchTrendRegistrationKpiDataAccess;
        private readonly IFetchKpiDataAccess _fetchTrendEventKpiDataAccess;
        private readonly ISubmitKpiDataAccess _submitAdmissionKpiDataAccess;
        private readonly ISubmitKpiDataAccess _submitViewKpiDataAccess;
        // private readonly IFetchKpiDataAccess _submitKpiDataAccess;
        // private readonly IFetchKpiDataAccess _submitKpiDataAccess;
        public UsageAnalysisDashboardDataAccessUnitTests()
        {
            _fetchBarViewKpiDataAccess = new FetchBarKpiViewDataAccess();
            _fetchBarFeedKpiDataAccess = new FetchBarKpiFeedDataAccess();
            _fetchTrendLoginKpiDataAccess = new FetchTrendKpiLoginDataAccess();
            _fetchTrendRegistrationKpiDataAccess = new FetchTrendKpiRegistrationDataAccess();
            _fetchTrendEventKpiDataAccess = new FetchTrendKpiEventDataAccess();
            _submitAdmissionKpiDataAccess = new SubmitAdmissionKpiDataAccess();
            _submitViewKpiDataAccess = new SubmitViewKpiDataAccess();
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

        [Fact]
        public void IsAsyncCallReturnData_SubmitAdmissionDataAccessLogin_ReturnTrue()
        {
            // Arrange
            IUsageMetricModel model = new LoginUsageMetricModel(10);
            // Act
            bool result = _submitAdmissionKpiDataAccess.SubmitKpiMetricsAsync(model).Result;
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsAsyncCallReturnData_SubmitAdmissionDataAccessRegistration_ReturnTrue()
        {
            // Arrange
            IUsageMetricModel model = new RegistrationUsageMetricModel(10);
            // Act
            bool result = _submitAdmissionKpiDataAccess.SubmitKpiMetricsAsync(model).Result;
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsAsyncCallReturnData_SubmitViewDataAccessDisplay_ReturnTrue()
        {
            // Arrange
            IUsageMetricModel model = new ViewUsageMetricModel("display", "Community Board", 1);
            // Act
            bool result = _submitViewKpiDataAccess.SubmitKpiMetricsAsync(model).Result;
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsAsyncCallReturnData_SubmitViewDataAccessDuration_ReturnTrue()
        {
            // Arrange
            IUsageMetricModel model = new ViewUsageMetricModel("duration", "Community Board", 1);
            // Act
            bool result = _submitViewKpiDataAccess.SubmitKpiMetricsAsync(model).Result;
            // Assert
            Assert.True(result);
        }
    }
}