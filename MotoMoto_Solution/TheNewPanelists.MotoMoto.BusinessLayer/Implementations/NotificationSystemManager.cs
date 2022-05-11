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
        /// Validates the user input, if valid calls 
        /// then return the list response model of events RetrieveRegisteredEvents
        /// else return to controller
        /// </summary>
        /// <returns>Return a list from RetrieveRegisteredEvents</returns>
        public List<NotificationSystemResponseModel> ValidateUserInputs(NotificationSystemRequestModel requestModel)
        {
            NotificationSystemResponseModel responseModel = new NotificationSystemResponseModel();
            List<NotificationSystemResponseModel> dataList = new List<NotificationSystemResponseModel>();

            if (String.IsNullOrEmpty(requestModel.username))
            {
                responseModel.notificationSystemStatusMessage = "INVALID USER INPUTS";

                dataList.Add(responseModel);

                return dataList;
            }

            return RetrieveRegisteredEvents(requestModel, responseModel);
        }
 
        /// <summary>
        /// Retrieve registered events by calling  FetchRegisteredEvents from the service layer
        /// then return the response model list
        /// </summary>
        /// <returns>Return a list with all the fetched data of registered events from data access layer</returns>
        public List<NotificationSystemResponseModel> RetrieveRegisteredEvents(NotificationSystemRequestModel requestModel, NotificationSystemResponseModel responseModel)
        {
            responseModel.notificationSystemStatusMessage = "USER INPUTS ACCEPTED";

            return _notificationSystemService.FetchRegisteredEvents(requestModel);
        }
    }
}