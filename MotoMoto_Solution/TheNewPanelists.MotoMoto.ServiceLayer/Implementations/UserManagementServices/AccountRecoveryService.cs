using System.Collections.Generic;
using System.Linq;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;
using TheNewPanelists.MotoMoto.DataAccess.Impementations.UserManagement;
using System.Net.Mail;
using System.Net;
using System;

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
            string username = _accountRecoveryDAO.FetchLostUsername(email);

            string From = "motomoto1ca@gmail.com";
            string FromName = "MotoMoto Account Recovery";
            string To = email!;
            string SMTP_Username = "AKIAQRMTN46LNEVL3VMJ";
            string SMTP_Password = "BPhxZNIGL/JbyRXHDb5VE9FWh6X/Y/KkZDG3y5WW3jyZ";
            string Host = "email-smtp.us-west-2.amazonaws.com";
            int Port = 587;
            string Subject = "Forgot Username MotoMoto";
            string Body = @$"
                    <html>
                    <div style=""font-family:Google Sans,Roboto,Helvetica Neue,Helvetica,Arial,sans-serif;"">
                    <img src=""https://dbimagebucket.s3.us-west-2.amazonaws.com/MotoMotoLogo_60.png"" />
                    <h1><u>MotoMoto</u></h1>
                        <div style=""font-size: 16px;"">
                            <p></p>Hello,</p>
                            <p>Thank you for registering with MotoMoto! Please click on the link below to complete  
                            your account registration.<br><br>
                            
                            {username}

                            <br><br>
                            MotoMoto Support Team</p>
                        </div>
                    </html>";

            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(From, FromName);
            message.To.Add(new MailAddress(To));
            message.Subject = Subject;
            message.Body = Body;
            // message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);
            using (var client = new SmtpClient(Host, Port))
            {
                client.Credentials = new NetworkCredential(SMTP_Username, SMTP_Password);
                client.EnableSsl = true;

                try
                {
                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception Type:" + ex.GetType() + "\nException Message:" + ex.Message);
                    return false;
                }

            }
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
