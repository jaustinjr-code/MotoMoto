namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreUsageAnalyticBar : IUsageAnalyticEntity
    {
        public string xTitle { get; }
        public string yTitle { get; }
        public IEnumerable<IAxisDetailsEntity> metricList { get; }

        public DataStoreUsageAnalyticBar(string x, string y, IEnumerable<IAxisDetailsEntity> metrics)
        {
            xTitle = x;
            yTitle = y;
            metricList = metrics;
        }
    }
}