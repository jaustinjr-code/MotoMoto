using System.Collections.Generic;
using System.Linq;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;


namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class AccountRecoveryService : IUserManagementService
    {
        private readonly AccountRecoveryDataAccess _accountRecoveryDAO;
        public AccountRecoveryService(AccountRecoveryDataAccess accountRecoveryDataAccess)
        {
            _accountRecoveryDAO = accountRecoveryDataAccess;

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
