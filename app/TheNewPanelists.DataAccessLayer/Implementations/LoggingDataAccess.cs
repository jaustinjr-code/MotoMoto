using System.Linq;
using MySql.Data.MySqlClient;
// using System.Data.Odbc;
using System.Collections;
using System.Text;


namespace TheNewPanelists.DataAccessLayer
{
    class LoggingDataAccess : IDataAccess
    {
        private string operation { get; set; }
        private bool isSuccess { get; }
        private Dictionary<string, string> log { get; set; }
        private MySqlConnection mySqlConnection = null;

        public LoggingDataAccess(string operation, bool isSuccess)//, Dictionary<string, string> log)
        {
            try
            {
                if (operation == "CREATE")
                {
                    this.operation = operation;
                    Console.WriteLine("CREATE");
                }
                else if (operation == "DELETE")
                {
                    this.operation = operation;
                    Console.WriteLine("DELETE");
                }
                else if (operation == "UPDATE")
                {
                    this.operation = operation;
                    Console.WriteLine("UPDATE");
                }
                else if (operation == "ABLE")
                {
                    this.operation = operation;
                    Console.WriteLine("ABLE");
                }
                else
                {
                    this.operation = "None";
                    //throw Exception;
                }
                this.isSuccess = isSuccess;
                //this.log = new Dictionary<string, string>(log);
                //this.EstablishMariaDBConnection();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool LogAccess(Dictionary<string, string> log)
        {
            this.log = log;
            //for (int i = 0; i < log.Length; ++i)
            //{
            //    Console.WriteLine(log[i]);
            //}

            this.EstablishMariaDBConnection();


            return false;
        }

        private void BuildTempUser(MySqlConnection mySqlConnection)
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


            //Console.WriteLine(pass);
            //Console.WriteLine(System.Environment.UserName);

            MySqlConnection tempMySqlConnection = new MySqlConnection($"server=localhost;user={user};password={pass}");
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
            // MySqlConnection mySqlConnection;
            // This is a hardcoded string, it will be different based on your naming
            // Need to generalize the database name or create a new database and run the restore sql file on it
            string connectionString = "server=localhost;user=tempuser;database=logs_MM_test;port=3306;password=123;";
            // string connectionString = "server=localhost;user=tempuser;database=logs_MM_test;port=3306;";

            try
            {
                mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                Console.WriteLine("Connection open");
                // SqlGenerator
                MySqlCommand command = new MySqlCommand(SqlGenerator(), mySqlConnection);
                // Console.WriteLine(command.ExecuteNonQuery());
                // if (command.ExecuteNonQuery() == 1)
                // {
                Console.WriteLine("Close");
                mySqlConnection.Close();

                return true;
                // }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("ERROR - Creating new user...");
                BuildTempUser(mySqlConnection);
            }

            return false;
        }

        public string SqlGenerator()
        {
            //MySqlCommand mySqlCommand = new MySqlCommand()
            //string commandSql = "INSERT INTO Category VALUES (NULL,\"Business\")";
            //string commandSql = "SELECT * FROM Category"; 

            //string commandSql = $"INSERT INTO Log VALUES (NULL, {categoryName}, {levelName}, NULL, {userID}, \"{operation} : {(isSuccess ? "Success" : "Failure")}\")";
            string commandSql = $@"INSERT INTO Log (logId, categoryName, levelName, userID, DSCRIPTION)
                                VALUES (NULL, '{log["categoryname"].ToUpper()}', '{log["levelname"].ToUpper()}',
                                {log["userid"]}, '{operation} : {(isSuccess ? "Success" : "Failure")}')";
            // string dateTime = DateTime.Now.ToString("G");
            // string commandSql = $"INSERT INTO Log VALUES (NULL, '{log["categoryname"].ToUpper()}', '{log["levelname"].ToUpper()}', '{dateTime}', {log["userid"]}, '{operation} : {(isSuccess ? "Success" : "Failure")}')";
            // string commandSql = "INSERT INTO Log (logId, categoryName, levelName, userID, DSCRIPTION) VALUES (NULL, '" +
            //                     log["categoryname"].ToUpper() + "', '" + log["levelname"].ToUpper() + "', " + log["userid"] + ", '" + operation + " : " +
            //                     (isSuccess ? "Success" : "Failure") + "')";
            Console.WriteLine(commandSql);
            return commandSql;
        }
    }
}