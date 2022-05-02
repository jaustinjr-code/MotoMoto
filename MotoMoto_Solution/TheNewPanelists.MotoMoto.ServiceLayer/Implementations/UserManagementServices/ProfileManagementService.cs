// using System;
// using System.Collections.Generic;
// using System.Linq;
// using TheNewPanelists.MotoMoto.DataAccess;
// using TheNewPanelists.MotoMoto.Models;
// using TheNewPanelists.MotoMoto.DataStoreEntities;
// using System.Data;

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
