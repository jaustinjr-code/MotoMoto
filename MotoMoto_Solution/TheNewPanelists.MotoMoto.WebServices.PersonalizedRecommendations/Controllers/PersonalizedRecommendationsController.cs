using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.WebServices.PersonalizedRecommendations.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class PersonalizedRecommendationsController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }
    }
}
