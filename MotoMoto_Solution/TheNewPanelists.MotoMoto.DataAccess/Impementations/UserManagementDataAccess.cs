using System;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using TheNewPanelists.MotoMoto.DataAccess.Contracts;
using TheNewPanelists.MotoMoto.Entities;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;

namespace TheNewPanelists.MotoMoto.DataAccess.Impementations
{
    public class UserManagementDataAccess : IDataAccess
    {
        private string? _query { get; set; }
        private MySqlConnection? mySqlConnection = null;

        private const string connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;";

        public UserManagementDataAccess() { }
        
        public UserManagementDataAccess(string query)   
        {
            _query = query;
        }

        public bool EstablishMariaDBConnection()
        {
            try
            {
                mySqlConnection = new MySqlConnection(connectionString);
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
        /// Select account operation will execute any query that it deems necessary. Select account
        /// operation will not return any users, and will be separate operations for specified return API Calls
        /// </summary>
        /// <returns>true if operation went through, otherwise we exit with failure</returns>
        public virtual bool SelectAccountOperation()
        {
            if (!EstablishMariaDBConnection()) return false;

            MySqlCommand command = new(_query, mySqlConnection);

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

        public ISet<AccountEntity> GetAllUsers()
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            MySqlCommand command = new MySqlCommand(_query, mySqlConnection);
            MySqlDataReader myReader = command.ExecuteReader();
            ISet<AccountEntity> accountsSet = new HashSet<AccountEntity>();
            while (myReader.Read())
            {
                AccountEntity userAccount = new AccountEntity();
                userAccount.UserId = myReader.GetInt32("userId");
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
        public AccountEntity RetrieveSpecifiedUserEntity()
        {   
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand(_query, mySqlConnection))
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;

                MySqlDataReader myReader = command.ExecuteReader();
                AccountEntity userAccount = new AccountEntity();

                while (myReader.Read())
                {
                    userAccount.UserId = myReader.GetInt32("userId");
                    userAccount.AccountType = myReader.GetString("typeName");
                    userAccount.username = myReader.GetString("username");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return userAccount;
            }           
        }

        public DataStoreUser RetrieveDataStoreSpecifiedUserEntity()
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand(_query, mySqlConnection))
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;

                MySqlDataReader myReader = command.ExecuteReader();
                DataStoreUser userAccount = new DataStoreUser();

                while (myReader.Read())
                {
                    userAccount.UserId = myReader.GetInt32("userId");
                    userAccount._userType = myReader.GetString("typeName");
                    userAccount._username = myReader.GetString("username");
                    userAccount._password = myReader.GetString("password");
                    userAccount._email = myReader.GetString("email");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return userAccount;
            }
        }
        public ProfileEntity RetrieveSpecifiedProfileEntity()
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand(_query, mySqlConnection))
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;

                MySqlDataReader myReader = command.ExecuteReader();
                ProfileEntity userProfile = new ProfileEntity();

                while (myReader.Read())
                {
                    userProfile.UserId = myReader.GetInt32("userId");
                    userProfile.username = myReader.GetString("username");
                    userProfile.status = myReader.GetBoolean("status");
                    userProfile.status = myReader.GetBoolean("eventAccount");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return userProfile;
            }
        }
    }
}