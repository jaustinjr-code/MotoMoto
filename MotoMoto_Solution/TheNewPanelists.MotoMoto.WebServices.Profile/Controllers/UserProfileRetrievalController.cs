using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.WebServices.Profile.Controllers
{
    [Route("ProfileRetrieval")]
    public class UserProfileController : Controller
    {
        private readonly IProfileDataAccess _profileDAO = new ProfileManagementDataAccess();

        /// <summary>
        /// Retrieves a user profile and returns all base information on a profile. This API call
        /// will not retain the history of a user profile but will contain all associated events 
        /// that have happened in the account.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("Profile")]
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
        /// <summary>
        /// Retrieve all user upvoted posts will grab all the posts that a user has upvoted and 
        /// display them on their own respective pages. Each user will be able to follow one another
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("ProfileUpvotePosts")]
        public IActionResult RetrieveAllUserUpvotePosts(string username)
        {
            IProfileManagementService profileService = new ProfileManagementService(_profileDAO);
            IProfileManagementManager profileManager = new ProfileManagementManager(profileService);

            var userProfile = new ProfileModel();
            try
            {
                userProfile = profileManager.RetrieveAllUpVotedPostsForProfileManager(username);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return Ok(userProfile);
        }
        /// <summary>
        /// Retrieve all profiles will allow admins to control who is an active user. This is meant 
        /// for administrative access to a disable, enable, or monitor user profiles.
        /// </summary>
        /// <param name="adminUsername"></param>
        /// <returns></returns>
        [HttpGet("AllProfiles")]
        public IActionResult RetrieveAllProfiles()
        {
            IProfileManagementService profileService = new ProfileManagementService(_profileDAO);
            IProfileManagementManager profileManager = new ProfileManagementManager(profileService);

            var profileList = new ProfileListModel();
            try
            {
                profileList = profileManager.RetrieveAllProfileManager();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return Ok(profileList);
        }
        /// <summary>
        /// Retrieval of profile posts. This functionality ensures that all posts created by a specified
        /// user are taken into account and retrieved to their respective profile pages
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("GetPosts")]
        public IActionResult RetrieveProfilePosts(string username)
        {
            IProfileManagementService profileService = new ProfileManagementService(_profileDAO);
            IProfileManagementManager profileManager = new ProfileManagementManager(profileService);

            var profile = new ProfileModel();
            try
            {
                profile = profileManager.RetrieveSpecifiedUserPostsManager(username);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);    
            }
            return Ok(profile);
        }
    }
}
