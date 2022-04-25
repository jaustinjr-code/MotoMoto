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
                accountType = userAccount!.accountType,
                username = userAccount!.username
            }).ToHashSet();
            return userAccounts;
        }
        public bool CreateAccount(DataStoreUser createdUser)
        {
            var dataStoreUser = new DataStoreUser()
            {
                userType = createdUser!.userType,
                username = createdUser!.username,
                password = createdUser!.password,
                email = createdUser!.email
            };
            return _userManagementDAO.InsertNewDataStoreAccountEntity(dataStoreUser);
        }
        public bool PerminateDeleteAccount(DeleteAccountModel deletedAccount)
        {
            var dataStoreUser = new DeleteAccountModel()
            {
                username = deletedAccount!.username,
                verifiedPassword = deletedAccount!.verifiedPassword
            };
            return _userManagementDAO.PerminateDeleteAccountEntity(dataStoreUser);
        }

        public bool KeepDeleteAccount(DeleteAccountModel deletedAccount)
        {
            var dataStoreUser = new DeleteAccountModel()
            {
                username = deletedAccount!.username,
                verifiedPassword = deletedAccount!.verifiedPassword
            };
            return _userManagementDAO.KeepDeleteAccountEntity(dataStoreUser);
        }
    }
}
