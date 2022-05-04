using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataAccess;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Collections.Specialized;
using System;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Collections.Generic;


namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class NotificationSystemService
    {
        private readonly NotificationSystemDataAccess _notificationSystemDataAccess;
        // Default constructor that creates the data access for notification system
        public NotificationSystemService()
        {
            _notificationSystemDataAccess = new NotificationSystemDataAccess();
        }

        public bool DeleteNotification(int eventID, string username)
        {
            if (eventID == 0 || username == null)
            {
                return false;
            }
            return (_notificationSystemDataAccess.DeleteNotification(eventID, username));
        }

        /// <summary>
        /// Calls GetRegisteredEvents from the data access layer then return the list of events
        /// </summary>
        ///
        /// <param name="username">Logged-in username to receive in-app notification</param>
        ///
        /// <returns>Return a list with all the fetched data of registered events from data access layer</returns>
        public List<NotificationSystemInAppModel> FetchRegisteredEvents(string username) 
        {
            Console.WriteLine("NotificationSystemService:FetchRegisteredEvents Hello " + username);
            List<NotificationSystemInAppModel> list;
            list = _notificationSystemDataAccess.GetRegisteredEvents(username);
            Console.WriteLine("return from service" + list[0].eventCity);
            return list;
        }

        // public bool SendEmailNotification(NotificationSystemModel notification)
        // {
        //     UriBuilder builder = new UriBuilder() {
        //             Host = "motomotoca.com",
        //             Scheme = "http",
        //             Path = "/Registration/Confirmation",
        //     };

        //     // NameValueCollection urlQueryString = HttpUtility.ParseQueryString(string.Empty);
        //     // urlQueryString.Add("registrationID", registrationRequest.RegistrationId.ToString());
        //     // urlQueryString.Add("email", registrationRequest.Email!);

        //     // string uniqueUrl = builder.ToString() + "?" + urlQueryString.ToString();
        //     // Console.WriteLine(uniqueUrl);

        //     string From = "support@daniel-bribiesca-jr.com";
        //     string FromName = "MotoMoto Support Testing";
        //     string To = NotificationSystemModel.Regi;
        //     string SMTP_Username = "AKIAQRMTN46LNEVL3VMJ";
        //     string SMTP_Password = "BPhxZNIGL/JbyRXHDb5VE9FWh6X/Y/KkZDG3y5WW3jyZ";
        //     // string Configset = "ConfigSet";
        //     string Host = "email-smtp.us-west-2.amazonaws.com";
        //     int Port = 587;
        //     string Subject = "Email Confirmation";
        //     string Body = @$"
        //             <html>
        //                 <body>
        //                     <p></p>Hello,</p>
        //                     <p>Please click on the link below to complete  
        //                     your account registration.<br><br><br>

        //                     <a href={uniqueUrl}>Confirm Email</a>

        //                     <br><br><p>Sincerely,<br>
        //                     MotoMoto Customer Care</p>
        //                 </body>
        //             </html>";

        //     MailMessage message = new MailMessage();
        //     message.IsBodyHtml = true;
        //     message.From = new MailAddress(From, FromName);
        //     message.To.Add(new MailAddress(To));
        //     message.Subject = Subject;
        //     message.Body = Body;
        //     // message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);
        //     using (var client = new SmtpClient(Host, Port))
        //     {
        //         client.Credentials = new NetworkCredential(SMTP_Username, SMTP_Password);
        //         client.EnableSsl = true;

        //         try
        //         {
        //             // log attempting to send email
        //             client.Send(message);
        //             return true;
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine("ExceptionType:" + ex.GetType() + "\nExceptionMessage:" + ex.Message);
        //             return false;
        //         }
        //     }
        // }
    }
}