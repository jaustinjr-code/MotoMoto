namespace TheNewPanelists.MotoMoto.Models
{
    public class ViewUsageMetricModel : IUsageMetricModel
    {
        public string type { get; } = "view";
        public string subType { get; } // DisplayTotal or DurationAvg
        public string title { get; }

        public int metric { get; }

        public ViewUsageMetricModel(string sub, string t, int m = 0)
        {
            subType = sub;
            title = t;
            metric = m;
        }
    }
}