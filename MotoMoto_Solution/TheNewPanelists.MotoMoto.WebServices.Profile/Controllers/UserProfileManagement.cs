using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.WebServices.Profile.Controllers
{
    [Route("ProfileManagement")]
    public class UserProfileManagement : Controller
    {
        private readonly IProfileDataAccess _profileDAO = new ProfileManagementDataAccess();
        /// <summary>
        /// Functionality generates all new users who create accounts. This functionality allows
        /// the system to create profiles in bulk as all users who are in the users table will generate
        /// a blank profile.
        /// </summary>
        /// <returns></returns>
        [HttpPut("Generate")]
        public IActionResult GenerateProfilesForNewUsers()
        {
            IProfileManagementService profileService = new ProfileManagementService(_profileDAO);
            IProfileManagementManager profileManager = new ProfileManagementManager(profileService);

            ProfileModel profile = new ProfileModel();
            try
            {
                profile = profileManager.CreateProfilesForAllNewAccountsManager();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return Ok(profile);
        }
        /// <summary>
        /// deletes a user profile from the system. All child dependencies must be deleted before 
        /// this action can take place. i.e. Posts must be removed from the DS since we run into the
        /// issue of dependencies and other features use profile.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpDelete("DeleteProf")]
        public IActionResult DeleteProfile(string username)
        {
            IProfileManagementService profileService = new ProfileManagementService(_profileDAO);
            IProfileManagementManager profileManager = new ProfileManagementManager(profileService);

            ProfileModel profile = new ProfileModel();
            try
            {
                profile = profileManager.DeleteProfileManager(username);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return Ok(profile);
        }
    }
}
