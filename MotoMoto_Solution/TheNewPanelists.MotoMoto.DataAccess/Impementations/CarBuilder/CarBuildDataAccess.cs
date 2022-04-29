using System;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data;
using System.Data.SqlClient;

namespace TheNewPanelists.MotoMoto.DataAccess.Implementations.CarBuilder
{
    public class CarBuildDataAccess : IDataAccess
    {

        private MySqlConnection? mySqlConnection { get; set; }

        // Connection string
        private string _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;"; //write config so this only appears once

        // CarBuildDataAccess constructors
        public CarBuildDataAccess() { }
        public CarBuildDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Function to establish connection with MariaDB
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

        // Function to execute a query in the database
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

        // Referenced in the Service Layer
        // Displays to the user a selection of makes, models, and years of cars
        // Takes in the make, model, year of the car they would like to modify
        // Returns a list of the information inputed by the usser
        public List<CarTypeModel> GetCarType()         
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            CarTypeModel carType = new CarTypeModel();
            List<CarTypeModel> carTypeList = new List<CarTypeModel>();
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string getSenderUserIdQuery = "SELECT carID, make, model, year FROM CarType'";
                MySqlCommand cmd = new MySqlCommand(getSenderUserIdQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        carType.make = reader["make"].ToString();
                        carType.model = reader["model"].ToString();
                        carType.year = reader["year"].ToString();
                        carTypeList.Add(carType);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return carTypeList;
        }

        // Referenced in the Service Layer
        // Displays to the user two types of parts the user can choose from (OEM or Aftermarket)
        // Once the user decides, displays names of parts
        // Takes in part the user wants to modify their car
        // Returns a list of the modifications the user has chosen
        public List<ModifyCarBuildModel> GetModifiedCarBuild()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            ModifyCarBuildModel carModification = new ModifyCarBuildModel();
            List<ModifyCarBuildModel> carModificationList = new List<ModifyCarBuildModel>();
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string getSenderUserIdQuery = "SELECT partName, type FROM OEMAndAfterMarketParts'";
                MySqlCommand cmd = new MySqlCommand(getSenderUserIdQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        carModification.partName = reader["partName"].ToString();
                        carModification.type = reader["type"].ToString();
                        carModificationList.Add(carModification);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return carModificationList;
        }

        // Stores the information from the model to the entity
        public bool InsertNewDataStoreCarTypeEntity(CarTypeModel carType)
        {
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

                command.CommandText = $"INSERT INTO USER (make, model, country, year)" +     // Do not pass carID 
                                      $"VALUES (@v0, @v1, @v2, @v3)";
                var parameters = new MySqlParameter[4];
                //parameters[0] = new MySqlParameter("@v0", carType!.carID);     // Should be removed because you do not need this if auto-incrementing
                parameters[0] = new MySqlParameter("@v1", carType!.make);
                parameters[1] = new MySqlParameter("@v2", carType!.model);
                //parameters[2] = new MySqlParameter("@v3", carType!.country);
                parameters[3] = new MySqlParameter("@v4", carType!.year);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }

        // Stores values for car build variables in database
        public bool InsertNewDataStoreCarBuildsEntity(DataStoreCarBuilds carBuilds)
        {
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

                command.CommandText = $"INSERT INTO USER (carBuildID, carID, username)" +     // Do not pass carID 
                                      $"VALUES (@v0, @v1, @v2)";
                var parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@v0", carBuilds!.carBuildID);     // Should be removed because you do not need this if auto-incrementing
                parameters[1] = new MySqlParameter("@v1", carBuilds!.carID);
                parameters[2] = new MySqlParameter("@v2", carBuilds!.username);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }

        // Stores values for car modifications variables in database
        public bool InsertNewDataStoreCarModificationsEntity(DataStoreCarModifications carModifications)
        {
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

                command.CommandText = $"INSERT INTO USER (carModificationID, carBuildID, partID)" +     // Do not pass carID 
                                      $"VALUES (@v0, @v1, @v2)";
                var parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@v0", carModifications!.carModificationID);     // Should be removed because you do not need this if auto-incrementing
                parameters[1] = new MySqlParameter("@v1", carModifications!.carBuildID);
                parameters[2] = new MySqlParameter("@v2", carModifications!.partID);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }

        // Stores the information from the model to the entity
        public bool InsertNewDataStoreOEMAndAfterMarketPartsEntity(ModifyCarBuildModel modifiedCar)
        {
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

                command.CommandText = $"INSERT INTO USER (partName, type)" +     // Do not pass carID 
                                      $"VALUES (@v0, @v1)";
                var parameters = new MySqlParameter[1];
                //parameters[0] = new MySqlParameter("@v0", carParts!.partID);     // Should be removed because you do not need this if auto-incrementing
                parameters[1] = new MySqlParameter("@v1", modifiedCar!.partName);
                parameters[2] = new MySqlParameter("@v2", modifiedCar!.type);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }
    }
}
