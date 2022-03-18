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
using TheNewPanelists.MotoMoto.DataStoreEntities;

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
        public bool SelectAccountOperation()
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
    }
}

