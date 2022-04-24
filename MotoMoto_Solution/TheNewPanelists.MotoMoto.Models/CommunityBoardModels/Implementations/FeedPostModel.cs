namespace TheNewPanelists.MotoMoto.Models
{
    public class FeedPostModel : IPostModel
    {
        public string type { get; } = "post";
        public int postID { get; }
        public string? postTitle { get; set; }
        public string? contentType { get; set; }
        public string? postUser { get; set; }
        public string? postDescription { get; set; }
        public DateTime submitUTC { get; set; }
        public IEnumerable<byte[]>? imageList { get; set; }

        public FeedPostModel(int id)
        {
            postID = id;
        }

        public FeedPostModel(int id, string title, string type, string username, DateTime utc)
        {
            postID = id;
            postTitle = title;
            contentType = type;
            postUser = username;
            submitUTC = utc;
        }
    }
}