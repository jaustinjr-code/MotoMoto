using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess.Impementations;
using TheNewPanelists.MotoMoto.ServiceLayer.Contracts;
using TheNewPanelists.MotoMoto.Entities;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data.SqlClient;
using System.Data;

namespace TheNewPanelists.MotoMoto.ServiceLayer.Implementations
{
    public class ProfileManagementService : IUserManagementService
    {
        private bool accountRecoveryFlag = false;
        private string? _operation { get; set; }
        private UserManagementDataAccess? _userManagementDataAccess;
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
        /*
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
