using System;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;
using MySql.Data.MySqlClient;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class ProfileManagementService : IProfileManagementService
    {
        private readonly IProfileDataAccess _profileDataAccess;

        public ProfileManagementService(IProfileDataAccess profileDataAccess)
        {
            _profileDataAccess = profileDataAccess;
        }
        /// <summary>
        /// function executes all new users that are created and generates a profile
        /// for each user that is not in the profile table. This functionality allows
        /// the system to dump in all new profiles to generate without needing to indivdually
        /// create profiles.
        /// </summary>
        /// <returns></returns>
        public ProfileModel CreateProfilesForAllNewUsersManager()
        {
            ProfileModel profileModel = new ProfileModel();
            try
            {
                profileModel = _profileDataAccess.InsertAllExsitingUsers();
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.serviceObjectCreationFailure);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// functionality passes in the validated username from the business layer
        /// and converts the string into a profile. Once passed through to the data
        /// access. Information should return with a response set to success or failure
        /// and passed back to the business layer for evaluation.
        /// </summary>
        /// <param name="_username"></param>
        /// <returns></returns>
        public ProfileModel RetrieveSpecifiedProfileEntity(ProfileModel _profileModel)
        {
            ProfileModel profileModel = _profileModel;
            try
            {
                profileModel = _profileDataAccess.RetrieveSpecifiedProfileEntity(profileModel);
            }
            catch
            {
                if (profileModel.username != _profileModel.username)
                    return profileModel.GetResponse(ResponseModel.response.serviceObjectFailOnRetrievalFromDataAccess);
                return profileModel.GetResponse(ResponseModel.response.serviceObjectCreationFailure);

            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Admin functionality that allows the system to retrieve all users. This functionality is mostly used 
        /// during the display of profiles and how many active users status are currently true
        /// </summary>
        /// <returns></returns>
        public ProfileListModel RetrieveAllProfileModels()
        {
            ProfileListModel profileList = new ProfileListModel();
            try
            {
                profileList = _profileDataAccess.GetAllProfiles(profileList);
            }
            catch
            {
                if (profileList.profiles == null)
                    return profileList.GetResponse(ResponseModel.response.serviceObjectFailOnRetrievalFromDataAccess);
                return profileList.GetResponse(ResponseModel.response.serviceObjectCreationFailure);
            }
            return profileList.GetResponse(ResponseModel.response.success);
            
        }
        /// <summary>
        /// Initializes a model on passing cases from the manager layer and properly formulates the model to 
        /// send to the datastore. From the datastore we retrieve an object where on passing results we return 
        /// our success model
        /// </summary>
        /// <param name="_username"></param>
        /// <returns></returns>
        public ProfileModel RetrieveAllUpvotesPostsForProfile(ProfileModel _profileModel)
        {
            ProfileModel profile = _profileModel;
            try
            {
                profile = _profileDataAccess.RetrieveAllUpvotesPostsForProfile(profile);
            }
            catch
            {
                if (profile.upVotedPosts == null)
                    return profile.GetResponse(ResponseModel.response.serviceObjectFailOnRetrievalFromDataAccess);
                return new ProfileModel().GetResponse(ResponseModel.response.serviceObjectCreationFailure);
            }
            return profile.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Initializs the model for the use and updates a profile description by calling data access.
        /// Retrieves the model back from data access on success.
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_newDescription"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileDescriptionService(ProfileModel _profileModel)
        {
            ProfileModel profileModel = _profileModel;
            try
            {
                profileModel = _profileDataAccess.UpdateProfileDescription(profileModel);
            }
            catch
            { 
                return profileModel.GetResponse(ResponseModel.response.serviceObjectFailOnRetrievalFromDataAccess);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Initializes a profile model and initializes the new profile username where the DAL passes back the object
        /// with the response object determining whether the return object was valid or not
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_newUsername"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileUsernameService(ProfileModel _profileModel)
        {
            ProfileModel profileModel = _profileModel;
            try
            {
                _profileDataAccess.UpdateProfileUsername(profileModel);
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.serviceObjectFailOnRetrievalFromDataAccess);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Updates a profile status to determine whether a user has deactivated their account. This functionality 
        /// works hand in hand with user management so a user can come back and log back into their profile without
        /// having to worry about their initial profile being lost.
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_status"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileStatus(ProfileModel _profileModel)
        {
            ProfileModel profileModel = _profileModel;
            try
            {
                profileModel = _profileDataAccess.UpdateProfileDescription(_profileModel);
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.serviceObjectFailOnRetrievalFromDataAccess);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partModel"></param>
        /// <returns></returns>
        public ProfileModel DeleteProfileService(ProfileModel _profileModel)
        {
            ProfileModel profileModel = _profileModel;
            try
            {
                profileModel = _profileDataAccess.DeleteProfileDataAccess(_profileModel);
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.serviceObjectFailOnRetrievalFromDataAccess);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileModel"></param>
        /// <returns></returns>
        public ProfileModel RetrieveSpecifiedUserPosts(ProfileModel profileModel)
        {
            try
            {
                profileModel = _profileDataAccess.GetAllUsersPosts(profileModel);
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.serviceObjectCreationFailure);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
    }
}