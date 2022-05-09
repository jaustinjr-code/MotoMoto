using Xunit;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;


namespace TheNewPanelists.MotoMoto.UnitTests.UsageAnalysisDashboardTests
{
    /// <summary>
    /// These unit tests only check for the success and invalid requests scenarios of the Business Layer
    /// There is possibility of false positives for the success response because the data is not evaluated
    /// </summary>
    public class UsageAnalysisDashboardBusinessLayerUnitTests
    {
        //private readonly IFetchChartManager _fetchBarChartManager;
        //private readonly IFetchChartManager _fetchTrendChartManager;
        public UsageAnalysisDashboardBusinessLayerUnitTests() { }

        [Fact]
        public void IsAnalyticRequestValid_FetchViewBarChartManagerDisplay_ReturnTrue()
        {
            // Arrange
            IFetchChartManager fetchBarChartManager = new FetchBarChartManager("view");
            IUsageAnalyticModel model = new BarChartAnalyticModel("Views", "displayTotal");
            IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
            // Act
            IResponseModel response = fetchBarChartManager.IsAnalyticRequestValid(request);
            // Assert
            Assert.True(response.isSuccess);
        }

        [Fact]
        public void IsAnalyticRequestValid_FetchViewBarChartManagerDuration_ReturnTrue()
        {
            // Arrange
            IFetchChartManager fetchBarChartManager = new FetchBarChartManager("view");
            IUsageAnalyticModel model = new BarChartAnalyticModel("Views", "durationAvg");
            IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
            // Act
            IResponseModel response = fetchBarChartManager.IsAnalyticRequestValid(request);
            // Assert
            Assert.True(response.isSuccess);
        }

        [Fact]
        public void IsAnalyticRequestInvalid_FetchViewBarChartManager_ReturnFalse()
        {
            // Arrange
            IFetchChartManager fetchBarChartManager = new FetchBarChartManager("invalid");
            IUsageAnalyticModel model = new BarChartAnalyticModel("Views", "durationAvg");
            IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
            // Act
            IResponseModel response = fetchBarChartManager.IsAnalyticRequestValid(request);
            // Assert
            Assert.False(response.isSuccess);
        }

        [Fact]
        public void IsAnalyticRequestValid_FetchFeedBarChartManager_ReturnTrue()
        {
            // Arrange
            IFetchChartManager fetchBarChartManager = new FetchBarChartManager("feed");
            IUsageAnalyticModel model = new BarChartAnalyticModel("Feeds", "feedPostTotal");
            IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
            // Act
            IResponseModel response = fetchBarChartManager.IsAnalyticRequestValid(request);
            // Assert
            Assert.True(response.isSuccess);
        }

        [Fact]
        public void IsAnalyticRequestInvalid_FetchFeedBarChartManager_ReturnFalse()
        {
            // Arrange
            IFetchChartManager fetchBarChartManager = new FetchBarChartManager("invalid");
            IUsageAnalyticModel model = new BarChartAnalyticModel("Feeds", "feedPostTotal");
            IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
            // Act
            IResponseModel response = fetchBarChartManager.IsAnalyticRequestValid(request);
            // Assert
            Assert.False(response.isSuccess);
        }

        [Fact]
        public void IsAnalyticRequestValid_FetchLoginTrendChartManager_ReturnTrue()
        {
            // Arrange
            IFetchChartManager fetchTrendChartManager = new FetchTrendChartManager("login");
            IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Login");
            IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
            // Act
            IResponseModel response = fetchTrendChartManager.IsAnalyticRequestValid(request);
            // Assert
            Assert.True(response.isSuccess);
        }

        [Fact]
        public void IsAnalyticRequestInvalid_FetchLoginTrendChartManager_ReturnFalse()
        {
            // Arrange
            IFetchChartManager fetchTrendChartManager = new FetchTrendChartManager("invalid");
            IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Login");
            IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
            // Act
            IResponseModel response = fetchTrendChartManager.IsAnalyticRequestValid(request);
            // Assert
            Assert.False(response.isSuccess);
        }

        [Fact]
        public void IsAnalyticRequestValid_FetchRegistrationTrendChartManager_ReturnTrue()
        {
            // Arrange
            IFetchChartManager fetchTrendChartManager = new FetchTrendChartManager("registration");
            IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Registration");
            IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
            // Act
            IResponseModel response = fetchTrendChartManager.IsAnalyticRequestValid(request);
            // Assert
            Assert.True(response.isSuccess);
        }

        [Fact]
        public void IsAnalyticRequestInvalid_FetchRegistrationTrendChartManager_ReturnFalse()
        {
            // Arrange
            IFetchChartManager fetchTrendChartManager = new FetchTrendChartManager("invalid");
            IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Registration");
            IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
            // Act
            IResponseModel response = fetchTrendChartManager.IsAnalyticRequestValid(request);
            // Assert
            Assert.False(response.isSuccess);
        }

