namespace TheNewPanelists.MotoMoto.Models
{
    public class LoginUsageMetricModel : IUsageMetricModel
    {
        public string type { get; } = "login";
        public string? subType { get; }
        public string? title { get; }
        public int metric { get; }

        public LoginUsageMetricModel(string? sub, int m = 0)
        {
            subType = sub;
            metric = m;
        }
    }
}