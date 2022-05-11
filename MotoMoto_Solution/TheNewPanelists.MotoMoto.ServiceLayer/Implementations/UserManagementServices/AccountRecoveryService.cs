using System.Collections.Generic;
using System.Linq;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;
using TheNewPanelists.MotoMoto.DataAccess.Impementations.UserManagement;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class AccountRecoveryService : IUserManagementService
    {
        private readonly AccountRecoveryDataAccess _accountRecoveryDAO;
        public AccountRecoveryService(AccountRecoveryDataAccess accountRecoveryDataAccess)
        {
            _accountRecoveryDAO = accountRecoveryDataAccess;

        }

        public bool RetrieveLostUsername(string email)
        {
            return _accountRecoveryDAO.FetchLostUsername(email);
        }

        public bool SendChangePasswordEmail(string email)
        {
            return _accountRecoveryDAO.FetchPasswordEmail(email);
        }

        public bool ChangeUserPassword(ChangePasswordModel changePasswordModel)
        {
            return _accountRecoveryDAO.ChangePassword(changePasswordModel);
        }
    }

    //**********DO NOT DELETE BELOW***********
    //Account Recovery Functions needed later
    /*
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
            username = forgottenPassword!.username
        };
        return _userManagementDAO.ForgotPasswordEntity(dataStoreUser); //What does this do?
    }
    public bool ChangePassword(ChangePasswordModel changedPassword)
    {
        var dataStoreUser = new ChangePasswordModel()
        {
            newPassword = changedPassword!.newPassword,
            verifiedNewPassword = changedPassword!.verifiedNewPassword
        };
        return _userManagementDAO.ChangePasswordEntity(dataStoreUser);
    }
    */
}
