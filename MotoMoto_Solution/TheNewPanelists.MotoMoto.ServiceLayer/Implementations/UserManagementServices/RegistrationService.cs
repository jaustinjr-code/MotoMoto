using System.Net;
using System.Net.Mail;
using System.Web;
using System.Collections.Specialized;
using System;
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

        public RegistrationRequestModel AccountRegistrationRequest(ref RegistrationRequestModel registrationRequest)
        {
            if (_registrationDAO.QueryUserTable(registrationRequest.Email!))
                registrationRequest.message = "An account already exists with that email. Please login to access the account.";
            else if (_registrationDAO.HasActiveRegistration(registrationRequest.Email!))
                registrationRequest.message = "Unable to register. Account is currently pending email validation.";
            else
            {
                if (_registrationDAO.InsertRegistrationEntry(ref registrationRequest))
                {             
                    registrationRequest.RegistrationId = _registrationDAO.ReturnRegistrationId(registrationRequest.Email!);

                    if (SendEmailConfirmationRequest(registrationRequest))
                    {
                        registrationRequest.message = "Registration submitted. Please validate your email to complete registration.";
                        registrationRequest.status = true;
                    }
                    else if (!_registrationDAO.DeleteActiveRegistration(registrationRequest.Email!))
                        registrationRequest.message = "Registration Deletion Error.";
                }
                else
                    registrationRequest.message = "Account registration Error.";
            }
            return registrationRequest;
        }

        public string EmailConfirmation(ref RegistrationRequestModel emailConfirmationRequest)
        {
            if (_registrationDAO.ConfirmRegistration(ref emailConfirmationRequest))
            {
                _registrationDAO.UpdateRegistrationToValid(emailConfirmationRequest.Email!);
                DataStoreUser newUserAccount = new DataStoreUser();
                string userName = GenerateUniqueName(emailConfirmationRequest);

                newUserAccount.userType = "Registered";
                newUserAccount.username = userName;
                newUserAccount.email = emailConfirmationRequest.Email;
                newUserAccount.password = emailConfirmationRequest.Password;

                UserManagementService userManagementService = new UserManagementService(new UserManagementDataAccess());

                if (userManagementService.CreateAccount(newUserAccount))
                    emailConfirmationRequest.message = String.Format("Registration complete!\n\n Username = {0}", userName);
                else
                    emailConfirmationRequest.message = "Registration Error.";
            }
            else
                emailConfirmationRequest.message = "ERROR: Registration not found.";

            return emailConfirmationRequest.message;
        }

        public bool SendEmailConfirmationRequest(RegistrationRequestModel registrationRequest)
        {
            UriBuilder builder = new UriBuilder() {
                    Host = "motomotoca.com",
                    Scheme = "http",
                    Path = "/Registration/Confirmation",
            };

            NameValueCollection urlQueryString = HttpUtility.ParseQueryString(string.Empty);
            urlQueryString.Add("registrationID", registrationRequest.RegistrationId.ToString());
            urlQueryString.Add("email", registrationRequest.Email!);

            string uniqueUrl = builder.ToString() + "?" + urlQueryString.ToString();
            Console.WriteLine(uniqueUrl);

            string From = "support@daniel-bribiesca-jr.com";
            string FromName = "MotoMoto Support Testing";
            string To = registrationRequest.Email!;
            string SMTP_Username = "AKIAQRMTN46LNEVL3VMJ";
            string SMTP_Password = "BPhxZNIGL/JbyRXHDb5VE9FWh6X/Y/KkZDG3y5WW3jyZ";
            // string Configset = "ConfigSet";
            string Host = "email-smtp.us-west-2.amazonaws.com";
            int Port = 587;
            string Subject = "Email Confirmation";
            string Body = @$"
                    <html>
                        <body>
                            <p></p>Hello,</p>
                            <p>Please click on the link below to complete  
                            your account registration.<br><br><br>

                            <a href={uniqueUrl}>Confirm Email</a>

                            <br><br><p>Sincerely,<br>
                            MotoMoto Customer Care</p>
                        </body>
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
                    // log attempting to send email
                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ExceptionType:" + ex.GetType() + "\nExceptionMessage:" + ex.Message);
                    return false;
                }
            }
        }
        
        private static string GenerateUniqueName(RegistrationRequestModel emailConfirmationRequest)
        {
            Random rand = new Random();
            var emailSplitString = emailConfirmationRequest.Email!.Split('@');

            string userName = string.Format(String.Format("{0:000000}", emailConfirmationRequest.RegistrationId));
            userName += emailSplitString[0];

            return userName;
        }
    }
}
