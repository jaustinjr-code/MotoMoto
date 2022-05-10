using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using Microsoft.AspNetCore.Cors;

namespace TheNewPanelists.MotoMoto.WebServices.Profile.Controllers
{
    [ApiController]
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
        [HttpGet("UsernameUpdate")]
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
        /// Update profile description allows users to change their description that is displayed
        /// onto their pages. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpGet("DescriptionUpdate")]
        public IActionResult UpdateUserProfileDescription(string username, string newDescription)
        {
            IProfileManagementService profileService = new ProfileManagementService(_profileDAO);
            IProfileManagementManager profileManager = new ProfileManagementManager(profileService);

            var userProfile = new ProfileModel();
            try
            {
                userProfile = profileManager.UpdateProfileDescriptionManager(username, newDescription);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return Ok(userProfile);
        }
        /// <summary>
        /// Update profile image is used to update a profile image and change their user profile image on
        /// thier respective user profile pages
        /// </summary>
        /// <param name="username"></param>
        /// <param name="imageURL"></param>
        /// <returns></returns>
        [HttpGet("ImageUpdate")]
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
        /// Function is used to update a profile and change the status. This functionality is morely 
        /// used for usermanagement for accounts that get banned and it is a decision made by an admin
        /// </summary>
        /// <param name="username"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("StatusUpdate")]
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
