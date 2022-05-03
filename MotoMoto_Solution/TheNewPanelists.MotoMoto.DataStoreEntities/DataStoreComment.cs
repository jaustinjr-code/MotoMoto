namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreComment : IContentEntity
    {
        public string type { get; } = "comment";
        public int commentId { get; }
        public int postId { get; }
        public string commentUsername { get; }
        public string commentDescription { get; }

        public DataStoreComment(int cid, int pid, string username, string description)
        {
            commentId = cid;
            postId = pid;
            commentUsername = username;
            commentDescription = description;
        }
    }
}