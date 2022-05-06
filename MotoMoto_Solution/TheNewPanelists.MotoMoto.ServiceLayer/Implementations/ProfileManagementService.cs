using System;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;
using MySql.Data.MySqlClient;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class ProfileManagementService
    {
        private readonly IProfileDataAccess profileDataAccess;

        public ProfileManagementService(IProfileDataAccess _profileDataAccess)
        {
            profileDataAccess = _profileDataAccess;
        }
        /// <summary>
        /// functionality passes in the validated username from the business layer
        /// and converts the string into a profile. Once passed through to the data
        /// access. Information should return with a response set to success or failure
        /// and passed back to the business layer for evaluation.
        /// </summary>
        /// <param name="_username"></param>
        /// <returns></returns>
        public ProfileModel RetrieveSpecifiedProfileEntity(string _username)
        {
            ProfileModel profileModel = new ProfileModel();
            try
            {
                profileModel.username = _username;
                profileModel = profileDataAccess.RetrieveSpecifiedProfileEntity(profileModel);
            }
            catch
            {
                if (profileModel.username != _username)
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
                profileList = profileDataAccess.GetAllProfiles(profileList);
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
        public ProfileModel RetrieveAllUpvotesPostsForProfile(string _username)
        {
            ProfileModel profile = new ProfileModel();
            try
            {
                profile.username = _username;
                profile = profileDataAccess.RetrieveAllUpvotesPostsForProfile(profile);
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
        public ProfileModel UpdateProfileDescriptionService(string _username, string _newDescription)
        {
            ProfileModel profileModel = new ProfileModel();
            try
            {
                profileModel.username = _username;
                profileModel.profileDescription = _newDescription;
                profileModel = profileDataAccess.UpdateProfileDescription(profileModel);
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
        public ProfileModel UpdateProfileUsernameService(string _username, string _newUsername)
        {
            ProfileModel profileModel = new ProfileModel();
            try
            {
                profileModel.username = _username;
                profileModel.newProfileUsername = _newUsername;
                profileDataAccess.UpdateProfileUsername(profileModel);
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
        public ProfileModel UpdateProfileStatus(string _username, bool _status)
        {
            ProfileModel profileModel = new ProfileModel();
            try
            {
                profileModel.username = _username;
                profileModel.status = _status;
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.serviceObjectFailOnRetrievalFromDataAccess);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
    }
}