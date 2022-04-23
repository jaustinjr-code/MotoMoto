namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public interface IFeedEntity
    {
        string feedName { get; }
        IEnumerable<IPostEntity> postList { get; }
        IAnalytic? feedAnalytic { get; }
    }
}