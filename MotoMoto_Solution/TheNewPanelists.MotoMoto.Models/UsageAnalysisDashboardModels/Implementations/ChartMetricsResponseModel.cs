using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Models
{
    public class ChartMetricsResponseModel : IResponseModel
    {
        public object? output { get; }
        public string responseMessage { get; }
        public bool isComplete { get; }
        public bool isSuccess { get; }

        public ChartMetricsResponseModel(IUsageAnalyticEntity o, string message, bool complete, bool success)
        {
            output = o;
            responseMessage = message;
            isComplete = complete;
            isSuccess = success;
        }

        public ChartMetricsResponseModel(string message, bool complete, bool success)
        {
            responseMessage = message;
            isComplete = complete;
            isSuccess = success;
        }
    }
}