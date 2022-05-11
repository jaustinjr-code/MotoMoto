namespace TheNewPanelists.MotoMoto.Models
{
    public interface IUsageAnalyticFetchRequestModel : IRequestModel
    {
        string username { get; }
        // Need info to check user credentials if are a valid admin
    }
}