namespace TheNewPanelists.MotoMoto.Models
{
    public class UpvotePostModel : IInteractionModel
    {
        public int contentId { get; set; }
        public string? contentTitle { get; set; }
        public string interactUsername { get; set; }

        public UpvotePostModel(int contentId, string contentTitle, string interactUsername)
        {
            this.contentId = contentId;
            this.contentTitle = contentTitle;
            this.interactUsername = interactUsername;
        }
    }
}