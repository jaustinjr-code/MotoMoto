using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class MeetingPointDirectionsDataAccess
    {
        // MySqlConnection property which will be used to establish a connection to our data store
        MySqlConnection? mySqlConnection { get; set; }

        // Create declare connection string to AWS RDS data store
        private string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";

        public MeetingPointDirectionsDataAccess() { }
        public MeetingPointDirectionsDataAccess(string connectionString) { _connectionString = connectionString; }

        // Function that will establish the connection to the AWS RDS MariaDB datastore
        public bool EstablishDBConnection()
        {
            // Create a MySqlConnection object which is connected to the database using the connection string
            mySqlConnection = new MySqlConnection(_connectionString);
            try
            {
                mySqlConnection.Open(); // Draws an open connection from the connection pool if one is available; else open new connection
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // CONSOLE MESSAGES SHOULD NOT BE IN PRODUCTION CODE
            }
            return false;
        }

        public ISet<EventDetailsModel>? FetchEventLocation(int eventID)
        {
            EstablishDBConnection();

            string fetchLocationQuery = "SELECT eventStreetAddress, eventCity, eventState, eventCountry, eventZipCode FROM EventDetails WHERE eventID = @eventID";

            // Using the desired query and open SQL connection, initialize an instance of MySqlCommand 
            MySqlCommand command = new MySqlCommand(fetchLocationQuery, mySqlConnection);

            command.Parameters.Add(new MySqlParameter("@eventID", System.Data.SqlDbType.Int));
            command.Parameters["@eventID"].Value = eventID;

            // Uses the query and SQL connection to build a SQL Data Reader instance
            // This will provide a way of reading forward-only stream of rows from a SQL Server database
            MySqlDataReader myReader = command.ExecuteReader();

            // Instantiate an instance of ISet that will store a set of all rows read from the EventDetails table
            ISet<EventDetailsModel> location = new HashSet<EventDetailsModel>();

            // Keep reading through the table until all rows have been iterated
            while (myReader.Read())
            {
                // Instance of the EventDetailsModel which will be used to store each value in every row
                EventDetailsModel eventDetails = new EventDetailsModel();

                // Read the value from each column and store it in the eventDetails object
                eventDetails.eventStreetAddress = myReader.GetString("eventStreetAddress");
                eventDetails.eventCity = myReader.GetString("eventCity");
                eventDetails.eventState = myReader.GetString("eventState");
                eventDetails.eventCountry = myReader.GetString("eventCountry");
                eventDetails.eventZipCode = myReader.GetString("eventZipCode");


                // Add the iterated row to the set which will contain all rows from the EventDetails table
                location.Add(eventDetails);
            }
            // Close the reader and connection to data store; return the list of events to the calling function
            myReader.Close();
            mySqlConnection!.Close();
            return location;
        }
    }
}
