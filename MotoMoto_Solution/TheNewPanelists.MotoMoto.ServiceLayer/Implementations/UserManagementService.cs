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
using System.Data;
using System.Data.SqlClient;

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
        /// <summary>
        /// Find account operation is a retrieval operation that finds a user's specified account. This function
        /// is able to return null in the case that there exists no user with the username inserted. 
        /// </summary>
        /// <returns> Returns an account that is inserted, otherwise returns a null value</returns>
        /*
        public AccountEntity FindAccountOperation()
        {
            AccountEntity retrievalAccount;
            using (var command = new SqlCommand())
            {
                command.CommandText = $"SELECT * FROM USER U WHERE U.USERNAME = @v1";
                var parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@v1", _userAccount!._username);

                command.Parameters.AddRange(parameters);
                _userManagementDataAccess = new UserManagementDataAccess(command.CommandText);
                retrievalAccount = _userManagementDataAccess.RetrieveSpecifiedUserEntity();
                if (retrievalAccount == null)
                    throw new NullReferenceException(nameof(retrievalAccount));
            }
            return retrievalAccount;
        }
        /// <summary>
        /// Create account operation allows accounts to be created. This includes all sensitive information 
        /// regarding a users account which can be modified based on needs.
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns>
        /// This functionality returns a boolean which represents whether a user was stored successfully
        /// </returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool CreateAccountOperation(EntityType accountType)
        {
            using (var command = new SqlCommand())
            {
                command.CommandText = $"INSERT INTO USER (typeName, username, password, email)" +
                                      $"VALUES (@v1, @v2, @v3, @v4,)";
                var parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@v1", accountType._typeName);
                parameters[1] = new SqlParameter("@v2", _userAccount!._username);
                parameters[2] = new SqlParameter("@v3", _userAccount!._password);
                parameters[3] = new SqlParameter("@v4", _userAccount!._email);

                command.Parameters.AddRange(parameters);
                if (!_userManagementDataAccess.SelectAccountOperation())
                {
                    throw new InvalidOperationException();
                }
                return true;
            }
        }
        /// <summary>
        /// DeleteAccountOperation deletes a stored user account from the 'user' table. Dependencies within profile,
        /// user account image, user description, etc. must be deleted before continuing on with perminate deletion
        /// </summary>
        /// <returns>boolean value whether the account was successfully deleted or not</returns>
        /// <exception cref="InvalidOperationException"></exception>
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

                command.Parameters.AddRange(parameters);
                _userManagementDataAccess = new UserManagementDataAccess(command.CommandText);
                if (!_userManagementDataAccess.SelectAccountOperation())
                {
                    throw new InvalidOperationException();
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

                command.Parameters.AddRange(parameters);
                _userManagementDataAccess = new UserManagementDataAccess(command.CommandText);
                retrievalAccount = _userManagementDataAccess.RetrieveDataStoreSpecifiedUserEntity();
                if ((retrievalAccount.UserId == _userAccount!.UserId) && (retrievalAccount._password == _userAccount!._password))
                {
                    return true;
                }
                return false;
            }
        }

        public bool UpdateAccountOptionsOperation(string operation, string newValue)
        {
            using (var command = new SqlCommand())
            {
                command.CommandText = $"UPDATE USER U SET @v1 = @v2 WHERE U.USERNAME = @v3";
                var parameters = new SqlParameter[3];
                switch (operation)
                {
                    case "NEWUSERNAME":
                        parameters[0] = new SqlParameter("@v1", "U.USERNAME");
                        parameters[1] = new SqlParameter("@v2", $"\"{newValue}\"");
                        parameters[2] = new SqlParameter("@v3", _userAccount!._username);
                        break;
                    case "NEWPASSWORD":
                        parameters[0] = new SqlParameter("@v1", "U.PASSWORD");
                        parameters[1] = new SqlParameter("@v2", $"\"{newValue}\"");
                        parameters[2] = new SqlParameter("@v3", _userAccount!._username);
                        break;
                    case "NEWEMAIL":
                        parameters[0] = new SqlParameter("@v1", "U.EMAIL");
                        parameters[1] = new SqlParameter("@v2", $"\"{newValue}\"");
                        parameters[2] = new SqlParameter("@v3", _userAccount!._username);
                        break;
                    default:
                        break;
                }
                command.Parameters.AddRange(parameters);
                _userManagementDataAccess = new UserManagementDataAccess(command.CommandText);
                if (!_userManagementDataAccess.SelectAccountOperation())
                {
                    throw new InvalidOperationException();
                }
            }
            return true;
        }
        */
    }
}
