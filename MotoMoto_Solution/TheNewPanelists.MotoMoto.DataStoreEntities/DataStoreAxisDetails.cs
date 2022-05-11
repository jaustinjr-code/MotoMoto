namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreAxisDetails : IAxisDetailsEntity
    {
        public string xData { get; }
        public string yData { get; }

        public DataStoreAxisDetails(string x, string y)
        {
            xData = x;
            yData = y;
        }
    }
}