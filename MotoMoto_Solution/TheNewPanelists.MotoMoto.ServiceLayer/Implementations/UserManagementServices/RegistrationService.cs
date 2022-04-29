using System.Net;
using System.Net.Mail;
using System;
using System.Text;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class RegistrationService : IUserManagementService
    {
        private readonly RegistrationDataAccess _registrationDAO;        
        public RegistrationService()
        {
            _registrationDAO = new RegistrationDataAccess();
        }

        public string AccountRegistrationRequest(RegistrationRequestModel registrationRequest)
        {
            string email = registrationRequest.Email!;

            if (_registrationDAO.QueryUserTable(email))
                return "An account already exists with that email. Please login to access the account.";
            else if (_registrationDAO.HasActiveRegistration(email))
                return "Unable to register. Account is currently pending email validation.";
            else
            {
                if (_registrationDAO.InsertRegistrationEntry(registrationRequest))
                {
                    int registrationId = _registrationDAO.ReturnRegistrationId(email);

                    if (SendEmailConfirmationRequest(email, registrationId))
                        return "Registration submitted. Please validate your email to complete registration.";
                    else
                    {
                        // Log send email failure
                        // Delete from registration table
                        if (!_registrationDAO.DeleteActiveRegistration(email));
                            // log deletion error
                    }
                }
                else;
                    // log entry error
            }
            return "Account registration Error.";
        }

        public string EmailConfirmation(EmailConfirmationRequestModel emailConfirmationRequest)
        {
            if (_registrationDAO.ConfirmRegistration(emailConfirmationRequest))
            {
                DataStoreConfirmedAccount entry = _registrationDAO.ReturnConfirmedAccount(emailConfirmationRequest);
                DataStoreUser newUserAccount = new DataStoreUser();
                string userName = GenerateUniqueName(emailConfirmationRequest.RegistrationID!);

                newUserAccount._userType = "Registered";
                newUserAccount._username = userName;
                newUserAccount._email = entry.Email;
                newUserAccount._password = entry.Password;

                UserManagementService userManagementService = new UserManagementService(new UserManagementDataAccess());

                if (userManagementService.CreateAccount(newUserAccount))
                    return String.Format("Registration complete!\n\n Username = {0}", userName);
                else
                    return "Registration Error.";
            }
            else
                return "ERROR: Registration not found.";
        }

        public bool SendEmailConfirmationRequest(string email, int registrationId)
        {
            // Need to generate a unique url here and insert the link into the email
            string uniqueLink = URLGenerator(registrationId);
            
            string From = "support@motomotoca.com";
            string FromName = "MotoMoto Support";
            string To = email;
            string SMTP_Username = "AKIA3YUD2T2A6VR4HELH";
            string SMTP_Password = "BNQUdw9SMvqsU8qmQvKAyk6y96wKve1U1q21O1NPJeIz";
            // string Configset = "ConfigSet";
            string Host = "email-smtp.us-west-2.amazonaws.com";
            int Port = 587;
            string Subject = "Email Confirmation";
            string Body = @$"
                    <html>
                        <body>
                            <p></p>Hello,</p>
                            <p>Please click on the link below to complete  
                            your account registration.<br>
                            <p>Sincerely,<br><br>
                            MotoMoto Customer Care</p>
                        </body>
                    </html>";

            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(From, FromName);
            message.To.Add(new MailAddress(To));
            message.Subject = Subject;
            message.Body = Body;

            using (var client = new SmtpClient(Host, Port))
            {
                client.Credentials = new NetworkCredential(SMTP_Username, SMTP_Password);
                client.EnableSsl = true;

                try
                {
                    // log attempting to send email
                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    // log error message
                    return false;
                }
            }
        }
        
        private static string URLGenerator(int? registrationID)
        {
            const int suffixSize = 10;

            string urlSuffix = "";
            Random rand = new Random();

            for (int i = 0; i < suffixSize; i++)
            {
                int sel = rand.Next(0, 3);
                switch (sel)
                {
                    case 0:
                        // upper case
                        urlSuffix += (char)rand.Next(65, 91);
                        break;
                    case 1:
                        // lower case
                        urlSuffix += (char)rand.Next(97, 123);   
                        break;
                    case 2:
                        // number 0 - 9
                        urlSuffix += (char)rand.Next(48, 58);    
                        break;
                }
            }
            return (registrationID.ToString() + urlSuffix);
        }

        private static string GenerateUniqueName(string registrationID)
        {
            Random rand = new Random();
            int nameSize = 5;
            string name = "";
            char[] specialChars = new char[4] { '!', '@', '.', ',' };

            for (int i = 0; i < nameSize; i++)
            {
                int num = rand.Next(0, 3);

                switch (num)
                {
                    case 0:
                        name += (char)rand.Next(65, 91);
                        break;
                    case 1:
                        name += (char)rand.Next(97, 123);
                        break;
                    case 2:
                        name += (char)rand.Next(48, 58);
                        break;
                    case 3:
                        int index = rand.Next(specialChars.Length);
                        name += specialChars[index];
                        break;
                }
            }
            return name + registrationID;
        }
    }
}
