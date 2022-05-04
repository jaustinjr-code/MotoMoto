
namespace TheNewPanelists.MotoMoto.Models
{
    // Specifically for Post details like Upvotes and Comments
    // TODO: edit model, maybe use Comments and Upvotes as separate variables?
    public class FeedPostResponseModel : IResponseModel
    {
        // I wanted all of these attributes private but the warnings won't go away if I do
        public object? output { get; }
        public string responseMessage { get; }
        public bool isComplete { get; }
        public bool isSuccess { get; }

        public FeedPostResponseModel(string message, bool complete, bool success)
        {
            responseMessage = message;
            isComplete = complete;
            isSuccess = success;
        }

        public FeedPostResponseModel(object? o, string message, bool complete, bool success)
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