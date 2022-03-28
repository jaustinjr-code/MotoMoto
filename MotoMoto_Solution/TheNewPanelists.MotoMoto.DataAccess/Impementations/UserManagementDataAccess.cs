using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private string _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;";

        public UserManagementDataAccess() {}
        public UserManagementDataAccess(string connectionString) 
        {

            _connectionString = connectionString;
        }
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
        public ISet<AccountModel> GetAllUsers()
        {
            MySqlCommand command = new MySqlCommand();
            MySqlDataReader myReader = command.ExecuteReader();
            ISet<AccountModel> accountsSet = new HashSet<AccountModel>();
            while (myReader.Read())
            {
                AccountModel userAccount = new AccountModel();
                userAccount.AccountType = myReader.GetString("typeName");
                userAccount.username = myReader.GetString("username");
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
                command.CommandText = $"SELECT * FROM USER U WHERE U.USERNAME = @v1";
                var parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@v1", userAccount!.username);

                command.Parameters.AddRange(parameters);
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;

                MySqlDataReader myReader = command.ExecuteReader();
                AccountModel returnAccount = new AccountModel();
                while (myReader.Read())
                {
                    returnAccount.AccountType = myReader.GetString("typeName");
                    returnAccount.username = myReader.GetString("username");
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
                command.CommandText = $"SELECT * FROM USER U WHERE U.USERNAME = @v1";
                var parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@v1", userAccount!._username);

                command.Parameters.AddRange(parameters);
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;

                MySqlDataReader myReader = command.ExecuteReader();
                DataStoreUser returnUser = new DataStoreUser();
                while (myReader.Read())
                {
                    returnUser.UserId = myReader.GetInt32("userId");
                    returnUser._userType = myReader.GetString("typeName");
                    returnUser._username = myReader.GetString("username");
                    returnUser._password = myReader.GetString("password");
                    returnUser._email = myReader.GetString("email");
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
            using (MySqlCommand command = new MySqlCommand())
            {
                command.CommandText = $"INSERT INTO USER (typeName, username, password, email)" +
                                      $"VALUES (@v1, @v2, @v3, @v4,)";
                var parameters = new SqlParameter[4];
                parameters[0] = new SqlParameter("@v1", userAccount!._userType);
                parameters[1] = new SqlParameter("@v2", userAccount!._username);
                parameters[2] = new SqlParameter("@v3", userAccount!._password);
                parameters[3] = new SqlParameter("@v4", userAccount!._email);

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
        public bool DeleteAccountEntity(DeleteAccountModel userAccount)
        {
            if (!EstablishMariaDBConnection())
            {
                return false;
            }
            var dataStoreUser = new DataStoreUser()
            {
                _username = userAccount.username,
                _password = userAccount.verifiedPassword
            };
            if (!UserNamePasswordDSValidation(dataStoreUser)) return false;

            using (var command = new MySqlCommand())
            {
                command.CommandText = $"DELETE * FROM USER U WHERE U.USERNAME = @v1 AND U.PASSWORD = @v2";
                var parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@v1", userAccount!.username);
                parameters[1] = new SqlParameter("@v2", userAccount!.verifiedPassword);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        private bool UserNamePasswordDSValidation(DataStoreUser userAccount)
        {
            DataStoreUser retrievalAccount;
            using (var command = new MySqlCommand())
            {
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


        //Isabel's Changes Below...probably inefficient, check with team if i'm making duplicate functions
        //Is this necessary, or am I callling the entities incorrectly now?
        public bool SelectAccount(bool flag) //Will continue to work on this to be more effective
        {
            if (!EstablishMariaDBConnection()) Console.WriteLine("Connection failed to open...");
            else Console.WriteLine("Connection opened...");

            MySqlCommand command = new(this.query, mySqlConnection);

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            if (command.ExecuteNonQuery() == 1)
            {
                mySqlConnection!.Close();
                Console.WriteLine("Connection closed...");
                return true;
            }
            while (reader.Read())
            {
                Console.WriteLine(String.Format("{0}", reader[0]));
                if (flag)
                {
                    string message = "Your username is: " + reader[0];
                    sendEmail(message);
                }
            }
            mySqlConnection!.Close();
            Console.WriteLine("Connection closed...");
            return false;

        }
        public void sendEmail(string message) //INEFFICIENT METHOD, Should not have a duplicate method and ideally should avoid a flag
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("projmotomoto@gmail.com");
                mail.To.Add("projmotomoto@gmail.com");
                mail.Subject = "MotoMoto Account Recovery";
                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("projmotomoto@gmail.com", "Tester491!");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
    }
}