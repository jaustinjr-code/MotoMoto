namespace TheNewPanelists.MotoMoto.Models
{
    public class CommentPostResponseModel : IResponseModel
    {
        public object? output { get; }
        public string responseMessage { get; }
        public bool isComplete { get; }
        public bool isSuccess { get; }

        public CommentPostResponseModel(string message, bool complete, bool success)
        {
            responseMessage = message;
            isComplete = complete;
            isSuccess = success;
        }

        public CommentPostResponseModel(object o, string message, bool complete, bool success)
        {
            output = o;
            responseMessage = message;
            isComplete = complete;
            isSuccess = success;
        }
    }
}