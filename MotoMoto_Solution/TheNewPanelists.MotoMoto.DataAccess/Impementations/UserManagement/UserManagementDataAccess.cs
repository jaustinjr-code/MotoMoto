using System;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data;
using System.Data.SqlClient;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class UserManagementDataAccess : IDataAccess
    {
        private MySqlConnection? mySqlConnection { get; set; }

        private string? _connectionString { get; set; }
        //write config so this only appears once

        public UserManagementDataAccess() 
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach(ConnectionStringSettings cs in settings)
                    _connectionString = cs.ConnectionString;
            }
        }
        public UserManagementDataAccess(string connectionString)
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                    _connectionString = cs.ConnectionString;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ISet<AccountModel> GetAllUsers()
        {
            MySqlCommand command = new MySqlCommand();
            MySqlDataReader myReader = command.ExecuteReader();
            ISet<AccountModel> accountsSet = new HashSet<AccountModel>();
            while (myReader.Read())
            {
                AccountModel userAccount = new AccountModel();
                userAccount._accountType = myReader.GetString("typeName");
                userAccount._username = myReader.GetString("username");
                accountsSet.Add(userAccount);
            }
            myReader.Close();
            mySqlConnection!.Close();
            return accountsSet;
        }

        /// <summary>
        /// Retrieve specified user entry function is used to return a specific user account
        /// entity which includers ('accountType', 'username', and 'eventAccount') status'.
        /// </summary>
        /// <param></param>
        /// <returns>The user account entity object</returns>
        /// <exception cref="NullReferenceException"></exception>
        public AccountModel RetrieveSpecifiedUserEntity(AccountModel userAccount)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = $"SELECT * FROM User U WHERE U.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!._username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                AccountModel returnAccount = new AccountModel();
                while (myReader.Read())
                {
                    returnAccount._accountType = myReader.GetString("typeName");
                    returnAccount._username = myReader.GetString("username");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return returnAccount;
            }         
        }

        /// <summary>
        /// RetrieveDataStoreSpecified user allows a specified user to execute a data store
        /// entity and retrieve a specifed account based on input
        /// </summary>
        /// <returns>A DataStore user is returned to the main</returns>
        /// <exception cref="NullReferenceException"></exception>
        public DataStoreUser RetrieveDataStoreSpecifiedUserEntity(DataStoreUser userAccount)
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

                command.CommandText = $"SELECT * FROM User U WHERE U.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!._username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                DataStoreUser returnUser = new DataStoreUser();
                while (myReader.Read())
                {
                    returnUser._userId = myReader.GetInt32("userId");
                    returnUser._userType = myReader.GetString("typeName");
                    returnUser._username = myReader.GetString("username");
                    returnUser._password = myReader.GetString("password");
                    returnUser._email = myReader.GetString("email");
                    returnUser._salt = myReader.GetString("salt");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return returnUser;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool InsertNewDataStoreAccountEntity(DataStoreUser userAccount)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            GenerateCiphertext cipherText = new GenerateCiphertext();
            cipherText.GeneratePasswordHash(userAccount!._password!);
            cipherText.GenerateUsernameHash(userAccount!._username!);
            userAccount._salt = cipherText.Salt;

            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = $"INSERT INTO User (userId, typeName, username, password, email)" +
                                      $"VALUES (@v0, @v1, @v2, @v3, @v4, @v5)";
                var parameters = new MySqlParameter[6];
                parameters[0] = new MySqlParameter("@v0", userAccount!._userId);
                parameters[1] = new MySqlParameter("@v1", userAccount!._userType);
                parameters[2] = new MySqlParameter("@v2", userAccount!._username);
                parameters[3] = new MySqlParameter("@v3", userAccount!._password);
                parameters[4] = new MySqlParameter("@v4", userAccount!._email);
                parameters[5] = new MySqlParameter("@v5", userAccount!._salt);

                command.Parameters.AddRange(parameters);
                return(ExecuteQuery(command));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool PerminateDeleteAccountEntity(DeleteAccountModel userAccount)
        {
            if (!EstablishMariaDBConnection())
            {
                return false;
            }
            var dataStoreUser = new DataStoreUser()
            {
                _username = userAccount._username,
                _password = userAccount._verifiedPassword
            };
            if (!UserNamePasswordDSValidation(dataStoreUser)) return false;

            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = $"DELETE * FROM User U WHERE U.USERNAME = @v1 AND U.PASSWORD = @v2";
                var parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@v1", userAccount!._username);
                parameters[1] = new MySqlParameter("@v2", userAccount!._verifiedPassword);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }
        /// <summary>
        /// KeepDeleteAccountEntity is going to be the default option for accounts where we totally remove the username and email to protect ourselves from having to 
        /// remove all information
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns>boolean value that deletes a user perminately and removes their unique ID and all items</returns>
        public bool KeepDeleteAccountEntity(DeleteAccountModel userAccount)
        {
            if (!EstablishMariaDBConnection())
            {
                return false;
            }
            var dataStoreUser = new DataStoreUser()
            {
                _username = userAccount._username,
                _password = userAccount._verifiedPassword
            };
            if (!UserNamePasswordDSValidation(dataStoreUser)) return false;

            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = $"UPDATE User SET USER.USERNAME = NULL, USER.EMAIL = NULL WHERE USER.USERNAME = @v1";
                var parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@v1", userAccount!._username);

                command.Parameters.AddRange(parameters);

                return (ExecuteQuery(command));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataStoreUser"></param>
        /// <returns>boolean value that updates their information and executes those values</returns>
        /// <exception cref="ArgumentNullException"></exception>
        private string RetrieveSaltFromDataStore(DataStoreUser dataStoreUser)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new ArgumentNullException(nameof(dataStoreUser));
            }
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = $"SELECT SALT FROM User WHERE USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", dataStoreUser!._username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                DataStoreUser returnUser = new DataStoreUser();
                while (myReader.Read())
                {
                    returnUser._userId = myReader.GetInt32("userId");
                    returnUser._userType = myReader.GetString("typeName");
                    returnUser._username = myReader.GetString("username");
                    returnUser._password = myReader.GetString("password");
                    returnUser._email = myReader.GetString("email");
                    returnUser._salt = myReader.GetString("salt");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return returnUser!._salt!;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        private bool UserNamePasswordDSValidation(DataStoreUser userAccount)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new ArgumentNullException();
            }
            DataStoreUser retrievalAccount;
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = $"SELECT * FROM User U WHERE U.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!._username);

                command.Parameters.AddRange(parameters);
                retrievalAccount = RetrieveDataStoreSpecifiedUserEntity(userAccount);
                if ((retrievalAccount._userId == userAccount!._userId) && (retrievalAccount._password == userAccount!._password))
                    return true;
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private bool SetNewUsername(AccountModel userAccount)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new ArgumentNullException();
            }
            DataStoreUser retrievalAccount;
            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = $"UPDATE User U SET U.USERNAME = '@v1' WHERE U.USERNAME ='@v2'";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!._newUsername);
                parameters[1] = new MySqlParameter("@v2", userAccount!._username);

                command.Parameters.AddRange(parameters);
                return(ExecuteQuery(command));
            }
        }
    }
}