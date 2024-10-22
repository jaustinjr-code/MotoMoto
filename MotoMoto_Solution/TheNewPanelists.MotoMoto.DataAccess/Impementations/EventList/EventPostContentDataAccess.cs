﻿using System;
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
        // MySqlConnection property which will be used to establish a connection to our data store
        MySqlConnection? mySqlConnection { get; set; }

        // Create declare connection string to AWS RDS data store
        private string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";

        // Connection string for localhost
        //private string _connectionString = "server=localhost;user=dev_moto;database=dev_EventList;port=3306;password=motomoto;";

        // Default and single argument constructor
        public EventPostContentDataAccess(){}
        public EventPostContentDataAccess(string connectionString){_connectionString = connectionString;}

        // Function that will establish the connection to the AWS RDS MariaDB datastore
        public bool EstablishMariaDBConnection()
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
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        // Used to fetch all of the posts within the data store
        public ISet<EventDetailsModel>? FetchAllPosts()
        {
            // Function call to establish the connection to the data store and open a new connection
            EstablishMariaDBConnection();

            // Query that will be executed to retrieve all EventDetails from the EventDetails table
            string selectAllQuery = "SELECT * FROM EventDetails"; 

            // Using the desired query and open SQL connection, initialize an instance of MySqlCommand 
            MySqlCommand command = new MySqlCommand(selectAllQuery, mySqlConnection);

            // Uses the query and SQL connection to build a SQL Data Reader instance
            // This will provide a way of reading forward-only stream of rows from a SQL Server database
            MySqlDataReader myReader = command.ExecuteReader();

            // Instantiate an instance of ISet that will store a set of all rows read from the EventDetails table
            ISet<EventDetailsModel> eventsList = new HashSet<EventDetailsModel>();

            // Keep reading through the table until all rows have been iterated
            while (myReader.Read())
            {
                // Instance of the EventDetailsModel which will be used to store each value in every row
                EventDetailsModel eventDetails = new EventDetailsModel();

                // Read the value from each column and store it in the eventDetails object
                eventDetails.eventID = myReader.GetInt32("eventID");
                eventDetails.eventLocation = myReader.GetString("eventLocation");
                eventDetails.eventTime = myReader.GetString("eventTime");
                eventDetails.eventDate = myReader.GetString("eventDate");

                // Add the iterated row to the set which will contain all rows from the EventDetails table
                eventsList.Add(eventDetails);
            }
            // Close the reader and connection to data store; return the list of events to the calling function
            myReader.Close();
            mySqlConnection!.Close();
            return eventsList;
        }
    }
}
