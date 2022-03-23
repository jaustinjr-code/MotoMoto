using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;
using System.Data.SqlClient;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class UserManagementService : IUserManagementService
    {
        // Readonly means that the object/variable cannot be defined outside of the
        // constructor
        private readonly UserManagementDataAccess _userManagementDAO;
        public UserManagementService()
        {
            _userManagementDAO = new UserManagementDataAccess();

        }
        public ISet<AccountEntity> RetrieveAllAccounts(AccountEntity userAccount)
        {
            var accountEntities = _userManagementDAO.GetAllUsers();

            var userAccounts = accountEntities.Select(acct => new AccountEntity()
            {
                AccountType = userAccount!.AccountType,
                username = userAccount!.username
            }).ToHashSet();
            return userAccounts;
        }
        public bool CreateAccount(DataStoreUser createdUser)
        {
            var dataStoreUser = new DataStoreUser()
            {
                _userType = createdUser!._userType,
                _username = createdUser!._username,
                _password = createdUser!._password,
                _email = createdUser!._email
            };
            return _userManagementDAO.InsertNewDataStoreAccountEntity(dataStoreUser);
        }
        public bool DeleteAccount(DeleteAccountEntity deletedAccount)
        {
            var dataStoreUser = new DeleteAccountEntity()
            {
                username = deletedAccount!.username,
                verifiedPassword = deletedAccount!.verifiedPassword
            };
            return _userManagementDAO.DeleteAccountEntity(dataStoreUser);
        }
    }
}
