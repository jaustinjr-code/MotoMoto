using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.DataAccess.Impementations.UserManagement
{
    public class AccountRecoveryDataAccess
    {
        private MySqlConnection? mySqlConnection { get; set; }

        // Connection string
        private string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";

        // CarBuildDataAccess constructors
        public AccountRecoveryDataAccess() { }
        public AccountRecoveryDataAccess(string connectionString)
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

        public bool FetchLostUsername(string email)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                string getSenderUserIdQuery = "SELECT username FROM Profile where email = '" + email + "'";
                MySqlCommand cmd = new MySqlCommand(getSenderUserIdQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string username = reader["username"].ToString();
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
            return false;
        }

        public bool FetchPasswordEmail(string email)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                string getSenderUserIdQuery = "SELECT username FROM Profile where email = '" + email + "'";
                MySqlCommand cmd = new MySqlCommand(getSenderUserIdQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //string username = reader["username"].ToString();
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
            return false;
        }

        public bool ChangePassword(ChangePasswordModel changePasswordModel)
        {
            MySqlConnection connection = new MySqlConnection(_connectionString);
            try
            {
                connection.Open();
                string getSenderUserIdQuery = "SELECT username FROM Profile where email = '";
                MySqlCommand cmd = new MySqlCommand(getSenderUserIdQuery, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //string username = reader["username"].ToString();
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
            return false;
        }
    }
}
