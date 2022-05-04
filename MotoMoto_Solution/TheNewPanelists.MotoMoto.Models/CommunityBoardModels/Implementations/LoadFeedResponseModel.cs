
namespace TheNewPanelists.MotoMoto.Models
{
    public class LoadFeedResponseModel : IResponseModel
    {
        // I wanted all of these attributes private but the warnings won't go away if I do
        public object? output { get; }
        public string responseMessage { get; }
        public bool isComplete { get; }
        public bool isSuccess { get; }

        public LoadFeedResponseModel(string message, bool complete, bool success)
        {
            responseMessage = message;
            isComplete = complete;
            isSuccess = success;
        }

        public LoadFeedResponseModel(object? o, string message, bool complete, bool success)
        {
            output = o;
            responseMessage = message;
            isComplete = complete;
            isSuccess = success;
        }

        public override string ToString()
        {
            return responseMessage + " Completion Status: " + (isComplete ? "Complete" : "Incomplete");
        }
    }
}