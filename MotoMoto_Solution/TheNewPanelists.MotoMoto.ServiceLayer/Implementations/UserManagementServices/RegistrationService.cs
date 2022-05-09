using System.Net;
using System.Net.Mail;
using System.Web;
using System.Collections.Specialized;
using System;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.DataAccess.Registration;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class RegistrationService : IUserManagementService
    {
        ///<value>Property <c>__registrationDAO</c> represents the data access object that will be used to access the data access layer.</value>
        private readonly RegistrationDataAccess _registrationDAO;  

        ///<summary>Default constructor</summary>
        ///<remarks>Instantiates the <c>_registrationDAO</c> property.</remarks>
        public RegistrationService()
        {
            _registrationDAO = new RegistrationDataAccess();
        }

        ///<summary>Communicates with the data access layer to process incoming registration requests.</summary>
        ///<param name="registrationRequest">the model representing the registration request</param>
        ///<remarks>The parameter is passed as a reference and messages are passed to the object for referencing in the frontend</remarks>
        ///<returns>A <c>RegistrationRequestModel</c> object containing the registration details, boolean status of the request, and 
        ///message to be processed in the frontend.</returns>
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
                        registrationRequest.message = "Registration submitted! A confirmation email has been sent to " + registrationRequest.Email + 
                        ". Please follow the link included to complete your registration.\n\n";
                        registrationRequest.status = true;
                    }
                    else if (!_registrationDAO.DeleteActiveRegistration(registrationRequest.RegistrationId!))
                        registrationRequest.message = "Registration Deletion Error.";
                }
                else
                    registrationRequest.message = "Account registration Error.";
            }
            return registrationRequest;
        }

        ///<summary>Communicates with the data access layer to process email confirmations to complete registration.</summary>
        ///<param name="emailConfirmationRequest">the model representing the email confirmation request</param>
        ///<remarks>The parameter is passed as a reference and messages are passed to the object for referencing in the frontend</remarks>
        ///<returns>A string representing the various reposes for success or failure cases</returns>
        public string EmailConfirmation(ref RegistrationRequestModel emailConfirmationRequest)
        {
            if (_registrationDAO.ConfirmRegistration(ref emailConfirmationRequest))
            {
                _registrationDAO.UpdateRegistrationToValid(emailConfirmationRequest.RegistrationId);
                string uniquelyGeneratedName = "";
                uniquelyGeneratedName = GenerateUniqueName(emailConfirmationRequest);

                DataStoreUser newUserAccount = new DataStoreUser {
                    userType = "REGISTERED",
                    username = uniquelyGeneratedName,
                    email = emailConfirmationRequest.Email,
                    password = emailConfirmationRequest.Password
                };

                UserManagementService userManagementService = new UserManagementService(new UserManagementDataAccess());

                if (userManagementService.CreateAccount(newUserAccount))
                {
                    emailConfirmationRequest.status = true;
                    emailConfirmationRequest.message = "Thank you for confirming your email! You're registration is complete! \n" +
                    "   Username is: " + uniquelyGeneratedName;
                    emailConfirmationRequest.Username = uniquelyGeneratedName;
                }
                else
                    emailConfirmationRequest.message = "Registration Error.";
            }
            else
                emailConfirmationRequest.message = "ERROR: Registration not found.";

            return emailConfirmationRequest.message;
        }

        ///<summary>Sends an email to the new user with a confirmation link to complete registration.</summary>
        ///<param name="registrationRequest">the model representing the registration request</param>
        ///<remarks>The exception is caught but not thrown. It is written to the console for debugging reference.</remarks>
        ///<returns>True if Send executes successfully. False if exception is caught./returns>
        public bool SendEmailConfirmationRequest(RegistrationRequestModel registrationRequest)
        {
            UriBuilder builder = new UriBuilder() {
                    //Host = "motomotoca.com",
                    //Scheme = "https"
                    Port = 8080,
                    Scheme = "http",
                    Fragment = "#"
            };

            NameValueCollection urlQueryString = HttpUtility.ParseQueryString(string.Empty);
            urlQueryString.Add("email", registrationRequest.Email!);
            urlQueryString.Add("registrationID", registrationRequest.RegistrationId.ToString());
            
            string uniqueUrl = builder.ToString() + "/Registration/Confirmation?" + urlQueryString.ToString();
            string From = "motomoto1ca@gmail.com";
            string FromName = "MotoMoto Support";
            string To = registrationRequest.Email!;
            string SMTP_Username = "AKIAQRMTN46LNEVL3VMJ";
            string SMTP_Password = "BPhxZNIGL/JbyRXHDb5VE9FWh6X/Y/KkZDG3y5WW3jyZ";
            string Host = "email-smtp.us-west-2.amazonaws.com";
            int Port = 587;
            string Subject = "Email Confirmation";
            string Body = @$"
                    <html>
                    <div style=""font-family:Google Sans,Roboto,Helvetica Neue,Helvetica,Arial,sans-serif;"">
                    <img src=""https://dbimagebucket.s3.us-west-2.amazonaws.com/MotoMotoLogo_60.png"" />
                    <h1><u>MotoMoto</u></h1>
                        <div style=""font-size: 16px;"">
                            <p></p>Hello,</p>
                            <p>Thank you for registering with MotoMoto! Please click on the link below to complete  
                            your account registration.<br><br>

                            <div style=""width: 105px; background-color: #1D843C; display: block; padding: 5px; border-left: 32px; border-right: 32px;"">
                                <a href = {uniqueUrl} style=""color: #FFFFFF;"">Confirm Email</a>
                            </div>

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
        
        ///<summary>Generates a unique username based on the registrationID (ensured to be unique), and the user's
        ///email.</summary>
        ///<param name="emailConfirmationRequest">the model representing the registration request</param>
        ///<remarks>The registration ID is set to length 3 with leading zeros. Will need to be adjust before registrationID
        ///length exceeds 3 digits.</remarks>
        ///<returns>A string representing the unique username./returns>
        public string GenerateUniqueName(RegistrationRequestModel emailConfirmationRequest)
        {
            Random rand = new Random();
            var emailSplitString = emailConfirmationRequest.Email!.Split('@');
            var emailSubString = emailSplitString[0].Substring(0, 5);

            string userName = "";
            userName += string.Format(String.Format("{0:000}", emailConfirmationRequest.RegistrationId));
            userName += emailSubString.ToLower();

            return userName;
        }
    }
}
