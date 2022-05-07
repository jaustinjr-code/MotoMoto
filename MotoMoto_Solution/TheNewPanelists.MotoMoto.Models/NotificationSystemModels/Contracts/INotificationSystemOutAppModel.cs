namespace TheNewPanelists.MotoMoto.Models
{
    public interface INotificationSystemOutAppModel
    {
        int postId { get; }
        string? postTitle { get; set; }
        string? eventLocation { get; set; }
        string? eventTime { get; set; }
        string? eventDate { get; set; }
        IEnumerable<string>? registeredUsers { get; set; }
        IEnumerable<string>? registeredUserEmails { get; set; }

    }
}
