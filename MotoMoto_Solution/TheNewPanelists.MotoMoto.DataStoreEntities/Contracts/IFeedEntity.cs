namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public interface IFeedEntity : IContentEntity
    {
        string feedName { get; }
        IEnumerable<IPostEntity> postList { get; }
        IAnalytic? feedAnalytic { get; }
    }
}