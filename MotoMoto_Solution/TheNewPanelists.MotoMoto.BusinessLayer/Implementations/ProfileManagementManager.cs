using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class ProfileManagementManager : IProfileManagementManager
    {
        private readonly IProfileManagementService _profileManagementService;

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
        /// 
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
        /// 
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
                if (_username.Length >= 25 && _username.Length < 0)
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
        /// 
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

                if (profileModel != null)
                {
                    if (profileModel.profileDescription == null && profileModel.profileDescription?.Length == 0)
                    {
                        return profileModel.GetResponse(ResponseModel.response.managerInvalidProfileDescriptionRetrieval);
                    }
                    else if (profileModel.username != _username)
                    {
                        return profileModel.GetResponse(ResponseModel.response.managerInvalidProfileUsernameRetrieval);
                    }
                }
                else
                {
                    return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                }
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// 
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
                if (_username.Length >= 25 && _username.Length < 0)
                    return profileModel.GetResponse(ResponseModel.response.managerInvalidString);
                if (_newDescription.Length < 0 && _newDescription.Length > 150)
                    return profileModel.GetResponse(ResponseModel.response.invalidIntegerParameter);

                profileModel = _profileManagementService.UpdateProfileDescriptionService(profileModel);

                if (profileModel != null)
                {
                    if (profileModel.systemResponse != "success")
                        return profileModel.GetResponse(ResponseModel.response.managerInvalidObject);
                }
                else
                {
                    return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                }
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// 
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
                if (_username.Length >= 25 && _username.Length < 0 && _newUsername.Length >= 25 && _newUsername.Length < 0)
                    return profileModel.GetResponse(ResponseModel.response.invalidStringParameter);

                profileModel = _profileManagementService.UpdateProfileUsernameService(profileModel);

                if (profileModel != null)
                {
                    if (profileModel.systemResponse != "success")
                        return profileModel.GetResponse(ResponseModel.response.managerInvalidObject);
                }
                else
                {
                    return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                }
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// 
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
                if (profileModel != null)
                {
                    if (profileModel.systemResponse != "success")
                        return profileModel.GetResponse(ResponseModel.response.managerInvalidObject);
                }
                else
                {
                    return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                }
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// 
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
                if (_username.Length >= 25 && _username.Length < 0)
                    return profileModel.GetResponse(ResponseModel.response.invalidStringParameter);

                profileModel = _profileManagementService.DeleteProfileService(profileModel);
                if (profileModel != null)
                {
                    if (profileModel.systemResponse != "success")
                        return profileModel.GetResponse(ResponseModel.response.managerInvalidObject);
                }
                else
                {
                    return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
                }
            }
            catch
            {
                return profileModel.GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// 
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
                if (profileModel != null)
                {
                    if (profileModel.systemResponse != "success")
                        return profileModel.GetResponse(ResponseModel.response.managerInvalidObject);
                }
                else
                {
                    return new ProfileModel().GetResponse(ResponseModel.response.nullObjectReferenceAchieved);
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
