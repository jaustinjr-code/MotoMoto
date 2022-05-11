using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.EventList.Controllers
{
    //[EnableCors("CorsPolicy")]
    [Route("[controller]")]
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
        //[HttpGet]
        [HttpGet, Route("GetEvents")]
        public IActionResult FetchAllEventPosts()
        {
            // Create dependency objects before performing operation
            // Create Service and Manager objects for EventList
            EventListService eventListService = new EventListService(_eventPostContentDataAccess);
            EventListManager eventListManager = new EventListManager(eventListService);

            try
            {
                // Make a call to the Event List Manager
                ISet<EventDetailsModel> fetchedAllEvents = eventListManager.FetchAllEventDetails();
                // Return the fetched EventDetails Model
                return Ok(fetchedAllEvents);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // Gets the user input retrieved from the frontend and stores it within the datastore
        [HttpGet, Route("CreateEvent")]
        public IActionResult CreateEventPost(string time, string date, string streetAddress, string city, string state, string country, string zipCode, string title)
        {
            EventListService eventListService = new EventListService(_eventPostContentDataAccess);
            EventListManager eventListManager = new EventListManager(eventListService);

            try
            {
                // Make a call to the Event List Manager
                EventDetailsModel postedEvent = eventListManager.CreateEventPost(time, date, streetAddress, city, state, country, zipCode, title);
                // Return the fetched EventDetails Model
                return Ok(postedEvent);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet, Route("GetAllEventAccounts")]
        public IActionResult FetchAllEventAccounts()
        {
            EventListService eventListService = new EventListService(_eventPostContentDataAccess);
            EventListManager eventListManager = new EventListManager(eventListService);

            try
            {
                // Make a call to the Event List Manager
                ISet<ProfileModel> fetchedAllEventAccounts = eventListManager.FetchAllEventAccounts();
                // Return the fetched EventDetails Model
                return Ok(fetchedAllEventAccounts);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        // Gets the user input retrieved from the frontend and stores it within the datastore
        [HttpGet, Route("CreateReview")]
        public IActionResult CreateReview(string username, int rating, string review)
        {
            EventListService eventListService = new EventListService(_eventPostContentDataAccess);
            EventListManager eventListManager = new EventListManager(eventListService);

            try
            {
                // Make a call to the Event List Manager
                EventAccountVerificationModel postedReview = eventListManager.CreateReview(username, rating, review);
                // Return the fetched EventDetails Model
                return Ok(postedReview);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet, Route("GetAllReviews")]
        public IActionResult FetchAllReviews(string username)
        {
            EventListService eventListService = new EventListService(_eventPostContentDataAccess);
            EventListManager eventListManager = new EventListManager(eventListService);

            try
            {
                // Make a call to the Event List Manager
                ISet<EventAccountVerificationModel> fetchedAllReviews= eventListManager.FetchAllReviews(username);
                // Return the fetched EventDetails Model
                return Ok(fetchedAllReviews);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
