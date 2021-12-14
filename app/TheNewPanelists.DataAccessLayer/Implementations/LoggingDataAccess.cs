using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections;


namespace TheNewPanelists.DataAccessLayer
{
    class LoggingDataAccess : IDataAccess
    {
        private string operation { get; set; }
        private bool isSuccess { get; }
        private string[] log { get; set; }
        // private MySqlConnection mySqlConnection { get; set; }

        public LoggingDataAccess(string operation, bool isSuccess)
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
                    Console.WriteLine("CREATE");
                }
                else if (operation == "UPDATE")
                {
                    this.operation = operation;
                    Console.WriteLine("CREATE");
                }
                else if (operation == "ABLE")
                {
                    this.operation = operation;
                    Console.WriteLine("CREATE");
                }
                else
                {
                    this.operation = "None";
                    // throw Exception;
                }
                this.isSuccess = isSuccess;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool LogAccess(string[] log)
        {
            this.log = log;
            // for (int i = 0; i < log.Length; ++i)
            // {
            //     Console.WriteLine(log[i]);
            // }

            this.EstablishMariaDBConnection();


            return false;
        }

        public bool EstablishMariaDBConnection()
        {
            MySqlConnection mySqlConnection;
            // NOTE: hardcoded, will be different based on your naming
            string connectionString = "server=localhost;user=admin_MM_test;database=logs_MM_test;port=3306;password=123;";

            mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();

            try
            {
                Console.WriteLine("Connection open");
                // SqlGenerator
                MySqlCommand command = new MySqlCommand(SqlGenerator(), mySqlConnection);
                command.ExecuteNonQuery();
                // MySqlDataReader mySqlDataReader = command.ExecuteReader();
                // while (mySqlDataReader.Read())
                // {
                //     Console.WriteLine(mySqlDataReader[0] + " " + mySqlDataReader[1]);
                // }
                // run query and compare against query
                // mySqlDataReader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            mySqlConnection.Close();

            return false;
        }

        public string SqlGenerator()
        {
            // MySqlCommand mySqlCommand = new MySqlCommand()
            // string commandSql = "INSERT INTO Category VALUES (NULL,\"Business\")";
            // string commandSql = "SELECT * FROM Category"; 

            // string commandSql = $"INSERT INTO Log VALUES (NULL, {categoryName}, {levelName}, NULL, {userID}, \"{operation} : {(isSuccess ? "Success" : "Failure")}\")";
            string commandSql = $"INSERT INTO Log (logId, categoryName, levelName, userID, DSCRIPTION) VALUES (NULL, \"{log[0].ToUpper()}\", \"{log[1].ToUpper()}\", {log[2]}, \"{operation} : {(isSuccess ? "Success" : "Failure")}\")";
            Console.WriteLine(commandSql);
            return commandSql;
        }
    }
}