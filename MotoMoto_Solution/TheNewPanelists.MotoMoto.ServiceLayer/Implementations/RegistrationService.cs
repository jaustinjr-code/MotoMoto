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

        public RegistrationService() { _registrationDAO = new RegistrationDataAccess(); }

        public string AccountRegistrationRequest(RegistrationRequestModel registrationRequest)
        {
            string registrationEmail = registrationRequest.Email!;

            if (_registrationDAO.QueryUserTable(registrationEmail))
                return "An account already exists with that email. Please login to access the account.";
            else if (_registrationDAO.HasActiveRegistration(registrationEmail))
                return "Unable to register. Account is currently pending email validation.";
            else
            {
                if (_registrationDAO.InsertRegistrationEntry(registrationRequest))
                {
                    RegistrationEntity registrationEntry = _registrationDAO.ReturnActiveRegistrationEntry(registrationEmail);

                    if (SendEmailConfirmationRequest(registrationEntry))
                        return "Registration submitted. Please validate your email to complete registration.";
                    else
                    {
                        // Log send email failure
                        // Delete from registration table
                        if (!_registrationDAO.DeleteActiveRegistrationEntry(registrationEmail));
                            // log deletion error
                    }
                }
                else;
                    // log entry error
            }
            return "Account registration Error.";
        }

        //public string EmailConfirmation(EmailConfirmationRequestModel emailConfirmationRequest)
        //{
        //    string registrationEmail = emailConfirmationRequest.Email!;

        //    if ()
        //}

        public static bool SendEmailConfirmationRequest(RegistrationEntity registrationEntry)
        {
            
            // Need to generate a unique url here and insert the link into the email
            // string uniqueLink = URLGenerator(registrationEntry.Email!);
            
            string From = "projmotomoto@gmail.com";
            string FromName = "MotoMoto Registration";
            string To = registrationEntry.Email!;
            string SMTP_Username = "smtp_username";
            string SMTP_Password = "smtp_password";
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
        
        public static string URLGenerator(string registrationID)
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
            return (registrationID + urlSuffix);
        }

        public static string GenerateUniqueName(string registrationID)
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
