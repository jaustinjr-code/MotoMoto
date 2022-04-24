namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStorePost : IPostEntity
    {
        public string type { get; } = "post";
        public int postId { get; }
        public string postTitle { get; }
        public DataStoreUserProfile? postUser { get; }
        public string? postUsername { get; }
        public string? postDescription { get; }
        public IEnumerable<byte[]>? imageList { get; set; }

        public DataStorePost(int id, string title, string username, string? description, IEnumerable<byte[]>? images)
        {
            postId = id;
            postTitle = title;
            postUsername = username;
            postDescription = description;
            imageList = images;
        }

        public DataStorePost(int id, string title, DataStoreUserProfile user, string? description, IEnumerable<byte[]>? images)
        {
            postId = id;
            postTitle = title;
            postUser = user;
            postDescription = description;
            imageList = images;
        }

        public override string ToString()
        {
            string result = postId + ". " + postTitle + ": " + postUsername + " said \"" + postDescription + "\"";
            return result;
        }
    }
}