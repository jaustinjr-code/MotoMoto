namespace TheNewPanelists.MotoMoto.Models
{
    public class LoginUsageMetricModel : IUsageMetricModel
    {
        public string type { get; } = "login";
        public string? subType { get; }
        public string? title { get; }
        public int metric { get; }

        public LoginUsageMetricModel(int metric)
        {
            this.metric = metric;
        }
    }
}