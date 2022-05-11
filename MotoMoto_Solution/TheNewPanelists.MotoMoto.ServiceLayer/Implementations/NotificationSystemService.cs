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

        /// <summary>
        /// Calls GetRegisteredEvents from the data access layer then return the list of events
        /// </summary>
        /// <returns>Return a list with all the fetched data of registered events from data access layer</returns>
        public List<NotificationSystemResponseModel> FetchRegisteredEvents(NotificationSystemRequestModel requestModel) 
        {
            return _notificationSystemDataAccess.GetRegisteredEvents(requestModel);
        }

        /// <summary>
        /// Fetch all the registered users email and registered event details to send email on every
        /// Monday and Wednesday at 3:00 PM PST
        /// </summary>
        public bool SendNotificationEmail()
        {
            List<NotificationSystemResponseModel> emailFailureList = new List<NotificationSystemResponseModel>();
            List<NotificationSystemResponseModel> emailList = new List<NotificationSystemResponseModel>();
            DateTime day = DateTime.UtcNow;
            string today = day.ToString("dddd");

            
            //if (today == "Monday")
            if (today == "Monday" || today == "Wednesday")
            {
                DateTime currentTime = DateTime.UtcNow;
                DateTime sentStartTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 22, 0, 0);
                DateTime sendEndTime = sentStartTime.AddMinutes(10);

                if (currentTime >= sentStartTime && currentTime <= sendEndTime)
                {
                    emailList = _notificationSystemDataAccess.GetEmail();
                   
                    foreach (NotificationSystemResponseModel model in emailList)
                    {
                        string eventDate = model.eventDate.Split(" ")[0];

                        string From = "motomoto1ca@gmail.com";
                        string FromName = "MotoMoto Notification Center";
                        string To = model.email;
                        string SMTP_Username = "AKIAQRMTN46LNEVL3VMJ";
                        string SMTP_Password = "BPhxZNIGL/JbyRXHDb5VE9FWh6X/Y/KkZDG3y5WW3jyZ";
                        string Host = "email-smtp.us-west-2.amazonaws.com";
                        int Port = 587;
                        string Subject = "Upcoming Event!";
                        string Body = @$"
                                <html>
                                <div style=""font-family:Google Sans,Roboto,Helvetica Neue,Helvetica,Arial,sans-serif;"">
                                <img src=""https://dbimagebucket.s3.us-west-2.amazonaws.com/MotoMotoLogo_60.png"" />
                                <h1><u>MotoMoto</u></h1>
                                    <div style=""font-size: 16px;"">
                                        <p></p>Hello {model.username},</p><br>
                                        <p>{model.eventTitle} event day is approaching!<br><br>
                                        Event Details:<br>
                                        Event Date: {eventDate}<br>
                                        Event Time: {model.eventTime}<br>
                                        Event Location: {model.eventStreetAddress}, {model.eventCity}, {model.eventState} {model.eventZipCode}, {model.eventCountry}


                                        <br><br><br>
                                        Sincerely,
                                        <br><br><br>
                                        MotoMoto Notification Center</p>
                                    </div>
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
                                client.Send(message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Exception Type:" + ex.GetType() + "\nException Message:" + ex.Message);
                            }
                        }
                    }
                }
   
            }
            return true;
        }
    }
}