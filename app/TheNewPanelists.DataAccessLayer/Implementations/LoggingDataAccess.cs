using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Text;
using TheNewPanelists.ServiceLayer.Logging;

namespace TheNewPanelists.DataAccessLayer
{
    public class LoggingDataAccess : IDataAccess
    {
        private string? query { get; set; }
        private MySqlConnection? mySqlConnection = null;

        public LoggingDataAccess() {}

        public LoggingDataAccess(string query)
        {
            this.query = query;
        }

        private void BuildTempUser()
        {
            // Hides password
            Console.WriteLine("Please Enter Your MariaDB Username:");
            string? username = Console.ReadLine();
            Console.WriteLine($"Please Enter the password for {username}:");
            StringBuilder input = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter) break;
                if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
                else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            }
            string pass = input.ToString();
            // Console.WriteLine(pass);
            // Console.WriteLine(System.Environment.UserName);

            MySqlConnection tempMySqlConnection = new MySqlConnection($"server=localhost;user={username};password={pass}");
            // MySqlConnection tempMySqlConnection = new MySqlConnection($"server=localhost;user={user};password={pass}");
            try
            {
                tempMySqlConnection.Open();
                MySqlCommand cmd1 = new MySqlCommand("DROP USER IF EXISTS 'tempuser'@'localhost';", tempMySqlConnection);
                MySqlCommand cmd2 = new MySqlCommand("CREATE USER IF NOT EXISTS 'tempuser'@'localhost' IDENTIFIED BY '123456abcdefg';", tempMySqlConnection);
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
            Dictionary<string, string> informationLog = new Dictionary<string, string>();

            Console.WriteLine("Please Enter a Valid Database/Schema: ");

            string? databaseName = Console.ReadLine();
            // MySqlConnection mySqlConnection;
            // This is a hardcoded string, it will be different based on your naming
            // Need to generalize the database name or create a new database and run the restore sql file on it
            string connectionString = $"server=localhost;user=root;database=motomotologs;port=3306;password=123456;";


            try
            {
                mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                Console.WriteLine("Connection open");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("ERROR - Creating new user...");
                BuildTempUser();
            }

            return false;
        }

        /**
        LogAccess gives access to database and stores the log
        log - necessary fields for the log
        */
        public bool LogAccess()
        {
            Dictionary<string, string> informationLog = new Dictionary<string, string>();
            if (!EstablishMariaDBConnection()) Console.WriteLine("Connection failed to open...");
            else Console.WriteLine("Connection opened...");

            MySqlCommand command = new MySqlCommand(this.query, mySqlConnection);

            if (command.ExecuteNonQuery() == 1)
            {
                mySqlConnection!.Close();
                Console.WriteLine("Connection closed...");
                
                return true;
            }
            mySqlConnection!.Close();

            Console.WriteLine("Connection closed...");
            return false;
        }

        public List<Dictionary<string, string>> ExtractLogs()
        {
            if (!EstablishMariaDBConnection()) Console.WriteLine("Connection failed to open...");
            else Console.WriteLine("Connection opened...");
            MySqlCommand cmd = new MySqlCommand("SET character_set_results=utf8", mySqlConnection);
            MySqlCommand command = new MySqlCommand(this.query, mySqlConnection);
            List<Dictionary<string, string>> result = ReadResult(command.ExecuteReader());
            mySqlConnection!.Close();
            Console.WriteLine("Connection closed...");
            return result;
        }

         private List<Dictionary<string, string>> ReadResult(MySqlDataReader mySqlDataReader)
        {
            List<Dictionary<string, string>> output = new List<Dictionary<string, string>>();
            //MySqlDataReader mySqlDataReader = command.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                for (int i = 0; i < mySqlDataReader.FieldCount; ++i)
                {
                        data.Add(mySqlDataReader.GetName(i), mySqlDataReader[i].ToString()!);
                        Console.WriteLine(mySqlDataReader.GetName(i) + " " + mySqlDataReader[i].ToString());
                }
                output.Add(data);
            }
            return output;
        }
    }
}