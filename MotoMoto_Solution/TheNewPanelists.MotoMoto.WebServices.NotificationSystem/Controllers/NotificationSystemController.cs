using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.NotificationSystem.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    [ApiController]
    [Route("[controller]")]

    public class NotificationSystemController : ControllerBase
    {
        // Create a private readonly DAO for Event List
        private readonly NotificationSystemDataAccess _notificationSystemDataAccess = new NotificationSystemDataAccess();

        // Web API call to fetch EventPostModel data from the data store and display it in the Frontend
        //[HttpGet]
        //[HttpGet("GetNotification")]
        // [HttpGet]
        [Route("GetRegisteredEventDetails")]
        public List<NotificationSystemResponseModel> FetchRegisteredEvents(NotificationSystemRequestModel requestModel)
        {
            Console.WriteLine("controller");
            Console.WriteLine(requestModel.notificationType);
            // NotificationSystemRequestModel requestModel = new NotificationSystemRequestModel();
            NotificationSystemService emailService = new NotificationSystemService();
            emailService.SendNotificationEmail();
            // requestModel.username = username;
            // requestModel.notificationType = type;
            //requestModel.notificationType = type;
            NotificationSystemManager notificationSystemManager = new NotificationSystemManager();
            List<NotificationSystemResponseModel> registeredEventsList = new List<NotificationSystemResponseModel>();
            registeredEventsList = notificationSystemManager.ValidateUserInputs(requestModel);

            return registeredEventsList;
        }
        
        // [HttpPost("DeleteNotification")]
        // public IActionResult RemoveNotification(int eventID, string username)
        // {
        //     NotificationSystemManager  notificationSystemManager = new NotificationSystemManager();
        //     notificationSystemManager.RemoveNotification(eventID, username);
        //     return Ok();
        // }

    }
}
