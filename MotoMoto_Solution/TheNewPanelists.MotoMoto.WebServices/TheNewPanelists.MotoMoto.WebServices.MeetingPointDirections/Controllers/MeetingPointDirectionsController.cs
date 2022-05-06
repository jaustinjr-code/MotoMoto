using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.MeetingPointDirections
{
    [Route("[controller]")]
    [ApiController]
    public class MeetingPointDirectionsController : Controller
    {
        private readonly MeetingPointDirectionsDataAccess _meetingPointDirectionsDataAccess = new MeetingPointDirectionsDataAccess();

        public IActionResult Index()
        {
            return View();
        }

        // Web API call to fetch the selected event location from the data store and display it in the Frontend
        //[Route("GetEventLocation")]
        [HttpGet("GetEventLocation")]
        public IActionResult FetchEventLocation(int eventID)
        {
            // Create dependency objects before performing operation
            MeetingPointDirectionsService meetingPointDirectionsService = new MeetingPointDirectionsService(_meetingPointDirectionsDataAccess);
            MeetingPointDirectionsManager meetingPointDirectionsManager = new MeetingPointDirectionsManager(meetingPointDirectionsService);

            try
            {
                ISet<EventDetailsModel>? fetchedEventLocation = meetingPointDirectionsManager.FetchEventLocation(eventID); 
                return Ok(fetchedEventLocation);
            }
            catch
            {
                // Log here
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
