using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Entities;
using TheNewPanelists.MotoMoto.DataAccess.Contracts;
using System.Data.SqlClient;

namespace TheNewPanelists.MotoMoto.DataAccess.Impementations
{
    public class ProfileManagementDataAccess : IDataAccess
    {
        private MySqlConnection? mySqlConnection = null;
        private readonly string _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;";
        private readonly UserManagementDataAccess userManagementDataAccess;

        public ProfileManagementDataAccess() 
        {
            userManagementDataAccess = new UserManagementDataAccess();
        }

        public ProfileManagementDataAccess(string connectionString)
        {
            _connectionString = connectionString;
            userManagementDataAccess = new UserManagementDataAccess(connectionString);
        }
        private bool ExecuteQuery(MySqlCommand command)
        {
            switch (command.ExecuteNonQuery())
            {
                case 1:
                    mySqlConnection!.Close();
                    return true;
                default:
                    mySqlConnection!.Close();
                    return false;
            }
        }
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
        public ProfileEntity RetrieveSpecifiedProfileEntity(ProfileEntity userProfile)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;

                command.CommandText = $"SELECT * FROM PROFILE P WHERE P.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userProfile!.username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                ProfileEntity returnProfile = new ProfileEntity();
                while (myReader.Read())
                {
                    returnProfile.UserId = myReader.GetInt32("userId");
                    returnProfile.username = myReader.GetString("username");
                    returnProfile.status = myReader.GetBoolean("status");
                    returnProfile.eventAccount = myReader.GetBoolean("eventAccount");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return returnProfile;
            }
        }
        public bool InsertNewProfileEntity()
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.CommandText = @"INSERT INTO PROFILE (userId, username) SELECT u.userId, u.username FROM USER u 
                                        EXCEPT SELECT p.userId, p.username FROM PROFILE p;";
                return (ExecuteQuery(command));
            }
        }
        public bool DeleteProfileEntity(DataStoreUser userAccount)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;

                command.CommandText = $"DELETE * FROM PROFILE P WHERE P.USERID = \'@v1\';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!.UserId);

                command.Parameters.AddRange(parameters);
                return(ExecuteQuery(command));
            }
        }
        
    }
}