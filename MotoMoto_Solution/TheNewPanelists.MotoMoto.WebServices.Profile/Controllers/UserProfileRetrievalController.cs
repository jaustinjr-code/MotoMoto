using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.WebServices.Profile.Controllers
{
    [Route("[controller]")]
    public class UserProfileController : Controller
    {
        private readonly IProfileDataAccess _profileDAO = new ProfileManagementDataAccess();

        [HttpGet]
        public IActionResult RetrieveUserProfile(string username)
        {
            IProfileManagementService profileService = new ProfileManagementService(_profileDAO);
            IProfileManagementManager profileManager = new ProfileManagementManager(profileService);

            var userProfile = new ProfileModel();
            try
            {
                userProfile = profileManager.RetrieveSpecifiedProfileManager(username);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return Ok(userProfile);
        }
    }
}
