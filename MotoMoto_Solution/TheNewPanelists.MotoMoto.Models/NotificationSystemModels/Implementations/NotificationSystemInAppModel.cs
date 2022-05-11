namespace TheNewPanelists.MotoMoto.Models
{
    public class NotificationSystemInAppModel : INotificationSystemInAppModel
    {
        public string? eventTime { get; set; }   
        public string? eventDate { get; set; } 
        public string? eventStreetAddress { get; set; }
        public string? eventCity { get; set; }
        public string? eventState { get; set; }
        public string? eventCountry { get; set; }
        public string? eventZipCode { get; set; }
        public int? eventID {get; set;}
        public string? eventTitle{get; set;}
        public string? notificationStatusMessage { get; set; }
        
    }
}
