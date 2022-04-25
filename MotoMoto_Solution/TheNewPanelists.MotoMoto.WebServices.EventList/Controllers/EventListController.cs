using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.MotoMoto.WebServices.EventList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventListController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetMessage")]
        public IActionResult GetPosts()
        {
            try
            {
                // TODO
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
