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
    }
}
