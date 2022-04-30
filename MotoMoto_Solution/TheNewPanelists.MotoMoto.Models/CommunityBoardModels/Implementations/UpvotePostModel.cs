namespace TheNewPanelists.MotoMoto.Models
{
    public class UpvotePostModel : IInteractionModel
    {
        public int contentId { get; set; }
        public string? contentTitle { get; set; }
        public string interactUsername { get; set; }
        public UpvotePostModel(int id, string title, string username)
        {
            contentId = id;
            contentTitle = title;
            interactUsername = username;
        }
    }
}