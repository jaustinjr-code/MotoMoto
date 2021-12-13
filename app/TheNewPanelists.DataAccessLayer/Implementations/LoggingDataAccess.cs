using System.Linq;


namespace TheNewPanelists.DataAccessLayer.Logging
{
    class LoggingDataAccess : IDataAccess
    {
        private string operation { get; set; }

        public LoggingDataAccess(string operation)
        {
            try
            {
                if (operation == "CREATE")
                {
                    this.operation = operation;
                    Console.WriteLine("CREATE");
                }
                else
                {
                    this.operation = "No";
                    Console.WriteLine("No");
                }
                // else if (operation == "DELETE")
                // {

                // }
                // else if (operation == "UPDATE")
                // {

                // }
                // else if (operation == "ABLE")
                // {

                // }
                // else
                // {
                //     throw Exception;
                // }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // this.operation = "No";

        }

        bool IDataAccess.EstablishMariaDBConnection()
        {
            // MySqlConnection mySqlConnection;
            // string connectionString = @"Data Source=localhost;User ID=admin_MM_test;Password=l23";
            // sqlConnection = new SqlConnection(connectionString);
            // sqlConnection.Open();

            // // SqlGenerator
            // // run query and compare against query

            // sqlConnection.Close();

            return false;
        }

        string IDataAccess.SqlGenerator()
        {
            return "";
        }
    }
}