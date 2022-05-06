using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using TheNewPanelists.MotoMoto.DataStoreEntities.PersonalizedRecommendations;
using TheNewPanelists.MotoMoto.ServiceLayer.Implementations;

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
        public IActionResult UpdatePreferences(int userId, string countries, string makes, string models)
        {
            PersonalizedRecommendationsService personalizedRecommendationsService = new PersonalizedRecommendationsService();
            DataStoreRequestPreferences requestPreferences = new DataStoreRequestPreferences()
            {
                followedCountries = JsonSerializer.Deserialize<List<Country>>(countries),
                followedMakes = JsonSerializer.Deserialize<List<Make>>(makes),
                followedModels = JsonSerializer.Deserialize<List<Model>>(models)
            };

            personalizedRecommendationsService.UpdateUserPreferences(userId, ref requestPreferences);
            return Ok(requestPreferences);
        }

        [HttpGet("Retrieve")]
        public IActionResult RetrievePreferences(int userId)
        {
            PersonalizedRecommendationsService personalizedRecommendationsService = new PersonalizedRecommendationsService();
            DataStoreRequestPreferences requestPreferences = personalizedRecommendationsService.GetUserPreferences(userId);

            if(requestPreferences.status)
                return Ok(requestPreferences);
            return BadRequest(requestPreferences);

        }
    }
}
