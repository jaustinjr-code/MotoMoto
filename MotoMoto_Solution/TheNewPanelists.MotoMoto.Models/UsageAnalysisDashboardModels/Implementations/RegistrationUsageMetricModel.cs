namespace TheNewPanelists.MotoMoto.Models
{
    public class RegistrationUsageMetricModel : IUsageMetricModel
    {
        public string type { get; } = "registration";
        public string? subType { get; }
        public string? title { get; }

        public int metric { get; }

        public RegistrationUsageMetricModel(string? sub, int m = 0)
        {
            subType = sub;
            metric = m;
        }
    }
}