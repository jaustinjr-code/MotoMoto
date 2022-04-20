using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class UserManagementManager
    {
        private readonly UserManagementService _userManagementService;

        public UserManagementManager(UserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        /// <summary>
        /// Account Deletion manager verifies that the inserted username does not exceed the DB stored username
        /// and for password entries. ** Need to work on the SHA256 hashes for password so the password length
        /// equivalent to 24 needs to be updated to 255 once new tables/services are set **
        /// </summary>
        /// <param name="DeleteAccountUser"></param>
        /// <returns>boolean value based off of the account deletion service</returns>
        public bool PerminateDeleteAccountManager(DeleteAccountModel deleteAccountUser)
        {
            if (deleteAccountUser.Username!.Length == 0 || deleteAccountUser.Username!.Length > 24)
            {
                return false;
            }
            if (deleteAccountUser.VerifiedPassword!.Length == 0 || deleteAccountUser.VerifiedPassword.Length > 24)
            {
                return false;
            }
            return _userManagementService.PerminateDeleteAccount(deleteAccountUser);
        }

        public bool KeepDeleteAccountManager(DeleteAccountModel deleteAccountModel)
        {
            if (deleteAccountModel.Username!.Length == 0 || deleteAccountModel.Username!.Length > 24)
            {
                return false;
            }
            if (deleteAccountModel.VerifiedPassword!.Length == 0 || deleteAccountModel.VerifiedPassword.Length > 24)
            {
                return false;
            }
            return _userManagementService.KeepDeleteAccount(deleteAccountModel);
        }

        /// <summary>
        /// Goal of the RetrieveAllUsers is to retrieve all user accounts. This excludes Admin/Default accounts
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Set of All the users to display</returns>
        public ISet<AccountModel> RetrieveAllUsers(string username)
        {
            var accountModel = new AccountModel()
            {
                AccountType = "REGISTERED",
                Username = username
            };
            return _userManagementService.RetrieveAllAccounts(accountModel);
        }
    }
}
