using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Text;


namespace TheNewPanelists.DataAccessLayer
{
    class UserManagementDataAccess : IDataAccess
    {
        private string query { get; set; }
        private MySqlConnection mySqlConnection = null;
        public UserManagementDataAccess()
        {
        }
        public UserManagementDataAccess(string query)
        {
            this.query = query;
        }
        private void BuildTempUser()
        {
            // Hides password
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            string pass = input.ToString();
            string user = System.Environment.UserName;


            Console.WriteLine(pass);
            Console.WriteLine(System.Environment.UserName);

            MySqlConnection tempMySqlConnection = new MySqlConnection($"server=localhost;user=root;password={pass}");
            // MySqlConnection tempMySqlConnection = new MySqlConnection($"server=localhost;user={user};password={pass}");
            try
            {
                tempMySqlConnection.Open();
                // MySqlCommand cmd1 = new MySqlCommand("DROP USER IF EXISTS 'tempuser'@'localhost';", tempMySqlConnection);
                MySqlCommand cmd2 = new MySqlCommand("CREATE USER IF NOT EXISTS 'tempuser'@'localhost' IDENTIFIED BY '123';", tempMySqlConnection);
                MySqlCommand cmd3 = new MySqlCommand("GRANT ALL PRIVILEGES ON *.* TO 'tempuser'@'localhost' WITH GRANT OPTION;", tempMySqlConnection);
                MySqlCommand cmd4 = new MySqlCommand("FLUSH PRIVILEGES;", tempMySqlConnection);
                // MySqlCommand cmd4 = new MySqlCommand("SHOW DATABASE LIKE logs_MM_test;", tempMySqlConnection);
                // MySqlCommand cmd5 = new MySqlCommand("CREATE DATABASE IF NOT EXISTS logs_MM_test;", tempMySqlConnection);

                Console.WriteLine("Connection Open...");
                // cmd1.ExecuteNonQuery();
                Console.WriteLine("DROP");
                cmd2.ExecuteNonQuery();
                Console.WriteLine("GRANT");
                cmd3.ExecuteNonQuery();
                Console.WriteLine("FLUSH");
                cmd4.ExecuteNonQuery();
                Console.WriteLine("CREATE");
                // cmd5.ExecuteNonQuery();
                Console.WriteLine("Temp User Created...");
                tempMySqlConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exited Program with Exit " + e.Message);
            }
            EstablishMariaDBConnection();
        }
        public bool EstablishMariaDBConnection()
        {
            // NOTE: hardcoded, will be different based on your naming
            string connectionString = "server=localhost;user=tempuser;database=motomotousermanagement;port=3306;password=123;";
            // @"Data Source=localhost;User ID=admin_MM_test;Password=l23";
            mySqlConnection = new MySqlConnection(connectionString);

            try
            {   
                mySqlConnection.Open();
                MySqlCommand command = new MySqlCommand(this.query, mySqlConnection);
                command.ExecuteNonQuery();
                Console.WriteLine("Connection open");
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Connection " + e + " creating temp user now");
                this.BuildTempUser();
            }
            // SqlGenerator
            // run query and compare against query
            mySqlConnection.Close();
            return false;
        }
        public bool SelectAccount()
        {
            if (!EstablishMariaDBConnection()) Console.WriteLine("Connection failed to open...");
            else Console.WriteLine("Connection opened...");

            MySqlCommand command = new MySqlCommand(this.query, mySqlConnection);
            if (command.ExecuteNonQuery() == 1)
            {
                mySqlConnection.Close();
                Console.WriteLine("Connection closed...");
                return true;
            }
            return false;
        }
    }
}