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
        private string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;"; 

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
            //CarTypeModel carType = new CarTypeModel();
            List<CarTypeModel> carTypeList = new List<CarTypeModel>();
            try
            {
                connection.Open();
                string getSenderUserIdQuery = "SELECT make, model, year FROM CarTypes";
                MySqlCommand cmd = new MySqlCommand(getSenderUserIdQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CarTypeModel carType = new CarTypeModel();
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
        public UserCarBuildModel GetModifiedCarBuild(string username)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            UserCarBuildModel userCarBuild = new UserCarBuildModel();
            try
            {
                connection.Open();
                string getSenderUserIdQuery = "select cp.make, cp.model, cp.year, oem.partNumber, oem.type from CarBuilds ct join CarModifications cb on ct.carBuildID = cb.carBuildID join OEMAndAfterMarketParts oem on cb.partID = oem.partID join CarTypes cp on cp.carID = ct.carID where ct.username = '" +  username +"'";
                MySqlCommand cmd = new MySqlCommand(getSenderUserIdQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userCarBuild.make = reader["make"].ToString();
                        userCarBuild.model = reader["model"].ToString();
                        userCarBuild.year = reader["year"].ToString();
                        userCarBuild.partNumber = reader["partNumber"].ToString();
                        userCarBuild.type = reader["type"].ToString();
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
            return userCarBuild;
        }

        // Referenced in the Service Layer
        // Displays to the user two types of parts the user can choose from (OEM or Aftermarket)
        // Once the user decides, displays names of parts
        // Takes in part the user wants to modify their car
        // Returns a list of the modifications the user has chosen
        public List<ModifyCarBuildModel> GetParts()
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            ModifyCarBuildModel carModification = new ModifyCarBuildModel();
            List<ModifyCarBuildModel> carModificationList = new List<ModifyCarBuildModel>();
            try
            {
                connection.Open();
                Console.WriteLine("Connection Open");
                string getSenderUserIdQuery = "SELECT partNumber, type FROM OEMAndAfterMarketParts";
                MySqlCommand cmd = new MySqlCommand(getSenderUserIdQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        carModification.partNumber = reader["partNumber"].ToString();
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
                string query = @$"INSERT INTO CarTypes (make, model, year) VALUES (@v0, @v1, @v2)";


                MySqlConnection connection = new MySqlConnection(_connectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add("@v0", MySqlDbType.VarChar);
                cmd.Parameters.Add("@v1", MySqlDbType.VarChar);
                cmd.Parameters.Add("@v2", MySqlDbType.VarChar);

                cmd.Parameters["@v0"].Value = carType.make;
                cmd.Parameters["@v1"].Value = carType.model;
                cmd.Parameters["@v2"].Value = carType.year;

                //connection.Close();
                return (ExecuteQuery(cmd));
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
                string query = @$"INSERT INTO OEMAndAfterMarketParts (partNumber, type) VALUES (@v0, @v1)";


                MySqlConnection connection = new MySqlConnection(_connectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.Add("@v0", MySqlDbType.VarChar);
                cmd.Parameters.Add("@v1", MySqlDbType.VarChar);

                cmd.Parameters["@v0"].Value = modifiedCar.partNumber;
                cmd.Parameters["@v1"].Value = modifiedCar.type;

                return (ExecuteQuery(cmd));
            }
        }
    }
}
