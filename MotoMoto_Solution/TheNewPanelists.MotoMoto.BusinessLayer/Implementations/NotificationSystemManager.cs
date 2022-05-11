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

        public List<NotificationSystemResponseModel> ValidateUserInputs(NotificationSystemRequestModel requestModel)
        {
            NotificationSystemResponseModel responseModel = new NotificationSystemResponseModel();
            List<NotificationSystemResponseModel> dataList = new List<NotificationSystemResponseModel>();

            if (String.IsNullOrEmpty(requestModel.username))
            {
                responseModel.notificationSystemStatusMessage = "INVALID USER INPUTS";
                Console.WriteLine("from notificationManager ", requestModel.username);

                dataList.Add(responseModel);

                return dataList;
            }

            return RetrieveRegisteredEvents(requestModel, responseModel);
        }

        public List<NotificationSystemResponseModel> RetrieveRegisteredEvents(NotificationSystemRequestModel requestModel, NotificationSystemResponseModel responseModel)
        {
            responseModel.notificationSystemStatusMessage = "USER INPUTS ACCEPTED";

            return _notificationSystemService.FetchRegisteredEvents(requestModel);
        }

        

    }
}