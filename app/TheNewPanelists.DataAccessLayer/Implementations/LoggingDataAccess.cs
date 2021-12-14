using System.Linq;
using MySql.Data.MySqlClient;
// using System.Data.Odbc;
using System.Collections;


namespace TheNewPanelists.DataAccessLayer
{
    class LoggingDataAccess : IDataAccess
    {
        private string operation { get; set; }
        private bool isSuccess { get; }
        private Dictionary<string, string> log { get; set; }
        //private MySqlConnection mySqlConnection { get; set; }

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

        // private void BuildTempUser(MySqlConnection mySqlConnection)
        // {
        //     using (mySqlConnection = new MySqlConnection("server=localhost;user=root;"))
        //     using (MySqlCommand cmd = new MySqlCommand("CREATE USER 'tempuser'@'localhost' IDENTIFIED BY '123';", mySqlConnection))
        //     {
        //         try
        //         {
        //             mySqlConnection.Open();
        //             Console.WriteLine("Connection Open...");
        //             cmd.ExecuteNonQuery();
        //             Console.WriteLine("Temp User Created...");
        //         }
        //         catch (Exception e)
        //         {
        //             Console.WriteLine("Exited Program with Exit " + e);
        //         }
        //         mySqlConnection.Close();
        //     }
        //     EstablishMariaDBConnection();
        // }

        public bool EstablishMariaDBConnection()
        {
            MySqlConnection mySqlConnection;
            // This is a hardcoded string, it will be different based on your naming
            string connectionString = "server=localhost;user=admin_MM_test;database=logs_MM_test;port=3306;password=123;";
            // string connectionString = "server=localhost;user=tempuser;database=logs_MM_test;port=3306;";

            try
            {
                mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();
                Console.WriteLine("Connection open");
                // SqlGenerator
                MySqlCommand command = new MySqlCommand(SqlGenerator(), mySqlConnection);
                // Console.WriteLine(command.ExecuteNonQuery());
                if (command.ExecuteNonQuery() == 1)
                {
                    Console.WriteLine("Close");
                    mySqlConnection.Close();

                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // Console.WriteLine("ERROR - Creating new user...");
                // BuildTempUser(mySqlConnection);
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