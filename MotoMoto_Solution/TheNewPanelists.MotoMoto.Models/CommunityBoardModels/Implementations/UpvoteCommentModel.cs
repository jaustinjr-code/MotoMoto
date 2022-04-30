namespace TheNewPanelists.MotoMoto.Models
{
    public class UpvoteCommentModel : IInteractionModel
    {
        public int contentId { get; set; }
        public int postId { get; set; }
        public string? contentTitle { get; set; }
        public string interactUsername { get; set; }
        public UpvoteCommentModel(int cid, int pid, string? title, string username)
        {
            contentId = cid;
            postId = pid;
            contentTitle = title;
            interactUsername = username;
        }
    }
}