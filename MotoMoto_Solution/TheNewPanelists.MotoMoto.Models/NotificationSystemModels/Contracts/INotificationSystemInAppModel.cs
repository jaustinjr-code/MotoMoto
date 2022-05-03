namespace TheNewPanelists.MotoMoto.Models
{
    public interface INotificationSystemInAppModel
    {
        string? eventTime { get; set; }   
        string? eventDate { get; set; } 
        string? eventStreetAddress { get; set; }
        string? eventCity { get; set; }

        string? eventState { get; set; }
        string? eventCountry { get; set; }
        string? eventZipCode { get; set; }
        // bool isNotificationSent { get; set; }
        // int eventID { get; }

    }
}
