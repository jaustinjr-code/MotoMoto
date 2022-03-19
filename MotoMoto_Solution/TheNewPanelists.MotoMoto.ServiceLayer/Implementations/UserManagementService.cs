using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.DataAccess.Impementations;
using TheNewPanelists.MotoMoto.ServiceLayer.Contracts;
using TheNewPanelists.MotoMoto.Entities;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data.SqlClient;
using System.Data;

namespace TheNewPanelists.MotoMoto.ServiceLayer.Implementations
{
    public class UserManagementService : IUserManagementService
    {
        private bool accountRecoveryFlag = false;
        private string? _operation { get; set; }
        private UserManagementDataAccess? _userManagementDataAccess;
        private DataStoreUser? _userAccount { get; set; }

        public UserManagementService() { }

        public UserManagementService(string operation, DataStoreUser userAccount)
        {
            _operation = operation;
            _userAccount = userAccount;
        }
        public bool SqlGenerator()
        {
            throw new NotImplementedException();
        }
        public AccountEntity FindAccountOperation()
        {
            AccountEntity retrievalAccount;
            using (var command = new SqlCommand())
            {
                command.CommandText = $"SELECT * FROM USER U WHERE U.USERNAME = @v1";
                var parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@v1", _userAccount!._username);

                command.Parameters.Add(parameters);
                _userManagementDataAccess = new UserManagementDataAccess(command.CommandText);
                retrievalAccount = _userManagementDataAccess.RetrieveSpecifiedUserEntity();
            }
            return retrievalAccount;
        }

        public bool CreateAccountOperation()
        {
            throw new NotImplementedException();
        }
        
        public bool DeleteAccountOperation()
        {
            if (!UserNamePasswordDSValidation()) 
                return false;
            using (var command = new SqlCommand())
            {
                command.CommandText = $"DELETE * FROM USER U WHERE U.USERNAME = @v1 AND U.PASSWORD = @v2";
                var parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@v1", _userAccount!._username);
                parameters[1] = new SqlParameter("@v2", _userAccount!._password);

                command.Parameters.Add(parameters);
                _userManagementDataAccess = new UserManagementDataAccess(command.CommandText);
                if (!_userManagementDataAccess.SelectAccountOperation())
                {
                    return false;
                }
            }
            return true;
        }

        private bool UserNamePasswordDSValidation()
        {
            DataStoreUser retrievalAccount;
            using (var command = new SqlCommand())
            {
                command.CommandText = $"SELECT * FROM USER U WHERE U.USERNAME = @v1";
                var parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@v1", _userAccount!._username);

                command.Parameters.Add(parameters);
                _userManagementDataAccess = new UserManagementDataAccess(command.CommandText);
                retrievalAccount = _userManagementDataAccess.RetrieveDataStoreSpecifiedUserEntity();
                if ((retrievalAccount.UserId == _userAccount!.UserId) && (retrievalAccount._password == _userAccount!._password))
                {
                    return true;
                }
                return false;
            }
        }

        public bool UpdateAccountOperation()
        { 
            throw new NotImplementedException(); 
        }
    }
}
