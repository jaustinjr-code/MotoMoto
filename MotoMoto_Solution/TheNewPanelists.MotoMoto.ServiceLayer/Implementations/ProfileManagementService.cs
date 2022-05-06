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
        /// 
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
        /// 
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
        /// 
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
            catch (Exception ex)
            {
                return profileModel.GetResponse(ResponseModel.response.serviceObjectFailOnRetrievalFromDataAccess);
            }
            return profileModel.GetResponse(ResponseModel.response.success);
        }
    }
}

// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace TheNewPanelists.MotoMoto.ServiceLayer
// {
//     public class ProfileManagementService : IUserManagementService
//     {
//         private readonly ProfileManagementDataAccess? _profileManagementDAO;

//         public ProfileManagementService()
//         {
//             _profileManagementDAO = new ProfileManagementDataAccess();
//         }
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="userAccount"></param>
//         /// <returns></returns>
//         public ISet<ProfileModel> RetrieveAllProfiles(ProfileModel userProfile)
//         {
//             var accountEntities = _profileManagementDAO!.GetAllProfiles();

//             var userAccounts = accountEntities.Select(acct => new ProfileModel()
//             {
//                 username = userProfile!.username,
//                 status = userProfile!.status,
//                 eventAccount = userProfile!.eventAccount,
//             }).ToHashSet();
//             return userAccounts;
//         }
//         /// <summary>
//         /// 
//         /// </summary>
//         /// <param name="deletedProfile"></param>
//         /// <returns></returns>
//         public bool DeleteAccountProfile(DeleteAccountModel deletedProfile)
//         {
//             var dataStoreUserProfile = new DeleteAccountModel()
//             {
//                 username = deletedProfile!.username,
//                 verifiedPassword = deletedProfile!.verifiedPassword
//             };
//             return _profileManagementDAO!.DeleteProfile(dataStoreUserProfile);
//         }

//         public bool CreateExistingAccountProfiles()
//         {
//             return _profileManagementDAO!.InsertNewProfileEntity();
//         }
//         public bool UpdateProfileDescription(ProfileModel profileModel)
//         {
//             return _profileManagementDAO!.UpdateProfileDescription(profileModel);
//         }

//         public ProfileModel RetrieveSpecifiedUserProfile(ProfileModel userProfile)
//         {
//             return _profileManagementDAO!.RetrieveSpecifiedProfileEntity(userProfile);
//         }

//         public bool UpdateUserProfileUsername(ProfileModel profileModel)
//         {
//             var _dataStoreUserAccount = new AccountModel
//             {
//                 username = profileModel.username
//             };
//             return _profileManagementDAO!.UpdateProfileUsername(_dataStoreUserAccount);
//         }
//     }
// }
