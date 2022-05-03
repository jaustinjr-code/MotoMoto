using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.NotificationSystem.Controllers
{
    //[EnableCors("CorsPolicy")]
    [Route("[api/controller]")]
    [ApiController]
    public class NotificationSystemController : ControllerBase
    {
        // Create a private readonly DAO for Event List
        private readonly NotificationSystemDataAccess _notificationSystemDataAccess = new NotificationSystemDataAccess();

        // Web API call to fetch EventPostModel data from the data store and display it in the Frontend
        //[HttpGet]
        [HttpGet]
        [Route("GetRegisteredEventDetails")]
        
        public IActionResult FetchRegisteredEvents()
        {

            string username = Request.QueryString["username"];
            // Create dependency objects before performing operation
            // Create Service and Manager objects for EventList
            NotificationSystemManager notificationSystemManager = new NotificationSystemManager();

            NameValueCollection urlQueryString = HttpUtility.ParseQueryString(string.Empty);
            urlQueryString.Add("registrationID", registrationRequest.RegistrationId.ToString());
            urlQueryString.Add("email", registrationRequest.Email!);

            // try
            // {
            //     // Make a call to the Event List Manager
            //     ISet<EventDetailsModel> fetchedAllEvents = eventListManager.FetchAllEventDetails();
            //     // Return the fetched EventDetails Model
            //     return Ok(fetchedAllEvents);
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            //     return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            // }
        }

    }
}
