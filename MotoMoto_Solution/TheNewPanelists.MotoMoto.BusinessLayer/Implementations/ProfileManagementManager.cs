using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class ProfileManagementManager : IProfileManagementManager
    {
        private readonly IProfileManagementService _profileManagementService;

        public ProfileManagementManager()
        {
            _profileManagementService = new ProfileManagementService();
        }
        public ProfileManagementManager(IProfileManagementService profileManagementService)
        {
            _profileManagementService = profileManagementService;
        }
        /// <summary>
        /// Function should never return broken unless there are issues with user management
        /// account creation which is handled in the UM-Business Layer ** Please see UMBL **
        /// </summary>
        /// <returns></returns>
        public ProfileModel CreateProfilesForAllNewAccountsManager()
        {
            ProfileModel managerProfile = new ProfileModel();
            try
            {
                managerProfile = _profileManagementService.CreateProfilesForAllNewUsersManager();
            }
            catch
            {
                return new ProfileModel().GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return managerProfile.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Retrieves the object list of all profiles and ensures that the data is not failing on retrieval
        /// </summary>
        /// <returns></returns>
        public ProfileListModel RetrieveAllProfileManager()
        {
            ProfileListModel managerProfileList = new ProfileListModel();
            try
            {
                managerProfileList = _profileManagementService.RetrieveAllProfileModels();
            }
            catch
            {
                return new ProfileListModel().GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return managerProfileList.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Retrieves a single profile and ensures that the username length is not exceeded on return or ensures that 
        /// the specified profile has not been invalidated by the username
        /// </summary>
        /// <param name="_username"></param>
        /// <returns></returns>
        public ProfileModel RetrieveSpecifiedProfileManager(string _username)
        {
            ProfileModel profileModel = new ProfileModel
            {
                username = _username
            };
            try
            {
                if (_username.Length >= 25 && _username.Length <= 1)
                    return profileModel.GetResponse(ResponseModel.response.managerInvalidString);
                profileModel = _profileManagementService.RetrieveSpecifiedProfileEntity(profileModel);
            }
            catch
            {
                return new ProfileModel().GetResponse(ResponseModel.response.managerInvalidString);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Retrieval for all upvvoted posts first ensures that a profile username is even valid. we do checks
        /// to see that this is true and then run our service. On retrieval we determine whether a profile is 
        /// legal and its response is valid 
        /// </summary>
        /// <param name="_username"></param>
        /// <returns></returns>
        public ProfileModel RetrieveAllUpVotedPostsForProfileManager(string _username)
        {
            ProfileModel profileModel = new ProfileModel
            {
                username = _username
            };
            try
            {
                if (_username.Length > 25 && _username.Length < 0)
                    return profileModel.GetResponse(ResponseModel.response.managerInvalidString);

                profileModel = _profileManagementService.RetrieveAllUpvotesPostsForProfile(profileModel);

                switch (profileModel)
                {
                    case null:
                        return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                    default:
                        if (profileModel.profileDescription == null && profileModel.profileDescription?.Length == 0)
                        {
                            return profileModel.GetResponse(ResponseModel.response.managerInvalidProfileDescriptionRetrieval);
                        }
                        if (profileModel.username != _username)
                        {
                            return profileModel.GetResponse(ResponseModel.response.managerInvalidProfileUsernameRetrieval);
                        }
                        break;
                }
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Manager used to validate the username and description. Our goal will be to set the new user with a new username and description.
        /// Which holds the main priority of ensuring that invalid data does not pass through the manager.
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_newDescription"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileDescriptionManager(string _username, string _newDescription)
        {
            ProfileModel profileModel = new ProfileModel()
            {
                username = _username,
                profileDescription = _newDescription
            };
            try
            {
                if (_username.Length >= 25 || _username.Length < 0)
                    return profileModel.GetResponse(ResponseModel.response.managerInvalidString);
                if (_newDescription.Length < 0 || _newDescription.Length > 150)
                    return profileModel.GetResponse(ResponseModel.response.managerInvalidString);

                profileModel = _profileManagementService.UpdateProfileDescriptionService(profileModel);

                switch (profileModel)
                {
                    case null:
                        return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                    default:
                        if (profileModel.systemResponse != "success")
                            return profileModel.GetResponse(ResponseModel.response.managerInvalidObject);
                        break;
                }
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Update profile username is used to ensure that usernames are allowed to be changed this functionality
        /// is simply dependent on the usermanagement operations and is non function throughout profile.
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_newUsername"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileUsernameManager(string _username, string _newUsername)
        {
            ProfileModel profileModel = new ProfileModel
            {
                username = _username,
                newProfileUsername = _newUsername
            };
            try
            {
                if (_username.Length >= 25 || _username.Length < 0 || _newUsername.Length >= 25 || _newUsername.Length < 0)
                    return profileModel.GetResponse(ResponseModel.response.invalidStringParameter);

                profileModel = _profileManagementService.UpdateProfileUsernameService(profileModel);

                switch (profileModel)
                {
                    case null:
                        return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                    default:
                        if (profileModel.systemResponse != "success")
                            return profileModel.GetResponse(ResponseModel.response.managerInvalidObject);
                        break;
                }
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Updates profile status, this is an admin controlled functionality that is used to disable profiles that are either
        /// unauthenticated after specified amount of days or need to be reactivated.
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_status"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileStatusManager(string _username, bool _status)
        {
            ProfileModel profileModel = new ProfileModel
            {
                username = _username,
                status = _status
            };
            try
            {
                if (_username.Length >= 25 && _username.Length < 0)
                    return profileModel.GetResponse(ResponseModel.response.invalidStringParameter);

                profileModel = _profileManagementService.UpdateProfileStatus(profileModel);
                
                switch (profileModel)
                {
                    case null :
                        return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                    default :
                        if (profileModel.systemResponse != "success")
                            return profileModel.GetResponse(ResponseModel.response.managerInvalidObject);
                        break;
                }
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Functionality is simply used to delete dependencies to get to profile. This function is simply reliant on 
        /// other actions before it is to take place.
        /// </summary>
        /// <param name="_username"></param>
        /// <returns></returns>
        public ProfileModel DeleteProfileManager(string _username)
        {
            ProfileModel profileModel = new ProfileModel
            {
                username = _username
            };
            try
            {
                if (_username.Length >= 25 || _username.Length < 0)
                    return profileModel.GetResponse(ResponseModel.response.invalidStringParameter);

                profileModel = _profileManagementService.DeleteProfileService(profileModel);

                switch (profileModel)
                {
                    case null:
                        return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                    default:
                        if (profileModel.systemResponse != "success")
                            return profileModel.GetResponse(ResponseModel.response.managerInvalidObject);
                        break;
                }
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// This function is used to retrieve all specified posts made by a user. Simply, this function is useful to retrieve all 
        /// posts made by a user that are sent to the community board.
        /// </summary>
        /// <param name="_username"></param>
        /// <returns></returns>
        public ProfileModel RetrieveSpecifiedUserPostsManager(string _username)
        {
            ProfileModel profileModel = new ProfileModel
            {
                username = _username
            };
            try
            {
                if (_username.Length >= 25 && _username.Length < 0)
                    return profileModel.GetResponse(ResponseModel.response.invalidStringParameter);

                profileModel = _profileManagementService.RetrieveSpecifiedUserPosts(profileModel);

                switch (profileModel)
                {
                    case null:
                        return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                    default:
                        if (profileModel.systemResponse != "success")
                            return profileModel.GetResponse(ResponseModel.response.managerInvalidObject);
                        break;
                }
            }
            catch
            {
                return new ProfileModel().GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Update profile image will be used to change the profile image. Since we are not storing images, the best solution 
        /// is to allow users to upload a online url but this can be flagged as copyright so changes will need to be made
        /// to support uploads.
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_imageURL"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileImageManager(string _username, string _imageURL)
        {
            ProfileModel profileModel = new ProfileModel
            {
                username = _username,
                profileImagePath = _imageURL
            };
            try
            {
                if (_username.Length > 25 || _username.Length <= 0)
                    return profileModel.GetResponse(ResponseModel.response.invalidStringParameter);
                if (_imageURL.Length > 260 || _imageURL.Length <= 0)
                    return profileModel.GetResponse(ResponseModel.response.invalidStringParameter);

                profileModel = _profileManagementService.UpdateProfileImage(profileModel);

                switch (profileModel)
                {
                    case null:
                        return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                    default:
                        if (profileModel.systemResponse != "success")
                            return profileModel.GetResponse(ResponseModel.response.managerInvalidObject);
                        break;
                }
            }
            catch
            {
                return new ProfileModel().GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
    }
}
