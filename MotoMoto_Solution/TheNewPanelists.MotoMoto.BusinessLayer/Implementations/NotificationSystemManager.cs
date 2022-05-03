using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class NotificationSystemManager
    {
        private readonly NotificationSystemService _notificationSystemService;
        // Default constructor, initializes service layer entity for Notification System
        public NotificationSystemManager()
        {
            _notificationSystemService = new NotificationSystemService();
        }

        public bool IsTimeToSendEmailNotifications()
        {
            bool result = false;
            DateTime currentTime = DateTime.UtcNow;
            
            if (currentTime.Hour >= 22)
            {
                result = true;
            }

            return result;
        }

        public IList<NotificationSystemInAppModel> RetrieveRegisteredEvents(string username) 
        {
            return _notificationSystemService.FetchRegisteredEvents(username);
        }

    }
}