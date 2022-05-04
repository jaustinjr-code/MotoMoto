using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.DataAccess;

namespace TheNewPanelists.MotoMoto.WebServices.PersonalizedRecommendations.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class PreferencesController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Update")]
        public IActionResult UpdatePreferences()
        {
            return Ok("ok");
        }

        [HttpGet("Retrieve")]
        public IActionResult RetrievePreferences()
        {
            return Ok("ok");
        }
    }
}
