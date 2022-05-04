namespace TheNewPanelists.MotoMoto.Models
{
    public class NotificationSystemOutAppModel : INotificationSystemOutAppModel
    {
        public int postId { get; }
        public string? postTitle { get; set; }
        public string? eventLocation { get; set; }
        public string? eventTime { get; set; }
        public string? eventDate { get; set; }
        public IEnumerable<string>? registeredUsers { get; set; }
        public IEnumerable<string>? registeredUserEmails { get; set; }
    }
}
