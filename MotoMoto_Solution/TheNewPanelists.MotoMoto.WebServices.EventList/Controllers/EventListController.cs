using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace TheNewPanelists.MotoMoto.WebServices.EventList.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventListController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }

        // Web API call to fetch the EventPostModel and display it in the Frontend
        [HttpGet]
        [Route("GetEvents")]
        public IActionResult FetchAllPosts()
        {
            try
            {
                // Make a call to EventPostModel

                // Return the fetched EventPostModel
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
