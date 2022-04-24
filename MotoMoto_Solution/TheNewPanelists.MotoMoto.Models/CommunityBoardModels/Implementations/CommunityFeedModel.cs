namespace TheNewPanelists.MotoMoto.Models
{
    public class CommunityFeedModel : IFeedModel
    {
        public string type { get; } = "feed";
        public string feedName { get; set; } = default!;
        //public IEnumerable<IPostModel> postList { get; set; } = default!;
    }
}