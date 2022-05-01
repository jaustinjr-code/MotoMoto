namespace TheNewPanelists.MotoMoto.Models
{
    public class UpvoteCommentModel : IInteractionModel
    {
        public int contentId { get; set; }
        public int postId { get; set; }
        public string? contentTitle { get; set; }
        public string interactUsername { get; set; }
        public UpvoteCommentModel(int contentId, int postId, string interactUsername)
        {
            this.contentId = contentId;
            this.postId = postId;
            this.interactUsername = interactUsername;
        }
    }
}