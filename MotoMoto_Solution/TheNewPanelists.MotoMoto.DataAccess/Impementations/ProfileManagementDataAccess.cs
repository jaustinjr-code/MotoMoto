using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
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
        public ProfileModel RetrieveSpecifiedProfileEntity(ProfileModel userProfile)
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
                parameters[0] = new MySqlParameter("@v1", userProfile!.Username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                ProfileModel returnProfile = new ProfileModel();
                while (myReader.Read())
                {
                    returnProfile.Username = myReader.GetString("username");
                    returnProfile.Status = myReader.GetBoolean("status");
                    returnProfile.EventAccount = myReader.GetBoolean("eventAccount");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return returnProfile;
            }
        }
        public ISet<ProfileModel> GetAllProfiles()
        {
            MySqlCommand command = new MySqlCommand();
            MySqlDataReader myReader = command.ExecuteReader();
            ISet<ProfileModel> accountsSet = new HashSet<ProfileModel>();
            while (myReader.Read())
            {
                ProfileModel userProfile = new ProfileModel();
                userProfile.Username = myReader.GetString("typeName");
                userProfile.Status = myReader.GetBoolean("status");
                userProfile.EventAccount = myReader.GetBoolean("eventAccount");
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
        public bool DeleteProfileEntity(DeleteAccountModel userAccount)
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
                parameters[0] = new MySqlParameter("@v1", userAccount!.Username);

                command.Parameters.AddRange(parameters);
                return(ExecuteQuery(command));
            }
        }
    }
}