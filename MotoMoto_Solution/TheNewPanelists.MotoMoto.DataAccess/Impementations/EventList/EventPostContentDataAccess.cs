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
    public class EventPostContentDataAccess : IDataAccess
    {
        MySqlConnection? mySqlConnection { get; set; }
        // Create declare connection string to AWS RDS data store
        private string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.comp;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";

        // Connection string for localhost
        //private string _connectionString = "server=localhost;user=dev_moto;database=dev_EventList;port=3306;password=motomoto;";

        // Default and single argument constructor
        public EventPostContentDataAccess(){}
        public EventPostContentDataAccess(string connectionString){_connectionString = connectionString;}

        public bool EstablishMariaDBConnection()
        {
            mySqlConnection = new MySqlConnection(_connectionString);
            try
            {
                mySqlConnection.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        // Used to fetch all of the posts within the data store
        public ISet<EventDetailsModel>? FetchAllPosts()
        {
            EstablishMariaDBConnection();
            string selectAllQuery = "SELECT * FROM EventDetails";
            MySqlCommand command = new MySqlCommand(selectAllQuery, mySqlConnection);
            MySqlDataReader myReader = command.ExecuteReader();
            ISet<EventDetailsModel> eventsList = new HashSet<EventDetailsModel>();
            while (myReader.Read())
            {
                EventDetailsModel eventDetails = new EventDetailsModel();
                eventDetails.eventID = myReader.GetInt32("eventID");
                eventDetails.eventLocation = myReader.GetString("eventLocation");
                eventDetails.eventTime = myReader.GetString("eventTime");
                eventDetails.eventDate = myReader.GetString("eventDate");
                eventsList.Add(eventDetails);
            }
            myReader.Close();
            mySqlConnection!.Close();
            return eventsList;
        }

        public IFeedEntity? GetPost(IFeedModel postInput)
        {
            throw new NotImplementedException();
        }

        public string SqlGenerator()
        {
            throw new NotImplementedException();
        }
    }
}
