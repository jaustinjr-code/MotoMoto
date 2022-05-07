namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreUsageAnalyticTrend : IUsageAnalyticEntity
    {
        public string xTitle { get; } // DateTime Objects?
        public string yTitle { get; }
        public IEnumerable<IAxisDetailsEntity> metricList { get; }

        public DataStoreUsageAnalyticTrend(string x, string y, IEnumerable<IAxisDetailsEntity> metrics)
        {
            xTitle = x;
            yTitle = y;
            metricList = metrics;
        }
    }
}