namespace TheNewPanelists.MotoMoto.Models
{
    public class ExceptionResponseModel : IResponseModel
    {
        public object? output { get; } = null;
        public string responseMessage { get; }
        public bool isComplete { get; }
        public bool isSuccess { get; }
        public Exception exception { get; }

        public ExceptionResponseModel(string message)
        {
            responseMessage = message;
            isComplete = false;
            isSuccess = false;
            exception = new Exception(responseMessage);
        }
    }
}