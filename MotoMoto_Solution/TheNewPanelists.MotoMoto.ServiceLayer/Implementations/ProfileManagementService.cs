using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data.SqlClient;
using System.Data;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class ProfileManagementService : IUserManagementService
    {
        private readonly ProfileManagementDataAccess? _profileManagementDAO;

        public ProfileManagementService()
        {
            _profileManagementDAO = new ProfileManagementDataAccess();
        }

        public ISet<ProfileModel> RetrieveAllProfiles(ProfileModel userAccount)
        {
            var accountEntities = _profileManagementDAO!.GetAllProfiles();

            var userAccounts = accountEntities.Select(acct => new ProfileModel()
            {
                username = userAccount!.username,
                status = userAccount!.status,
                eventAccount = userAccount!.eventAccount,
            }).ToHashSet();
            return userAccounts;
        }

        public bool CreateAccountProfile(DeleteAccountModel deletedProfile)
        {
            var dataStoreUserProfile = new DeleteAccountModel()
            {
                username = deletedProfile!.username,
                verifiedPassword = deletedProfile!.verifiedPassword,

            };
            return _profileManagementDAO!.DeleteProfileEntity(dataStoreUserProfile);
        }



            /*
            private bool accountRecoveryFlag = false;
            private string? _operation { get; set; }
            private readonly UserManagementDataAccess? _userManagementDataAccess;
            private DataStoreUserProfile? _userProfile { get; set; }

            public ProfileManagementService() { }

            public ProfileManagementService(string operation, DataStoreUserProfile userProfile)
            {
                _operation = operation;
                _userProfile = userProfile;
                _userManagementDataAccess = new UserManagementDataAccess();
            }
            public bool SqlGenerator()
            {
                throw new NotImplementedException();
            }

            public ProfileEntity FindProfileOperation()
            {
                ProfileEntity retrievalAccount;
                using (var command = new SqlCommand())
                {
                    command.CommandText = $"SELECT * FROM PROFILE P WHERE P.USERNAME = @v1";
                    var parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@v1", _userProfile!._username);

                    command.Parameters.AddRange(parameters);
                }
                return retrievalAccount;
            }

            public bool CreateProfileOperation()
            {
                using (var command = new SqlCommand())
                {
                    command.CommandText = @"INSERT INTO PROFILE (userId, username) SELECT u.userId, u.username FROM USER u 
                                            EXCEPT SELECT p.userId, p.username FROM PROFILE p;";
                    _userManagementDataAccess = new UserManagementDataAccess(command.CommandText);
                    if (!_userManagementDataAccess.SelectAccountOperation())
                        throw new NullReferenceException(nameof(command));
                    return true;    
                }
            }
            */
            public bool DeleteProfileOperation()
        {
            throw new NotImplementedException();
        }

        public bool UpdateProfileOptionsOperation(string operation, string newValue)
        {
            throw new NotImplementedException();
        }
    }
}
