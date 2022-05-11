using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.NotificationSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class NotificationSystemController : ControllerBase
    {
        private readonly NotificationSystemDataAccess _notificationSystemDataAccess = new NotificationSystemDataAccess();

        [Route("GetRegisteredEventDetails")]
        public List<NotificationSystemResponseModel> FetchRegisteredEvents(NotificationSystemRequestModel requestModel)
        {
            NotificationSystemService emailService = new NotificationSystemService();
            
            emailService.SendNotificationEmail();
            
            NotificationSystemManager notificationSystemManager = new NotificationSystemManager();
            List<NotificationSystemResponseModel> registeredEventsList = new List<NotificationSystemResponseModel>();
            registeredEventsList = notificationSystemManager.ValidateUserInputs(requestModel);

            return registeredEventsList;
        }
    }
}
