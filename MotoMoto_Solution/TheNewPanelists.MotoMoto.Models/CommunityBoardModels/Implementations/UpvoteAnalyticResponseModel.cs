namespace TheNewPanelists.MotoMoto.Models
{
    public class UpvoteAnalyticResponseModel : IResponseModel
    {
        public object? output { get; }
        public string responseMessage { get; }
        public bool isComplete { get; }
        public bool isSuccess { get; }

        public UpvoteAnalyticResponseModel(object o, string message, bool complete, bool success)
        {
            output = o;
            responseMessage = message;
            isComplete = complete;
            isSuccess = success;
        }

        public UpvoteAnalyticResponseModel(string message, bool complete, bool success)
        {
            responseMessage = message;
            isComplete = complete;
            isSuccess = success;
        }
    }
}