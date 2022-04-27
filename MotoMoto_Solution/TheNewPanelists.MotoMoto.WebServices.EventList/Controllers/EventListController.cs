using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.EventList.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventListController : Controller
    {
        // Create a private readonly DAO for Event List
        private readonly EventPostContentDataAccess _eventPostContentDataAccess = new EventPostContentDataAccess();

        // Used to display the Index view of the project
        [HttpOptions]
        public IActionResult Index()
        {
            return View(); // Display the view
        }

        // Web API call to fetch EventPostModel data from the data store and display it in the Frontend
        [HttpGet]
        [Route("GetEvents")]
        public IActionResult FetchAllEventPosts(int evntID)
        {
            // Create dependency objects before performing operation
            // Create Service and Manager objects for EventList
            EventListService eventListService = new EventListService(_eventPostContentDataAccess);
            EventListManager eventListManager = new EventListManager(eventListService);

            try
            {
                // Make a call to the Event List Manager
                ISet<EventDetailsModel> fetchedAllEvents = eventListManager.FetchAllEventDetails(evntID);
                // Return the fetched EventDetails Model
                return Ok(fetchedAllEvents);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
