using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Text;
using TheNewPanelists.ServiceLayer.Logging;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

namespace TheNewPanelists.DataAccessLayer
{
    public class RegistrationDataAccess : IDataAccess
    {
        private string operation;
        private string query { get; set; }
        private MySqlConnection? mySqlConnection = null;

        public RegistrationDataAccess() 
        {
            this.operation = string.Empty;
            this.query = string.Empty;
        }

        public RegistrationDataAccess(string operation, string query)
        {
            this.operation = operation;
            this.query = query;
        }

        public bool EstablishMariaDBConnection()
        {
            Dictionary<string, string> informationLog = new Dictionary<string, string>();

            // Console.WriteLine("Please Enter a Valid Database/Schema: ");
            // string? databaseName = Console.ReadLine();

            // Console.WriteLine("Please Enter Database/Schema password: ");
            // StringBuilder input = new StringBuilder();
            // while (true)
            // {
            //     var key = Console.ReadKey(true);
            //     if (key.Key == ConsoleKey.Enter) break;
            //     if (key.Key == ConsoleKey.Backspace && input.Length > 0) input.Remove(input.Length - 1, 1);
            //     else if (key.Key != ConsoleKey.Backspace) input.Append(key.KeyChar);
            // }
            // string databasePass = input.ToString();

            // string databaseName = "MotoMotoDB";
            // string databasePass = "naeun";
            // MySqlConnection mySqlConnection;
            // This is a hardcoded string, it will be different based on your naming
            // Need to generalize the database name or create a new database and run the restore sql file on it

            /** ROOT CONNECTION PASSWORD IS DIFFERENT FOR EVERYONE!!! PLEASE CHANGE*/
            string connectionString = @$"server=localhost;user=dev_moto;database=dev_um;port=3306;password=motomoto;";
            //connectionString 
            try
            {
                mySqlConnection = new MySqlConnection(connectionString);
                mySqlConnection.Open();

                Console.WriteLine("Connection open");

                // Console.WriteLine("Close");
                //mySqlConnection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("ERROR - Creating new user...");
            }

            return false;
        }

        public Dictionary<string, string> SingleRowQuery()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (!EstablishMariaDBConnection())
            {
                mySqlConnection!.Close();
                Console.WriteLine("Connection failed to open...");
                result.Add("Error", "DataAccess: Connnection to database failed.");
                return result;
            }
            else if (!this.HasValidAttributes())
            {
                mySqlConnection!.Close();
                Console.WriteLine("Connection closed...");
                result.Add("Error", "DataAccess: Invalid Query.");
                return result;
            }
            Console.WriteLine("Connection opened...");

            MySqlCommand command = new MySqlCommand(this.query, this.mySqlConnection);

            MySqlDataReader myReader;
            myReader = command.ExecuteReader();
            string? value = "";
 
            if (myReader.Read())
            {
                if (myReader.HasRows)
                {
                    for (int i = 0; i < myReader.FieldCount; i++)
                    {
                        if (myReader[i] != null)
                            value = myReader[i].ToString();
                            result.Add(myReader.GetName(i), value);
                    }
                }
                else
                    result.Add("Error", "No rows found.");
            }
            else
                result.Add("Error", "DataAccess: Data reader false.");

            Console.WriteLine("Connection closed...");
            mySqlConnection.Close();
            return result;
        }

        // Not yet optimized since not in use
        public List<Dictionary<string, string>> MultiRowQuery()
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            if (!EstablishMariaDBConnection())
            {
                Console.WriteLine("Connection failed to open...");
                return result;
            }
            else Console.WriteLine("Connection opened...");

            MySqlCommand command = new MySqlCommand(this.query, this.mySqlConnection);

            MySqlDataReader myReader;
            myReader = command.ExecuteReader();
            string? value = "";

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    Dictionary<string, string> row = new Dictionary<string, string>();
                    for (int i = 0; i < myReader.FieldCount; i++)
                    {
                        if (myReader[i] != null)
                            value = myReader[i].ToString();

                        row.Add(myReader.GetName(i), value);
                    }

                    result.Add(row);
                }
            }

            mySqlConnection.Close();
            Console.WriteLine("Connection closed...");
            return result;
        }

        public bool SelectAccount()
        {
            if (!EstablishMariaDBConnection())
            {
                mySqlConnection!.Close();
                Console.WriteLine("Connection failed to open...");
                return false;
            }
            else if (!this.HasValidAttributes())
            {
                Console.WriteLine("Invalid query.");
                mySqlConnection!.Close();
                Console.WriteLine("Connection closed...");
                return false;
            }

            MySqlCommand command = new(this.query, mySqlConnection);
            int returnVal = command.ExecuteNonQuery();

            if ((returnVal == 1) || ((this.operation == "REGDOESNOTEXIST") && (returnVal == -1)))
            {
                mySqlConnection!.Close();
                Console.WriteLine("Connection closed...");
                return true;
            }

            Console.WriteLine("Query Request Failed.");
            mySqlConnection!.Close(); 
            Console.WriteLine("Connection closed...");
            return false;
        }

        public bool HasValidAttributes()
        {
            bool hasValidAttributes = false;

            switch (this.operation)
            {
                case "ISVALID":
                    hasValidAttributes = query.Contains("UPDATE Registration r SET r.validated = TRUE WHERE r.email =");
                    break;
                case "DROPREG":
                    hasValidAttributes = query.Contains("DELETE r FROM REGISTRATION r WHERE r.email = ");
                    break;
                case "ACCOUNT REGISTRATION":
                    hasValidAttributes = query.Contains("INSERT INTO REGISTRATION (email, password, expiration) VALUES");
                    break;
                case "RETURNREG":
                    hasValidAttributes = query.Contains("SELECT * FROM Registration r WHERE r.email =");
                    break;
                case "REGDOESNOTEXIST":
                    hasValidAttributes = query.Contains("SELECT * FROM Registration r WHERE r.email =");
                    break;
                case "CONFIRMREG":
                    hasValidAttributes = query.Contains("SELECT r.email, r.password FROM Registration r WHERE r.url =") 
                        && query.Contains("AND NOW() < r.expiration AND r.validated = false");
                    break;
            }
            return hasValidAttributes;
        }
    }
}