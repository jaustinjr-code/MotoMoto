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


        /// <summary>
        /// Calls RetchRegisteredEvents from the service layer then return the list of events
        /// </summary>
        ///
        /// <param name="username">Logged-in username to receive in-app notification</param>
        ///
        /// <returns>Return a list with all the fetched data of registered events</returns>
        public IList<NotificationSystemInAppModel> RetrieveRegisteredEvents(string username) 
        {
            Console.WriteLine("NotificationSystemManager:RetrieveRegisteredEvents Hello " + username);
            return _notificationSystemService.FetchRegisteredEvents(username);
        }

    }
}