namespace TheNewPanelists.MotoMoto.Models
{
    public interface IPostModel
    {
        string postTitle { get; set; }
        // Content type can be a community feed or event 
        string contentType { get; set; }
        string postUser { get; set; }
        string? postDescription { get; set; }
        DateTime submitUTC { get; set; }
        // Need to include an ImageList
        IEnumerable<byte[]> imageList { get; set; }
    }
}

