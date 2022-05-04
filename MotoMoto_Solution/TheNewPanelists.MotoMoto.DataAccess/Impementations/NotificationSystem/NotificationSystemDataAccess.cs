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
        /// Returns list of registered events within give date range
        /// </summary>
        /// <returns>List<NotificationSystemInAppModel> GetRegisteredEvents</returns>
        public List<NotificationSystemInAppModel> GetRegisteredEvents(string username) //cookie   
        {
            Console.WriteLine("NotificationSystemSDataAccess:FetchRegisteredEvents Hello " + username);
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            List<NotificationSystemInAppModel> registeredEventList = new List<NotificationSystemInAppModel>();
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;
                var todayDate = DateTime.Now;
                var endDate = todayDate.AddDays(2);

                command.CommandText = "SELECT i.eventID, i.eventTime, i.eventDate, i.eventStreetAddress, i.eventCity, i.eventState, i.eventCountry, i.eventZipCode, p.postTitle FROM InAppEventDetails i INNER JOIN Post p ON i.postID = p.postID WHERE i.registeredUsers = @username AND (eventDate >= @todayDate AND eventDate <= @endDate) ORDER BY eventDate;";

                // command.CommandText = "SELECT i.eventID, i.eventTime, i.eventDate, i.eventStreetAddress, i.eventCity, i.eventState, i.eventCountry, i.eventZipCode, p.postTitle FROM InAppEventDetails i INNER JOIN Post p ON i.postID = p.postID WHERE i.registeredUsers = @username;";
                // command.CommandText = "SELECT eventID, eventTime, eventDate, eventStreetAddress, eventCity, eventState, eventCountry, eventZipCode FROM InAppEventDetails WHERE registeredUsers = @username;";
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@todayDate", todayDate.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@endDate", endDate.ToString("yyyy-MM-dd"));

                MySqlDataReader myReader = command.ExecuteReader();
        
                while (myReader.Read())
                {
                    NotificationSystemInAppModel inAppNotificationData = new NotificationSystemInAppModel();
        
                    inAppNotificationData.eventID = myReader.GetInt32("eventID");
                    inAppNotificationData.eventTime = myReader.GetString("eventTime");
                    Console.WriteLine("thisis myreader.Read()" + inAppNotificationData.eventTime);
                    inAppNotificationData.eventDate = myReader.GetString("eventDate");
                    inAppNotificationData.eventStreetAddress = myReader.GetString("eventStreetAddress");
                    inAppNotificationData.eventCity = myReader.GetString("eventCity");
                    inAppNotificationData.eventState = myReader.GetString("eventState");
                    inAppNotificationData.eventCountry = myReader.GetString("eventCountry");
                    inAppNotificationData.eventZipCode= myReader.GetString("eventZipCode");
                    inAppNotificationData.eventTitle = myReader.GetString("postTitle");

                    registeredEventList.Add(inAppNotificationData);
                }
                myReader.Close();
                mySqlConnection!.Close();

                Console.WriteLine("Returning from NotificationSystemSDataAccess:FetchRegisteredEvents Hello " + username);
                Console.WriteLine("this is registered Events" + registeredEventList[0].eventTitle);
                return registeredEventList;
            }
        }

        // Establish connection assigns connection string
            // if (!EstablishMariaDBConnection())
            // {
            //     throw new NullReferenceException();
            // }
            // else mySqlConnection!.BeginTransaction();

            // string commandText = "SELECT eventTime, eventDate, eventStreetAddress, eventCity, eventState, eventCountry, eventZipCode FROM InAppEventDetails WHERE registeredUsers = @username;";
            
            // using (MySqlCommand command = new MySqlCommand(commandText, mySqlConnection))
            // {
            //     command.Parameters.AddWithValue("@username", username);
            //     try
            //     {
            //         command.Transaction = mySqlConnection!.BeginTransaction();
            //         command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
            //         List<NotificationSystemInAppModel> data = RefineData(command.ExecuteReader());
            //         command.Transaction.Commit();
            //         mySqlConnection.Close();

                    
            //         return data;
            //     }
            //     catch (Exception e)
            //     {
            //         command.Transaction.Rollback();
            //         // Trigger Logging Service
            //         // e.ToString();
            //         // Console.WriteLine(e.Message);
            //         throw e;
            //     }
            }      
        // {
        //     MySqlConnection connection = new MySqlConnection(_connectionString);
        //     NotificationSystemInAppModel inAppNotification = new NotificationSystemInAppModel();
        //     List<NotificationSystemInAppModel> registeredEventsList = new List<NotificationSystemInAppModel>();
        //     string commandText = "SELECT eventTime, eventDate, eventStreetAddress, eventCity, eventState, eventCountry, eventZipCode FROM InAppEventDetails WHERE registeredUsers = @username;";
        //     try
        //     {
        //         connection.Open();
        //         Console.WriteLine("Connection Open");
        //         var currentDate = DateOnly.FromDateTime(DateTime.Now);

        //         MySqlCommand cmd = new MySqlCommand(commandText, connection);
        //         MySqlDataReader reader = cmd.ExecuteReader();
        //         if (reader.HasRows)
        //         {
        //             while (reader.Read())
        //             {
        //                 inAppNotification.eventTime = reader["eventTime"].ToString();
        //                 inAppNotification.eventDate = reader["eventDate"].ToString();
        //                 inAppNotification.eventStreetAddress = reader["eventStreetAddress"].ToString();
        //                 inAppNotification.eventCity = reader["eventCity"].ToString();
        //                 inAppNotification.eventState = reader["eventState"].ToString();
        //                 inAppNotification.eventCountry = reader["eventCountry"].ToString();
        //                 inAppNotification.eventZipCode = reader["eventZipCode"].ToString();
        //                 registeredEventsList.Add(inAppNotification);
        //             }
        //             reader.Close();
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception(ex.Message);
        //     }
        //     finally
        //     {
        //         connection.Close();
        //     }
        //     return registeredEventsList;
        }

        // /// <summary>
        // /// RetrieveAllCategorialPartInformation will be used to allow users to see specified
        // /// lists of items on their view. 
        // /// </summary>
        // /// <param name="listModel"></param>
        // /// <returns></returns>
        // /// <exception cref="NullReferenceException"></exception>
        // public PartListModel RetrieveAllCategorialPartInformationDataAccess(PartListModel listModel)
        // {
        //     if (!EstablishMariaDBConnection())
        //     {
        //         return listModel;
        //     }
        //     using (var command = new MySqlCommand())
        //     {
        //         command.Transaction = mySqlConnection!.BeginTransaction();
        //         command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
        //         command.Connection = mySqlConnection!;
        //         command.CommandType = CommandType.Text;

        //         command.CommandText = "SELECT * FROM VehicleParts WHERE productName LIKE @v1";
        //         var parameters = new MySqlParameter[1];
        //         listModel.categorySelect = $"%{listModel.categorySelect}%";
        //         parameters[0] = new MySqlParameter("@v1", listModel.categorySelect);

        //         command.Parameters.AddRange(parameters);

        //         IEnumerable<IPartEntity> _partList = new List<IPartEntity>();

        //         using (MySqlDataReader myReader = command.ExecuteReader())
        //         {
        //             while (myReader.Read())
        //             {
        //                 int partID = myReader.GetInt32("productId");
        //                 string partName = myReader.GetString("productName");
        //                 string rating = myReader.GetString("rating");
        //                 int ratingCount = myReader.GetInt32("ratingCount");
        //                 double productPrice = myReader.GetDouble("productPrice");
        //                 string productURL = myReader.GetString("productURL");

        //                 IPartEntity partEntity = new DataStoreVehicleParts(partID, partName, rating, ratingCount, productURL, productPrice);
        //                 partEntity.ShrinkPartName();
        //                 ((List<IPartEntity>)_partList).Add(partEntity);
        //             }
        //             myReader.Close();
        //             mySqlConnection!.Close();
        //         };
        //         listModel.partList = _partList;
        //         return listModel;
        //     }
        // }
        // /// <summary>
        // /// Retrieve part information will be used to fetch part information from the 
        // /// datastore. This function will primarily be called to display our list of items
        // /// in the database for quick retrieval. If a user wants to see visually more in
        // /// depth information about a product we will provide the Amazon URL for them.
        // /// </summary>
        // /// <param name="part"></param>
        // /// <returns></returns>
        // /// <exception cref="NullReferenceException"></exception>
        // public PartModel RetrievePartInformation(PartModel part)
        // {
        //     if (!EstablishMariaDBConnection())
        //     {
        //         return part;
        //     }
        //     using (var command = new MySqlCommand())
        //     {
        //         command.Transaction = mySqlConnection!.BeginTransaction();
        //         command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
        //         command.Connection = mySqlConnection!;
        //         command.CommandType = CommandType.Text;

        //         command.CommandText = "SELECT * FROM VehicleParts WHERE productId = @v1;";
        //         var parameters = new MySqlParameter[1];
        //         parameters[0] = new MySqlParameter("@v1", part.partID);

        //         command.Parameters.AddRange(parameters);
        //         PartModel _partModel = new PartModel();
        //         using (MySqlDataReader myReader = command.ExecuteReader())
        //         {
        //             while (myReader.Read())
        //             {
        //                 _partModel.partID = myReader.GetInt32("productId");
        //                 _partModel.partName = myReader.GetString("productName");
        //                 _partModel.rating = myReader.GetString("rating");
        //                 _partModel.ratingCount = myReader.GetInt32("ratingCount");
        //                 _partModel.productURL = myReader.GetString("productURL");
        //                 _partModel.currentPrice = myReader.GetDouble("productPrice");

        //                 _partModel.partName = _partModel.partName.Substring(0, 45);
        //             }
        //             myReader.Close();
        //             mySqlConnection!.Close();
        //             return _partModel;
        //         };
        //     }
        // }
