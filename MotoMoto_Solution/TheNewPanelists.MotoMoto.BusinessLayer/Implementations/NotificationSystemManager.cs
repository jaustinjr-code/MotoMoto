using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class NotificationSystemManager
    {
        private readonly NotificationSystemService _notificationSystemService;
        // Default constructor, initializes service layer for Notification System
        public NotificationSystemManager()
        {
            _notificationSystemService = new NotificationSystemService();
        }


        /// <summary>
        /// Calls RetchRegisteredEvents from the service layer then return the list of events
        /// </summary>
        ///
        /// <param name="username">Logged-in username to receive in-app notification</param>
        ///
        /// <returns>Return a list with all the fetched data of registered events</returns>
        public List<NotificationSystemInAppModel> RetrieveRegisteredEvents(string username) 
        {
            Console.WriteLine("NotificationSystemManager:RetrieveRegisteredEvents Hello " + username);
            List<NotificationSystemInAppModel> list;
            list = _notificationSystemService.FetchRegisteredEvents(username);
            //Console.WriteLine("return from business" + list[0].eventStreetAddress);
            return list;
        }

        public bool RemoveNotification(int eventID, string username)
        {
            if (eventID == 0 || username == null)
            {
                return false;
            }
            else
            {
                return (_notificationSystemService.DeleteNotification(eventID, username));
            }
        }


        // public bool IsTimeToSendEmailNotifications()
        // {
        //     bool result = false;
        //     DateTime currentTime = DateTime.UtcNow;
            
        //     if (currentTime.Hour >= 22)
        //     {
        //         result = true;
        //     }

        //     return result;
        // }

    }
}