namespace TheNewPanelists.MotoMoto.Models
{
    public class NotificationSystemRequestModel
    {
        public string? username { get; }   
        public string? password { get; }

        public NotificationSystemRequestModel() {}

        public NotificationSystemRequestModel(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        
    }
}
