using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.Models.UserManagementModels.AccountRecoveryModels;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.BusinessLayer.Implementations
{
    public class AccountRecoveryManager
    {
        private readonly AccountRecoveryService _accountRecoveryService;

        public AccountRecoveryManager(AccountRecoveryService accountRecoveryService)
        {
            _accountRecoveryService = accountRecoveryService;
        }

        public bool AccountRecoveryRetrieveUsername(string email)
        {
            if (email!.Length == 0 || email!.Length > 30)   // Make sure user input is not null and is less than 30 characters
            {
                return false;
            }
            return _accountRecoveryService.RetrieveLostUsername(email);
        }

        public bool AccountRecoveryChangePasswordEmail(string email)
        {
            if (email!.Length == 0 || email!.Length > 30)   // Make sure user input is not null and is less than 30 characters
            {
                return false;
            }
            return _accountRecoveryService.SendChangePasswordEmail(email);
        }
        public bool AccountRecoveryChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (changePasswordModel.newPassword!.Length == 0 || changePasswordModel.newPassword!.Length > 30)   // Make sure user input is not null and is less than 30 characters
            {
                return false;
            }
            return _accountRecoveryService.ChangeUserPassword(accountRecoveryModel);
        }
    }
}
