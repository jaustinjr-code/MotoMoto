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
                //ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["test"];
                //if (settings != null)
                //_mySqlConnection = new MySqlConnection(settings.ConnectionString);

                // Check if SqlConnection is already open
                //if (_mySqlConnection != null && _mySqlConnection.State == System.Data.ConnectionState.Closed)
                //_mySqlConnection.Open();
                //else if (_mySqlConnection == null)
                //{
                // Temporarily using hard-coded connection string until App.config works
                _mySqlConnection = new MySqlConnection("server=localhost;user=jcaustin;database=test;port=3306;password=;");
                //else throw new Exception("No connection string found.");
                _mySqlConnection.Open();
                //}
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
            // Parameter.AddWithValue could be used instead of String Interpolation in the Service Layer
            MySqlDataReader reader;
            //IList<string> values = new List<string>();
            //Object[] values;
            //IDictionary<string, IList<string>> rawData = new Dictionary<string, IList<string>>();
            //IDictionary<string, string> rowValues = new Dictionary<string, string>();
            //IList<IDictionary<string, string>> rawData = new List<IDictionary<string, string>>();
            try
            {
                MySqlCommand command = new MySqlCommand(_query, _mySqlConnection);
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
            //finally
            //{
            //_mySqlConnection.Close();
            //}

            return reader;
        }

        public bool UpdateIndicatorData()
        {
            bool isUpdated = false;
            try
            {
                MySqlCommand command = new MySqlCommand(_query, _mySqlConnection);
                // 0 means no rows affected, -1 is error or anything else was returned, handle error maybe in the Service Layer
                int result = command.ExecuteNonQuery();
                if (result < 0) throw new Exception("Improper query");
                isUpdated = result == 0 ? false : true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                // Log here
            }
            return isUpdated;
        }

        // public bool InsertIndicatorData()
        // {
        //     bool isInserted = false;
        //     try
        //     {
        //         MySqlCommand command = new MySqlCommand(_query, _mySqlConnection);
        //         int result = command.ExecuteNonQuery();
        //         if (result < 0) throw new Exception("Improper query");
        //         isInserted = result == 0 ? false : true;
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e.StackTrace);
        //         // Log here
        //     }
        //     return isInserted;
        // }
    }
}