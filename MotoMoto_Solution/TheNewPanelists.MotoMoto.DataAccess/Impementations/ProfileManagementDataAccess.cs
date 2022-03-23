using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto;
using TheNewPanelists.MotoMoto.Models;
using System.Data;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class ProfileManagementDataAccess : IDataAccess
    {
        private MySqlConnection? mySqlConnection = null;
        private readonly string _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;";

        public ProfileManagementDataAccess() {}

        public ProfileManagementDataAccess(string connectionString)
        {
            _connectionString = connectionString;
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
                command.CommandType = CommandType.Text;

                command.CommandText = $"SELECT * FROM PROFILE P WHERE P.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userProfile!.username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                ProfileEntity returnProfile = new ProfileEntity();
                while (myReader.Read())
                {
                    returnProfile.username = myReader.GetString("username");
                    returnProfile.status = myReader.GetBoolean("status");
                    returnProfile.eventAccount = myReader.GetBoolean("eventAccount");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return returnProfile;
            }
        }
        public ISet<ProfileEntity> GetAllProfiles()
        {
            MySqlCommand command = new MySqlCommand();
            MySqlDataReader myReader = command.ExecuteReader();
            ISet<ProfileEntity> accountsSet = new HashSet<ProfileEntity>();
            while (myReader.Read())
            {
                ProfileEntity userProfile = new ProfileEntity();
                userProfile.username = myReader.GetString("typeName");
                userProfile.status = myReader.GetBoolean("status");
                userProfile.eventAccount = myReader.GetBoolean("eventAccount");
                accountsSet.Add(userProfile);
            }
            myReader.Close();
            mySqlConnection!.Close();
            return accountsSet;
        }
        public bool InsertNewProfileEntity()
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandType = CommandType.Text;

                command.CommandText = @"INSERT INTO PROFILE (userId, username) SELECT u.userId, u.username FROM USER u 
                                        EXCEPT SELECT p.userId, p.username FROM PROFILE p;";
                return (ExecuteQuery(command));
            }
        }
        public bool DeleteProfileEntity(DeleteAccountEntity userAccount)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandType = CommandType.Text;

                command.CommandText = $"DELETE * FROM PROFILE P WHERE P.USERNAME = \'@v1\';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!.username);

                command.Parameters.AddRange(parameters);
                return(ExecuteQuery(command));
            }
        }
    }
}