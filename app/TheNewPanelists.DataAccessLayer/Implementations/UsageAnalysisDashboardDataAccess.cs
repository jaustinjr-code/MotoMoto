using MySql.Data.MySqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace TheNewPanelists.DataAccessLayer
{
    class UsageAnalysisDashboardDataAccess : IDataAccess
    {
        private MySqlConnection? _mySqlConnection;   // Nullable variable
        private string? _query;

        public UsageAnalysisDashboardDataAccess() { }

        public UsageAnalysisDashboardDataAccess(string query)
        {
            _query = query;
            EstablishMariaDBConnection();
        }

        public bool EstablishMariaDBConnection()
        {
            try
            {
                // App.config code for retrieving Connection Strings stored in it here
                // ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["test"];
                // if (settings != null)
                // _mySqlConnection = new MySqlConnection(settings.ConnectionString);
                // Temporarily using hard-coded connection string until App.config works
                _mySqlConnection = new MySqlConnection("server=localhost;user=jcaustin;database=test;port=3306;password=;");
                // else throw new Exception("No connection string found.");
                _mySqlConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // Add Logging Service here
                return false;
            }

            return _mySqlConnection.Ping();
        }

        //private IDictionary<string, string> SelectIndicatorData()
        public MySqlDataReader? SelectIndicatorData()
        {
            MySqlCommand command = new MySqlCommand(_query, _mySqlConnection);
            MySqlDataReader reader;
            //IList<string> values = new List<string>();
            //Object[] values;
            //IDictionary<string, IList<string>> rawData = new Dictionary<string, IList<string>>();
            //IDictionary<string, string> rowValues = new Dictionary<string, string>();
            //IList<IDictionary<string, string>> rawData = new List<IDictionary<string, string>>();
            try
            {
                reader = command.ExecuteReader();
                //values = new Object[reader.FieldCount];
                //while (reader.HasRows)
                //{
                //reader.GetValues(values);
                //List<Object> data = (List<Object>) reader.GetSchemaTable();
                //for (int i = 0; i < reader.FieldCount; ++i)
                //{
                //    string value = reader.GetValue(i).ToString() == null ? "NULL" : reader.GetValue(i).ToString();
                //    rowValues.Add(reader.GetName(i), reader.GetValue(i).ToString());
                //}
                //rawData.Add(rowValues);
                //reader.Read();
                //}
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Command threw error number {0}", e.Number);
                return null;
            }

            return reader;
        }

        public bool UpdateIndicatorData()
        {
            MySqlCommand command = new MySqlCommand(_query, _mySqlConnection);
            // 0 means no rows affected, -1 is error, handle error maybe in the Service Layer
            return command.ExecuteNonQuery() == -1 ? false : true;
        }
    }
}