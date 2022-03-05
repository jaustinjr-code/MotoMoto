using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using MySql.Data.MySqlClient;


namespace TheNewPanelists.ServiceLayer.UsageAnalysisDashboard
{
    class AdmissionAnalyticService : IAnalysisService
    {
        private IDataAccess? _dataAccess;
        private string _operation;
        private IDictionary<string, string>? _dataInfo;
        // Note that an alternative approach is separating each Analytic category into separate Services
        public AdmissionAnalyticService()
        {
            _operation = "GET";
            //_dataAccess = new AdminssionAnalyticDataAccess();
        }
        public AdmissionAnalyticService(IDictionary<string, string> dataInfo)
        {
            _operation = "UPDATE";
            _dataInfo = dataInfo;
        }

        public IList<IList<IDictionary<string, string>>?> GetAnalytics()
        {
            IList<IList<IDictionary<string, string>>?> refinedData = new List<IList<IDictionary<string, string>>?>();
            // Call SqlGenerator()
            string query;
            try
            {
                query = SqlGenerator();
                // May not ever throw b/c queries is allocated memory at the beginning of SqlGenerator
                if (query == null) throw new Exception("Empty query");
            }
            catch (Exception e)
            {
                // Probably best to Log here
                Console.WriteLine(e.StackTrace);
                refinedData.Add(null);
                return refinedData;
            }

            // for (int i = 0; i < query.Length; ++i)
            // {
            // Call Data Access to retrieve MySqlDataReader
            if (_dataAccess == null) _dataAccess = new UsageAnalysisDashboardDataAccess(query);
            //_dataAccess.EstablishMariaDBConnection();
            // Great opportunity to implement async calls to process multiple queries
            MySqlDataReader? reader = ((UsageAnalysisDashboardDataAccess)_dataAccess).SelectIndicatorData();
            if (reader == null) refinedData.Add(null);
            else refinedData.Add(RefineData(reader));
            // }
            // Call RefineData() to refine data


            // FINISH REFINE DATA
            return refinedData;
        }
        public bool UpdateAnalytics()
        {
            try
            {
                // Call SqlGenerator
                string query = SqlGenerator();
                // Debug
                //Console.WriteLine("FIRST QUERY: {0}", query);
                // Call Data Access with update query
                //for (int i = 0; i < query.Length; ++i)
                //{
                _dataAccess = new UsageAnalysisDashboardDataAccess(query);
                if (!((UsageAnalysisDashboardDataAccess)_dataAccess).UpdateIndicatorData()) return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            // }
            // Return bool based on Data Access result
            // Defaults to true, security vulnerability?
            return true;
        }
        private string SqlGenerator()
        {
            // Use operation to determine which query to make
            // string? query;
            string table = "AdmissionAnalytics";
            string[] indicators = { "admissionTotal", "registrationTotal" };
            // string[] queries = new string[2];

            // Are string immutable in C#?
            string query = "";
            if (_operation.Equals("GET"))
            {
                //SELECT *
                //FROM AdmissionAnalytics
                //WHERE accessDate >= NOW() - INTERVAL 3 MONTH;
                query = $"SELECT * FROM {table} WHERE accessDate >= NOW() - INTERVAL 3 MONTH;";
            }
            else if (_operation.Equals("UPDATE"))
            {
                // DataInfo needs to be extracted
                //UPDATE AdmissionAnalytics
                //SET loginTotal = loginTotal + 1;
                //UPDATE AdmissionAnalytics
                //SET registrationTotal = registrationTotal + 1;
                if (_dataInfo != null
                    && _dataInfo.ContainsKey("indicator1")
                    && _dataInfo.ContainsKey("indicator2"))
                {
                    // NEW DATE IS NOT INSERTED BUT STILL RETURNS TRUE
                    if (!InsertCurrentDate()) throw new Exception("Failed insertion");
                    query = $"UPDATE {table} SET {_dataInfo["indicator1"]} = {_dataInfo["indicator1"]} + {_dataInfo["modifier1"]}, {_dataInfo["indicator2"]} = {_dataInfo["indicator2"]} + {_dataInfo["modifier2"]} WHERE {_dataInfo["title"]} = CURDATE();";
                }
            }
            else
            {
                throw new Exception("Empty operation");
            }

            return query;
        }
        private IList<IDictionary<string, string>> RefineData(MySqlDataReader reader)
        {
            IList<IDictionary<string, string>> data = new List<IDictionary<string, string>>();
            IDictionary<string, string> fields = new Dictionary<string, string>();

            // Extract data from reader
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; ++i)
                {
                    string? value = reader.GetValue(i).ToString();
                    if (value != null)
                        fields[$"{reader.GetName(i)}"] = value;
                }

                // Store data into data list
                data.Add(fields);
                fields.Clear();
            }

            return data;
        }

        private bool InsertCurrentDate()
        {
            if (_dataInfo == null) return false;
            // Debug
            //Console.WriteLine("NOT NULL");
            string table = "AdmissionAnalytics";
            string query = $"SELECT COUNT(*) FROM {table} WHERE {_dataInfo["title"]} = CURDATE();";
            try
            {
                IDataAccess select = new UsageAnalysisDashboardDataAccess(query);
                // More efficient to return false if the row doesn't exist and insert all the information in one INSERT statement
                // Instead of inserting the current date separate from the UPDATE statement
                MySqlDataReader? reader = ((UsageAnalysisDashboardDataAccess)select).SelectIndicatorData();
                if (reader == null) throw new Exception("Reader not found");
                reader.Read();
                long result = reader.GetInt64(0);
                if (result == 1) return true;
                else if (result == 0)
                {
                    //INSERT INTO AdmissionAnalytics (accessDate)
                    //VALUES ("2021-2-2");
                    query = $"INSERT INTO {table} ({_dataInfo["title"]}) VALUES (CURDATE());";
                    IDataAccess insert = new UsageAnalysisDashboardDataAccess(query);
                    // NOT PROCESSING QUERY
                    return ((UsageAnalysisDashboardDataAccess)insert).UpdateIndicatorData();
                    //((UsageAnalysisDashboardDataAccess)_dataAccess).InsertIndicatorData();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }
    }
}