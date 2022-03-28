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
        public UserManagementService()
        {
            _userManagementDAO = new UserManagementDataAccess();

        }
        public ISet<AccountModel> RetrieveAllAccounts(AccountModel userAccount)
        {
            var accountEntities = _userManagementDAO.GetAllUsers();

            var userAccounts = accountEntities.Select(acct => new AccountModel()
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
        public bool DeleteAccount(DeleteAccountModel deletedAccount)
        {
            var dataStoreUser = new DeleteAccountModel()
            {
                username = deletedAccount!.username,
                verifiedPassword = deletedAccount!.verifiedPassword
            };
            return _userManagementDAO.DeleteAccountEntity(dataStoreUser);
        }
        public bool ForgotUsername(ForgotUsernameModel forgottenUsername)
        {
            var dataStoreUser = new ForgotUsernameModel()
            {
                email = forgottenUsername!.email
            };
            return _userManagementDAO.ForgotUsernameEntity(dataStoreUser);
        }
        public bool ForgotPassword(ForgotPasswordModel forgottenPassword) //What is forgottenPassword supposed to be?
        {
            var dataStoreUser = new ForgotPasswordModel()
            {
                email = forgottenPassword!.email,
                username = forgottenPassword!.username
            };
            return _userManagementDAO.ForgotPasswordEntity(dataStoreUser); //What does this do?
        }
        public bool ChangePassword(ChangePasswordModel changedPassword)
        {
            var dataStoreUser = new ChangePasswordModel()
            {
                email = changedPassword!.email,
                username = changedPassword!.username
            };
            return _userManagementDAO.ChangePasswordEntity(dataStoreUser);
        }
    }
}
