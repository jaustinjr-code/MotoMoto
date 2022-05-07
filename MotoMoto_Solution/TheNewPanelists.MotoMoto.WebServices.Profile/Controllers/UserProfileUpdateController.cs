using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.WebServices.Profile.Controllers
{
    [Route("ProfileUpdate")]
    public class UserProfileUpdateController : Controller
    {
        private readonly IProfileDataAccess _profileDAO = new ProfileManagementDataAccess();
        /// <summary>
        /// Updates user profile and takes in the input of the username and new username.
        /// All logic will be identified in the manager layer where inputs of username are checked
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newUsername"></param>
        /// <returns></returns>
        [HttpPut("UsernameUpdate")]
        public IActionResult UpdateUserProfileUsername(string username, string newUsername)
        {
            IProfileManagementService profileService = new ProfileManagementService(_profileDAO);
            IProfileManagementManager profileManager = new ProfileManagementManager(profileService);

            var userProfile = new ProfileModel();
            try
            {
                userProfile = profileManager.UpdateProfileUsernameManager(username, newUsername);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return Ok(userProfile);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPut("DescriptionUpdate")]
        public IActionResult UpdateUserProfileDescription(string username, string description)
        {
            IProfileManagementService profileService = new ProfileManagementService(_profileDAO);
            IProfileManagementManager profileManager = new ProfileManagementManager(profileService);

            var userProfile = new ProfileModel();
            try
            {
                userProfile = profileManager.UpdateProfileDescriptionManager(username, description);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return Ok(userProfile);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="imageURL"></param>
        /// <returns></returns>
        [HttpPut("ImageUpdate")]
        public IActionResult UpdateUserProfileImage(string username, string imageURL)
        {
            IProfileManagementService profileService = new ProfileManagementService(_profileDAO);
            IProfileManagementManager profileManager = new ProfileManagementManager(profileService);

            var userProfile = new ProfileModel();
            try
            {
                userProfile = profileManager.UpdateProfileImageManager(username, imageURL);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return Ok(userProfile);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPut("StatusUpdate")]
        public IActionResult UpdateUserProfileStatus(string username, bool status)
        {
            IProfileManagementService profileService = new ProfileManagementService(_profileDAO);
            IProfileManagementManager profileManager = new ProfileManagementManager(profileService);

            var userProfile = new ProfileModel();
            try
            {
                userProfile = profileManager.UpdateProfileStatusManager(username, status);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return Ok(userProfile);
        }

    }
}
