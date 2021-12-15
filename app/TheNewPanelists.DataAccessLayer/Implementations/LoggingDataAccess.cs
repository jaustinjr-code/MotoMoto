using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Text;


namespace TheNewPanelists.DataAccessLayer
{
    class LoggingDataAccess : IDataAccess
    {
        private string operation { get; }
        private bool isSuccess { get; }
        private Dictionary<string, string> log = null;
        private MySqlConnection mySqlConnection = null;

        /**
        Constructor for Logging Data Access
        Assigns operation and isSuccess
        operation - the operation performed
        isSuccess - whether the operation is successful or not
        */
        public LoggingDataAccess(string operation, bool isSuccess)
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
            }
            this.isSuccess = isSuccess;
        }

        /**
        LogAccess gives access to database and stores the log
        log - necessary fields for the log
        */
        public bool LogAccess(Dictionary<string, string> log)
        {
            this.log = log;
            if (!EstablishMariaDBConnection()) Console.WriteLine("Connection failed to open...");
            else Console.WriteLine("Connection opened...");

            MySqlCommand command = new MySqlCommand(SqlGenerator(), mySqlConnection);
            if (command.ExecuteNonQuery() == 1)
            {
                mySqlConnection.Close();
                Console.WriteLine("Connection closed...");
                return true;
            }
            return false;
        }

        /**
        ExtractLogs returns the logs older than 30 days
        */
        public List<Dictionary<string, string>> ExtractLogs()
        {
            if (!EstablishMariaDBConnection()) Console.WriteLine("Connection failed to open...");
            else Console.WriteLine("Connection opened...");
            MySqlCommand command = new MySqlCommand(SqlGenerator(), mySqlConnection);
            List<Dictionary<string, string>> result = ReadResult(command.ExecuteReader());
            mySqlConnection.Close();
            Console.WriteLine("Connection closed...");
            return result;
        }

        /**
        BuildTempUser builds a temporary user if none already exists
        Grants privileges so tempuser can access the database
        Prompts user to enter password associated to their machine to access MariaDB console
        Reruns the EstablishMariaDBConnection
        */
        private void BuildTempUser()
        {
            // Hides password inputted in the console
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
                Console.WriteLine("Connection opened...");
                //MySqlCommand cmd1 = new MySqlCommand("DROP USER IF EXISTS 'tempuser'@'localhost';", tempMySqlConnection);
                MySqlCommand cmd2 = new MySqlCommand("CREATE USER IF NOT EXISTS 'tempuser'@'localhost' IDENTIFIED BY '123';", tempMySqlConnection);
                MySqlCommand cmd3 = new MySqlCommand("GRANT ALL PRIVILEGES ON *.* TO 'tempuser'@'localhost' WITH GRANT OPTION;", tempMySqlConnection);
                MySqlCommand cmd4 = new MySqlCommand("FLUSH PRIVILEGES;", tempMySqlConnection);
                //MySqlCommand cmd4 = new MySqlCommand("SHOW DATABASE LIKE logs_MM_test;", tempMySqlConnection);
                //MySqlCommand cmd5 = new MySqlCommand("CREATE DATABASE IF NOT EXISTS logs_MM_test;", tempMySqlConnection);

                //cmd1.ExecuteNonQuery();
                //Console.WriteLine("DROP");
                cmd2.ExecuteNonQuery();
                //Console.WriteLine("GRANT");
                cmd3.ExecuteNonQuery();
                //Console.WriteLine("FLUSH");
                cmd4.ExecuteNonQuery();
                //Console.WriteLine("CREATE");
                //cmd5.ExecuteNonQuery();
                Console.WriteLine("Temp User Created...");
                tempMySqlConnection.Close();
                Console.WriteLine("Connection closed...");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exited Program with Exit " + e.Message);
            }
            // Reruns to establish connection
            EstablishMariaDBConnection();
        }

        /**
        ReadResult reads the query result and returns the list of logs older than 30 days
        */
        private List<Dictionary<string, string>> ReadResult(MySqlDataReader mySqlDataReader)
        {
            List<Dictionary<string, string>> output = new List<Dictionary<string, string>>();
            //MySqlDataReader mySqlDataReader = command.ExecuteReader();
            while (mySqlDataReader.Read())
            {
                Dictionary<string, string> data = new Dictionary<string, string>();
                for (int i = 0; i < mySqlDataReader.FieldCount; ++i)
                {
                    //Console.Write(mySqlDataReader[i] + "\t");
                    //if (mySqlDataReader.GetName(i) == "timeStamp")
                    //{
                    //data.Add(mySqlDataReader.GetName(i), new DateTime(mySqlDataReader[i]).ToString("yyyy-MM-dd HH:mm:ss"));
                    //Console.WriteLine(mySqlDataReader.GetName(i) + " " + Convert.ToDateTime(mySqlDataReader[i].ToString()).ToString());
                    //}
                    //else
                    //{
                    //data.Add(mySqlDataReader.GetName(i), mySqlDataReader[i].ToString());
                    //Console.WriteLine(mySqlDataReader.GetName(i) + " " + mySqlDataReader[i].ToString());
                    //}

                }
                output.Add(data);
            }
            return null;
        }

        /**
        EstablishMariaDBConnection establishes the connection to MariaDB logs database
        NOTE: the database name is not generalized and needs to be fixed
        */
        public bool EstablishMariaDBConnection()
        {
            //MySqlConnection mySqlConnection;

            // This is a hardcoded string, it will be different based on your naming
            // Need to generalize the database name or create a new database and run the restore sql file on it
            string connectionString = "server=localhost;user=tempuser;database=logs_MM_test;port=3306;password=123;";
            //string connectionString = "server=localhost;user=tempuser;database=logs_MM_test;port=3306;";

            try
            {
                mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("ERROR - Creating new user...");
                BuildTempUser();
            }

            return false;
        }

        /**
        SqlGenerator generates the appropriate sql statement
        */
        public string SqlGenerator()
        {
            //MySqlCommand mySqlCommand = new MySqlCommand()
            //string commandSql = "INSERT INTO Category VALUES (NULL,\"Business\")";
            //string commandSql = "SELECT * FROM Category"; 

            //string commandSql = $"INSERT INTO Log VALUES (NULL, {categoryName}, {levelName}, NULL, {userID}, \"{operation} : {(isSuccess ? "Success" : "Failure")}\")";
            string commandSql;
            // Generates the statement for inserting new logs
            if (this.log != null)
            {
                commandSql = $@"INSERT INTO Log (logId, categoryName, levelName, userID, DSCRIPTION)
                                VALUES (NULL, '{log["categoryname"].ToUpper()}', '{log["levelname"].ToUpper()}',
                                {log["userid"]}, '{operation} : {(isSuccess ? "Success" : "Failure")}')";
            }
            // Generates the statement for returning the logs older than 30 days
            else
            {
                commandSql = $"SELECT * FROM Log WHERE DATEDIFF(NOW(), timeStamp) > 30";
            }

            //string dateTime = DateTime.Now.ToString("G");
            //string commandSql = $"INSERT INTO Log VALUES (NULL, '{log["categoryname"].ToUpper()}', '{log["levelname"].ToUpper()}', '{dateTime}', {log["userid"]}, '{operation} : {(isSuccess ? "Success" : "Failure")}')";
            //string commandSql = "INSERT INTO Log (logId, categoryName, levelName, userID, DSCRIPTION) VALUES (NULL, '" +
            //log["categoryname"].ToUpper() + "', '" + log["levelname"].ToUpper() + "', " + log["userid"] + ", '" + operation + " : " +
            //(isSuccess ? "Success" : "Failure") + "')";

            //Console.WriteLine(commandSql);
            return commandSql;
        }
    }
}