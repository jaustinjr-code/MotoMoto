namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public interface IPostEntity : IContentEntity
    {
        int postId { get; }
        string postTitle { get; }
        DataStoreUserProfile? postUser { get; }
        string? postDescription { get; }
        IEnumerable<byte[]>? imageList { get; set; }
    }
}