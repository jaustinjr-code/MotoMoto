using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.MeetingPointDirections.Controllers
{
    public class MeetingPointDirectionsController : Controller
    {

        // Create a private readonly DAO for Event List
        private readonly EventPostContentDataAccess _eventPostContentDataAccess = new EventPostContentDataAccess();

        public IActionResult Index()
        {
            return View();
        }

        // Web API call to fetch the selected event location from the data store and display it in the Frontend
        //[HttpGet]
        [Route("GetEventLocation")]
        public IActionResult FetchEventLocation()
        {
            // Create dependency objects before performing operation
            // Create Service and Manager objects for EventList
            EventListService eventListService = new EventListService(_eventPostContentDataAccess);
            EventListManager eventListManager = new EventListManager(eventListService);

            try
            {
                // Make a call to the Event List Manager
                ISet<EventDetailsModel> fetchedEventLocation = eventListManager.FetchAllEventDetails(); // ONLY FETCHING 1 ROW -> SET NOT NEEDED?
                // Return the fetched EventDetails Model
                return Ok(fetchedEventLocation);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError); // ADD ERROR MESSAGE HERE
            }
        }

    }
}
