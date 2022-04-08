using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool DeleteAccountManager(DeleteAccountModel deleteAccountUser)
        {
            if (deleteAccountUser.username!.Length == 0 || deleteAccountUser.username!.Length > 24)
            {
                return false;
            }
            if (deleteAccountUser.verifiedPassword!.Length == 0 || deleteAccountUser.verifiedPassword.Length > 24)
            {
                return false;
            }
            return _userManagementService.DeleteAccount(deleteAccountUser);
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
                username = username
            };
            return _userManagementService.RetrieveAllAccounts(accountModel);
        }
    }
}
