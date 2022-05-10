namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public interface IUsageAnalyticEntity
    {
        // IAxisDetailsEntity x_data { get; }
        // IAxisDetailsEntity y_data { get; }

        string xTitle { get; }
        string yTitle { get; }
        IEnumerable<IAxisDetailsEntity> metricList { get; }
    }
}