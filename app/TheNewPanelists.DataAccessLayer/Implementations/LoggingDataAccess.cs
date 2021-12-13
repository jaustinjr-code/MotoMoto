using System.Linq;
using MySql.Data.MySqlClient;


namespace TheNewPanelists.DataAccessLayer
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // this.operation = "No";

        }

        bool IDataAccess.EstablishMariaDBConnection()
        {
            MySqlConnection mySqlConnection;
            // NOTE: hardcoded, will be different based on your naming
            string connectionString = "server=localhost;user=admin_MM_test;database=users_MM_test;port=3306;password=123";// @"Data Source=localhost;User ID=admin_MM_test;Password=l23";

            mySqlConnection = new MySqlConnection(connectionString);
            mySqlConnection.Open();

            // SqlGenerator
            // run query and compare against query
            Console.WriteLine("Connection open");

            mySqlConnection.Close();

            return false;
        }

        string IDataAccess.SqlGenerator()
        {

            return "";
        }
    }
}