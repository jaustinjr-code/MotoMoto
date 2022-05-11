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
    [Route("api/[controller]")]

    public class NotificationSystemController : ControllerBase
    {
        // Create a private readonly DAO for Event List
        private readonly NotificationSystemDataAccess _notificationSystemDataAccess = new NotificationSystemDataAccess();

        // Web API call to fetch EventPostModel data from the data store and display it in the Frontend
        //[HttpGet]
        //[HttpGet("GetNotification")]
        // [HttpGet]
        [Route("GetRegisteredEventDetails")]
        public List<NotificationSystemResponseModel> FetchRegisteredEvents(string username)
        {
            Console.WriteLine("NotificationSystemController:FetchRegisteredEvents Hello " + username);

            // Create dependency objects before performing operation
            // Create Service and Manager objects for EventList
            NotificationSystemManager notificationSystemManager = new NotificationSystemManager();
            List<NotificationSystemResponseModel> registeredEventsList = new List<NotificationSystemResponseModel>();
            registeredEventsList = notificationSystemManager.ValidateUserInputs(username);
            Console.WriteLine("after manager call");
            return ;
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
