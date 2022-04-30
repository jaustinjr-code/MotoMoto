namespace TheNewPanelists.MotoMoto.Models
{
    public class CommentPostResponseModel : IResponseModel
    {
        public object? output { get; set; } = null;
        public string responseMessage { get; }
        public bool isComplete { get; }
        public bool isSuccess { get; }

        public CommentPostResponseModel(string message, bool complete, bool success)
        {
            responseMessage = message;
            isComplete = complete;
            isSuccess = success;
        }
    }
}