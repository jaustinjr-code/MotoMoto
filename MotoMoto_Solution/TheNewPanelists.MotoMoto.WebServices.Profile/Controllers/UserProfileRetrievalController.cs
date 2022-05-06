using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.WebServices.Profile.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IProfileDataAccess _profileDAO = new ProfileManagementDataAccess();

        [HttpGet("ProfileRetrieval")]
        public IActionResult RetrieveProfile(string username)
        {
            throw new NotImplementedException();
        }
    }
}
