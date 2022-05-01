namespace TheNewPanelists.MotoMoto.Models
{
    public class FeedPostModel : IPostModel
    {
        public string type { get; } = "post";
        //public int postID { get; }
        public string? postTitle { get; set; }
        public string? contentType { get; set; }
        public string? postUser { get; set; }
        public string? postDescription { get; set; }
        //public DateTime submitUTC { get; set; }
        public IEnumerable<byte[]>? imageList { get; set; }

        // Need default constructor for model binding
        public FeedPostModel() { }
        public FeedPostModel(string title, string type, string username, string description)
        {
            postTitle = title;
            contentType = type;
            postUser = username;
            postDescription = description;
        }

        public FeedPostModel(string title, string type, string username, string description, IEnumerable<byte[]> images)
        {
            postTitle = title;
            contentType = type;
            postUser = username;
            postDescription = description;
            imageList = images;
        }
    }
}