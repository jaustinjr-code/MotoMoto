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
        MySqlConnection? mySqlConnection { get; set; }

        // Create declare connection string to the data store
        private string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";

        public MeetingPointDirectionsDataAccess() { }
        public MeetingPointDirectionsDataAccess(string connectionString) { _connectionString = connectionString; }

        // Function to open a database connection using connection string
        public bool EstablishDBConnection()
        {
            mySqlConnection = new MySqlConnection(_connectionString);
            try
            {
                mySqlConnection.Open(); 
                return true;
            }
            catch (Exception ex)
            {
                // Should log error here
                throw new ArgumentException("ERROR: Could not establish database connection...", ex);
            }
        }

        // Queries the datastore and fetches the event location information using the passed in eventID
        public ISet<EventDetailsModel>? FetchEventLocation(int eventID)
        {
            EstablishDBConnection();
            string fetchLocationQuery = "SELECT eventStreetAddress, eventCity, eventState, eventCountry, eventZipCode FROM EventDetails WHERE eventID = @eventID";
            try
            {
                using (MySqlCommand command = new MySqlCommand(fetchLocationQuery, mySqlConnection))
                {
                    command.Parameters.Add(new MySqlParameter("@eventID", System.Data.SqlDbType.Int));
                    command.Parameters["@eventID"].Value = eventID;
                    MySqlDataReader myReader = command.ExecuteReader();
                    ISet<EventDetailsModel> location = new HashSet<EventDetailsModel>();

                    // Read queried rows and store into a Set of EventDetailsModels
                    while (myReader.Read())
                    {
                        EventDetailsModel eventDetails = new EventDetailsModel();

                        eventDetails.eventStreetAddress = myReader.GetString("eventStreetAddress");
                        eventDetails.eventCity = myReader.GetString("eventCity");
                        eventDetails.eventState = myReader.GetString("eventState");
                        eventDetails.eventCountry = myReader.GetString("eventCountry");
                        eventDetails.eventZipCode = myReader.GetString("eventZipCode");

                        location.Add(eventDetails);
                    }
                    myReader.Close();
                    return location;
                }
            }
            catch (Exception ex) 
            {
                // Log here
                throw new ArgumentException("ERROR: Could not retrieve information...", ex);
            }
            finally
            {
                mySqlConnection!.Dispose();
            }
        }
    }
}