        [Fact]
        public void IsAnalyticRequestValid_FetchEventTrendChartManager_ReturnTrue()
        {
            // Arrange
            IFetchChartManager fetchTrendChartManager = new FetchTrendChartManager("Event");
            IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Event");
            IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
            // Act
            IResponseModel response = fetchTrendChartManager.IsAnalyticRequestValid(request);
            // Assert
            Assert.True(response.isSuccess);
        }

        [Fact]
        public void IsAnalyticRequestInvalid_FetchEventTrendChartManager_ReturnFalse()
        {
            // Arrange
            IFetchChartManager fetchTrendChartManager = new FetchTrendChartManager("invalid");
            IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Event");
            IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
            // Act
            IResponseModel response = fetchTrendChartManager.IsAnalyticRequestValid(request);
            // Assert
            Assert.False(response.isSuccess);
        }

        [Fact]
        public void IsAsyncCallReturnSuccessResponse_SubmitAdmissionKpiManagerLogin_ReturnTrue()
        {
            // Arrange
            IUsageMetricModel model = new LoginUsageMetricModel(null, 10);
            ISubmitKpiManager manager = new SubmitAdmissionKpiManager();
            // Act
            IResponseModel response = manager.IsSubmitKpiRequestValidAsync(model).Result;
            // Assert
            Assert.True(response.isSuccess);
        }

        [Fact]
        public void IsAsyncCallReturnExceptionResponse_SubmitAdmissionKpiManagerLogin_ReturnFalse()
        {
            // Arrange
            IUsageMetricModel model = new LoginUsageMetricModel(null, -1);
            ISubmitKpiManager manager = new SubmitAdmissionKpiManager();
            // Act
            IResponseModel response = manager.IsSubmitKpiRequestValidAsync(model).Result;
            // Assert
            Assert.False(response.isComplete || response.isSuccess);
        }

        [Fact]
        public void IsAsyncCallReturnSuccessResponse_SubmitAdmissionKpiManagerRegistration_ReturnTrue()
        {
            // Arrange
            IUsageMetricModel model = new RegistrationUsageMetricModel(null, 10);
            ISubmitKpiManager manager = new SubmitAdmissionKpiManager();
            // Act
            IResponseModel response = manager.IsSubmitKpiRequestValidAsync(model).Result;
            // Assert
            Assert.True(response.isSuccess);
        }

        [Fact]
        public void IsAsyncCallReturnExceptionResponse_SubmitAdmissionKpiManagerRegistration_ReturnFalse()
        {
            // Arrange
            IUsageMetricModel model = new RegistrationUsageMetricModel(null, -1);
            ISubmitKpiManager manager = new SubmitAdmissionKpiManager();
            // Act
            IResponseModel response = manager.IsSubmitKpiRequestValidAsync(model).Result;
            // Assert
            Assert.False(response.isComplete || response.isSuccess);
        }

        [Fact]
        public void IsAsyncCallReturnSuccessResponse_SubmitViewKpiManagerDisplay_ReturnTrue()
        {
            // Arrange
            IUsageMetricModel model = new ViewUsageMetricModel("display", "Community Board", 1);
            ISubmitKpiManager manager = new SubmitViewKpiManager();
            // Act
            IResponseModel result = manager.IsSubmitKpiRequestValidAsync(model).Result;
            // Assert
            Assert.True(result.isSuccess);
        }

        [Fact]
        public void IsAsyncCallReturnExceptionResponse_SubmitViewKpiManagerDisplay_ReturnFalse()
        {
            // Arrange
            IUsageMetricModel model = new ViewUsageMetricModel("displa", "Community Board", -1);
            ISubmitKpiManager manager = new SubmitViewKpiManager();
            // Act
            IResponseModel result = manager.IsSubmitKpiRequestValidAsync(model).Result;
            // Assert
            Assert.False(result.isComplete || result.isSuccess);
        }

        [Fact]
        public void IsAsyncCallReturnSuccessResponse_SubmitViewKpiManagerDuration_ReturnTrue()
        {
            // Arrange
            IUsageMetricModel model = new ViewUsageMetricModel("duration", "Community Board", 1);
            ISubmitKpiManager manager = new SubmitViewKpiManager();
            // Act
            IResponseModel result = manager.IsSubmitKpiRequestValidAsync(model).Result;
            // Assert
            Assert.True(result.isSuccess);
        }

        [Fact]
        public void IsAsyncCallReturnExceptionResponse_SubmitViewKpiManagerDuration_ReturnFalse()
        {
            // Arrange
            IUsageMetricModel model = new ViewUsageMetricModel("duratio", "Community Board", -1);
            ISubmitKpiManager manager = new SubmitViewKpiManager();
            // Act
            IResponseModel result = manager.IsSubmitKpiRequestValidAsync(model).Result;
            // Assert
            Assert.False(result.isComplete || result.isSuccess);
        }
    }
}