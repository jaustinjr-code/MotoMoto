namespace TheNewPanelists.MotoMoto.Models
{
    public class RegistrationUsageMetricModel : IUsageMetricModel
    {
        public string type { get; } = "registration";
        public string? subType { get; }
        public string? title { get; }

        public int metric { get; }

        public RegistrationUsageMetricModel(int metric)
        {
            this.metric = metric;
        }
    }
}