using System.Collections.Generic;
using System.Linq;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class UserManagementService : IUserManagementService
    {
        // Readonly means that the object/variable cannot be defined outside of the
        // constructor
        private readonly UserManagementDataAccess _userManagementDAO;
        public UserManagementService(UserManagementDataAccess userManagementDataAccess)
        {
            _userManagementDAO = userManagementDataAccess;

        }
        public ISet<AccountModel> RetrieveAllAccounts(AccountModel userAccount)
        {
            var accountEntities = _userManagementDAO.GetAllUsers();

            var userAccounts = accountEntities.Select(acct => new AccountModel()
            {
                _accountType = userAccount!._accountType,
                _username = userAccount!._username
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
        public bool PerminateDeleteAccount(DeleteAccountModel deletedAccount)
        {
            var dataStoreUser = new DeleteAccountModel()
            {
                _username = deletedAccount!._username,
                _verifiedPassword = deletedAccount!._verifiedPassword
            };
            return _userManagementDAO.PerminateDeleteAccountEntity(dataStoreUser);
        }

        public bool KeepDeleteAccount(DeleteAccountModel deletedAccount)
        {
            var dataStoreUser = new DeleteAccountModel()
            {
                _username = deletedAccount!._username,
                _verifiedPassword = deletedAccount!._verifiedPassword
            };
            return _userManagementDAO.KeepDeleteAccountEntity(dataStoreUser);
        }
    }
}
