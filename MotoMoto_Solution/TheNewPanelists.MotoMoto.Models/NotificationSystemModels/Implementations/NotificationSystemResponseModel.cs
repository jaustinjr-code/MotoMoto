namespace TheNewPanelists.MotoMoto.Models
{
    public class NotificationSystemResponseModel
    {
        public int? eventID { get; set;}
        public string? username { get; set; }   
        public string? password { get; }
        public string? eventTime { get; set; }
        public string? eventDate { get; set; }
        public string? eventStreetAddress { get; set; }
        public string? eventCity { get; set; }
        public string? eventState { get; set; }
        public string? eventCountry { get; set; }
        public string? eventZipCode { get; set; }
        public string? eventTitle { get; set; }
        public string? notificationSystemStatusMessage { get; set; }
        public string? email { get; set; }

        public NotificationSystemResponseModel() {}

        public NotificationSystemResponseModel(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        // public NotificationResponseModel(string username, string password, string eventTitme, string eventDate, string eventStreetAddress, string eventCity, string eventState, string eventCountry, string eventZipCode, string eventTitle, )
        // {
        //     this.username = username;
        //     this.password = password;
        // }
        
    }
}
