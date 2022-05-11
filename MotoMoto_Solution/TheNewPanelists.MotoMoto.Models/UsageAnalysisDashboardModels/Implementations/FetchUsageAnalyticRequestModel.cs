namespace TheNewPanelists.MotoMoto.Models
{
    public class FetchUsageAnalyticRequestModel : IUsageAnalyticFetchRequestModel
    {
        public object input { get; }
        public string username { get; }

        public FetchUsageAnalyticRequestModel(IUsageAnalyticModel i, string user)
        {
            input = i;
            username = user;
        }
    }
}