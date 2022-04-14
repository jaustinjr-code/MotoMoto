using System;
using MySql.Data.MySqlClient;
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

        private string _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;"; //write config so this only appears once

        public UserManagementDataAccess() {}
        public UserManagementDataAccess(string connectionString) 
        {

            _connectionString = connectionString;
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
                userAccount.AccountType = myReader.GetString("typeName");
                userAccount.Username = myReader.GetString("username");
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

                command.CommandText = $"SELECT * FROM USER U WHERE U.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!.Username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                AccountModel returnAccount = new AccountModel();
                while (myReader.Read())
                {
                    returnAccount.AccountType = myReader.GetString("typeName");
                    returnAccount.Username = myReader.GetString("username");
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

                command.CommandText = $"SELECT * FROM USER U WHERE U.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!._username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                DataStoreUser returnUser = new DataStoreUser();
                while (myReader.Read())
                {
                    returnUser.UserId = myReader.GetInt32("userId");
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

                command.CommandText = $"INSERT INTO USER (userId, typeName, username, password, email)" +
                                      $"VALUES (@v0, @v1, @v2, @v3, @v4, @v5)";
                var parameters = new MySqlParameter[6];
                parameters[0] = new MySqlParameter("@v0", userAccount!.UserId);
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
                _username = userAccount.Username,
                _password = userAccount.VerifiedPassword
            };
            if (!UserNamePasswordDSValidation(dataStoreUser)) return false;

            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = $"DELETE * FROM USER U WHERE U.USERNAME = @v1 AND U.PASSWORD = @v2";
                var parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@v1", userAccount!.Username);
                parameters[1] = new MySqlParameter("@v2", userAccount!.VerifiedPassword);

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
                _username = userAccount.Username,
                _password = userAccount.VerifiedPassword
            };
            if (!UserNamePasswordDSValidation(dataStoreUser)) return false;

            using (var command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = $"UPDATE USER SET USER.USERNAME = NULL, USER.EMAIL = NULL WHERE USER.USERNAME = @v1";
                var parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@v1", userAccount!.Username);

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

                command.CommandText = $"SELECT SALT FROM USER WHERE USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", dataStoreUser!._username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                DataStoreUser returnUser = new DataStoreUser();
                while (myReader.Read())
                {
                    returnUser.UserId = myReader.GetInt32("userId");
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

                command.CommandText = $"SELECT * FROM USER U WHERE U.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!._username);

                command.Parameters.AddRange(parameters);
                retrievalAccount = RetrieveDataStoreSpecifiedUserEntity(userAccount);
                if ((retrievalAccount.UserId == userAccount!.UserId) && (retrievalAccount._password == userAccount!._password))
                    return true;
                return false;
            }
        }

        //**********DO NOT DELETE BELOW***********
        //Account Recovery Functions needed later

        /*
        private bool UserEmailDSValidation(DataStoreUser userAccount)
        {
            DataStoreUser retrievalAccount;
            using (var command = new MySqlCommand())
            {
                command.CommandText = $"SELECT * FROM USER U WHERE U.EMAIL = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!._email);

                command.Parameters.AddRange(parameters);
                retrievalAccount = RetrieveDataStoreSpecifiedUserEntity(userAccount);
                if (retrievalAccount._email == userAccount!._email)
                    return true;
                return false;
            }
        }
        
        public bool ForgotUsernameEntity(ForgotUsernameModel userAccount) 
        {
            if (!EstablishMariaDBConnection())
            {
                return false;
            }
            var dataStoreUser = new DataStoreUser()
            {
                _email = userAccount.email
            };
            if (!UserEmailDSValidation(dataStoreUser)) return false;

            using (var command = new MySqlCommand())
            {
                command.CommandText = $"DELETE * FROM USER U WHERE U.USERNAME = @v1 AND U.PASSWORD = @v2";
                var parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@v1", userAccount!.username);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }
        private bool UserNameDSValidation(DataStoreUser userAccount)
        {
            DataStoreUser retrievalAccount;
            using (var command = new MySqlCommand())
            {
                command.CommandText = $"SELECT * FROM USER U WHERE U.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!._username);

                command.Parameters.AddRange(parameters);
                retrievalAccount = RetrieveDataStoreSpecifiedUserEntity(userAccount);
                if (retrievalAccount._username == userAccount!._username)
                    return true;
                return false;
            }
        }
        public bool ForgotPasswordEntity(ForgotPasswordModel userAccount) 
        {
            if (!EstablishMariaDBConnection())
            {
                return false;
            }
            var dataStoreUser = new DataStoreUser()
            {
                _username = userAccount.username
            };
            if (!UserEmailDSValidation(dataStoreUser)) return false;

            using (var command = new MySqlCommand())
            {
                command.CommandText = $"DELETE * FROM USER U WHERE U.USERNAME = @v1 AND U.PASSWORD = @v2";
                var parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@v1", userAccount!.username);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }
        public bool ChangePasswordEntity(ChangePasswordModel userAccount)
        {
            if (!EstablishMariaDBConnection())
            {
                return false;
            }
            var dataStoreUser = new DataStoreUser()
            {
                _password = userAccount.newPassword,
                _verifiedPassword = userAccount.verifiedNewPassword
            };
            //if (!UserEmailDSValidation(dataStoreUser)) return false;

            using (var command = new MySqlCommand())
            {
                command.CommandText = $"DELETE * FROM USER U WHERE U.USERNAME = @v1 AND U.PASSWORD = @v2";
                var parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@v1", userAccount!.username);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }
        */
    }
}
