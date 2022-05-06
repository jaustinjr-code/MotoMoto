namespace TheNewPanelists.MotoMoto.Models
{
    public interface IResponseModel
    {
        // Should these attributes be protected?
        // NOTE: using protected on the implementing classes gives error
        // Generic output object in case anyone else wants to use this interface
        object? output { get; }
        // Must have a message for the client side
        string responseMessage { get; }
        // Complete is a finished operation
        bool isComplete { get; }
        // Success is a correct operation
        bool isSuccess { get; }
    }
}