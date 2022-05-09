namespace TheNewPanelists.MotoMoto.Models
{
    public class ViewUsageMetricModel : IUsageMetricModel
    {
        public string type { get; } = "view";
        public string subType { get; } // DisplayTotal or DurationAvg
        public string title { get; }

        public int metric { get; }

        public ViewUsageMetricModel(string subType, string title, int metric = 0)
        {
            this.subType = subType;
            this.title = title;
            this.metric = metric;
        }
    }
}