namespace TheNewPanelists.MotoMoto.Models
{
    public interface IFeedModel : IContentModel
    {
        string feedName { get; set; }
        //IEnumerable<IPostModel> postList { get; set; }
    }
}