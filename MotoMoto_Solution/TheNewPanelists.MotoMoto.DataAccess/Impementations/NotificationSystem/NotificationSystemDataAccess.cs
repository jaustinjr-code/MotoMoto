using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Configuration;
using System.Data;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class NotificationSystemDataAccess
    {
        MySqlConnection? mySqlConnection { get; set; }
        private string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";

        public NotificationSystemDataAccess(){}
        public NotificationSystemDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// Execute query function is soley used to execute non queries
        /// in the sense of sql database. We use this functionality of 
        /// booleans to determine if the query returns valid or not.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool ExecuteQuery(MySqlCommand command)
        {
            if (command.ExecuteNonQuery() == 1)
            {
                mySqlConnection!.Close();
                return true;
            }
            mySqlConnection!.Close();
            return false;
        }

        /// <summary>
        /// Establish mariadbconnection ensures the our connection string
        /// is valid. If it does not pass the establishment portion then
        /// our system is notified that connection cannot be passed
        /// </summary>
        /// <returns></returns>
        public bool EstablishMariaDBConnection()
        {
            try
            {
                mySqlConnection = new MySqlConnection(_connectionString);
                mySqlConnection.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Gets all the email addresses to send to the users and all the necessary event details
        /// </summary>
        /// <returns>List<NotificationSystemResponseModel></returns>
        public List<NotificationSystemResponseModel> GetEmail()
        {
            List<NotificationSystemResponseModel> notificationEmailList = new List<NotificationSystemResponseModel>();

            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;
                var todayDate = DateTime.UtcNow;
                var endDate = todayDate.AddDays(2);

                command.CommandText = "SELECT n.eventID, n.eventTime, n.eventDate, n.registeredUsers, n.eventStreetAddress, n.eventCity, n.eventState, n.eventCountry, n.eventZipCode, p.postTitle, u.email FROM Notifications n inner join Post p on  n.postID = p.postID INNER JOIN User u on n.registeredUsers = u.username WHERE n.eventDate >= @todayDate and n.eventDate <= @endDate;";

                // command.CommandText = "SELECT i.eventID, i.eventTime, i.eventDate, i.eventStreetAddress, i.eventCity, i.eventState, i.eventCountry, i.eventZipCode, p.postTitle FROM InAppEventDetails i INNER JOIN Post p ON i.postID = p.postID WHERE i.registeredUsers = @username;";
                // command.CommandText = "SELECT eventID, eventTime, eventDate, eventStreetAddress, eventCity, eventState, eventCountry, eventZipCode FROM InAppEventDetails WHERE registeredUsers = @username;";
                command.Parameters.AddWithValue("@todayDate", todayDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@endDate", endDate.ToString("yyyy-MM-dd"));

                MySqlDataReader myReader = command.ExecuteReader();
        
                while (myReader.Read())
                {
                    NotificationSystemResponseModel notificationData = new NotificationSystemResponseModel();

                    notificationData.eventID = myReader.GetInt32("eventID");
                    notificationData.eventTime = myReader.GetString("eventTime");
                    notificationData.eventDate = myReader.GetString("eventDate");
                    notificationData.username = myReader.GetString("registeredUsers");
                    notificationData.eventStreetAddress = myReader.GetString("eventStreetAddress");
                    notificationData.eventCity = myReader.GetString("eventCity");
                    notificationData.eventState = myReader.GetString("eventState");
                    notificationData.eventCountry = myReader.GetString("eventCountry");
                    notificationData.eventZipCode= myReader.GetString("eventZipCode");
                    notificationData.eventTitle = myReader.GetString("postTitle");
                    notificationData.email = myReader.GetString("email");

                    notificationEmailList.Add(notificationData);
                }
                myReader.Close();
                mySqlConnection.Close();

                return notificationEmailList;
            }
        }

        public List<NotificationSystemResponseModel> GetRegisteredEvents(NotificationSystemRequestModel requestModel)
        {
            List<NotificationSystemResponseModel> registeredEventList = new List<NotificationSystemResponseModel>();

            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;
                
                if (requestModel.notificationType == "Upcoming Events")
                {
                    var todayDate = DateTime.UtcNow;
                    var endDate = todayDate.AddDays(2);
                    command.CommandText = "SELECT n.eventID, n.eventTime, n.eventDate, n.eventStreetAddress, n.eventCity, n.eventState, n.eventCountry, n.eventZipCode, p.postTitle FROM Notifications n INNER JOIN Post p ON n.postID = p.postID WHERE n.registeredUsers = @username AND (eventDate >= @todayDate AND eventDate <= @endDate) ORDER BY eventDate;";
                    command.Parameters.AddWithValue("@username", requestModel.username);
                    command.Parameters.AddWithValue("@todayDate", todayDate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue("@endDate", endDate.ToString("yyyy-MM-dd"));
                }
                else
                {
                    command.CommandText = "SELECT n.eventID, n.eventTime, n.eventDate, n.eventStreetAddress, n.eventCity, n.eventState, n.eventCountry, n.eventZipCode, p.postTitle FROM Notifications n INNER JOIN Post p ON n.postID = p.postID WHERE n.registeredUsers = @username ORDER BY eventDate;";
                    command.Parameters.AddWithValue("@username", requestModel.username);
                }

                MySqlDataReader myReader = command.ExecuteReader();
        
                while (myReader.Read())
                {
                    NotificationSystemResponseModel notificationData = new NotificationSystemResponseModel();

                    notificationData.eventID = myReader.GetInt32("eventID");
                    notificationData.eventTime = myReader.GetString("eventTime");
                    notificationData.eventDate = myReader.GetString("eventDate");
                    notificationData.eventStreetAddress = myReader.GetString("eventStreetAddress");
                    notificationData.eventCity = myReader.GetString("eventCity");
                    notificationData.eventState = myReader.GetString("eventState");
                    notificationData.eventCountry = myReader.GetString("eventCountry");
                    notificationData.eventZipCode= myReader.GetString("eventZipCode");
                    notificationData.eventTitle = myReader.GetString("postTitle");
                    notificationData.notificationSystemStatusMessage = "DATA FETCHED";

                    registeredEventList.Add(notificationData);
                }
                myReader.Close();
                mySqlConnection.Close();

                //Console.WriteLine("Returning from NotificationSystemSDataAccess:FetchRegisteredEvents Hello " + username);
                //Console.WriteLine("this is registered Events" + registeredEventList[0].eventTitle);
                return registeredEventList;
            }
        }  
    }
}
