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

        public ISet<EventDetailsModel>? FetchEventLocation()
        {
            return null;
        }
    }
}
