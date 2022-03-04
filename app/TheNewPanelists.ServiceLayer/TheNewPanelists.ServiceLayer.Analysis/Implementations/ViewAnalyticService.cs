using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using MySql.Data.MySqlClient;


namespace TheNewPanelists.ServiceLayer.UsageAnalysisDashboard
{
    class ViewAnalyticService : IAnalysisService
    {
        private IDataAccess? _dataAccess;
        private string _operation;
        private IDictionary<string, string>? _dataInfo;
        // Note that an alternative approach is separating each Analytic category into separate Services
        public ViewAnalyticService()
        {
            _operation = "GET";
            // _dataAccess = new ViewAnalyticDataAccess();
        }
        public ViewAnalyticService(IDictionary<string, string> dataInfo)
        {
            _operation = "UPDATE";
            _dataInfo = dataInfo;
        }

        public IList<IList<IDictionary<string, string>>?> GetAnalytics()
        {
            IList<IList<IDictionary<string, string>>?> refinedData = new List<IList<IDictionary<string, string>>?>();
            // Call SqlGenerator()
            string[] queries;
            try
            {
                queries = SqlGenerator();
                // May not ever throw b/c queries is allocated memory at the beginning of SqlGenerator
                if (queries == null) throw new Exception("Empty query");
            }
            catch (Exception e)
            {
                // Probably best to Log here
                Console.WriteLine(e.StackTrace);
                refinedData.Add(null);
                return refinedData;
            }

            for (int i = 0; i < queries.Length; ++i)
            {
                // Call Data Access to retrieve MySqlDataReader
                if (_dataAccess == null) _dataAccess = new UsageAnalysisDashboardDataAccess(queries[i]);
                //_dataAccess.EstablishMariaDBConnection();
                // Great opportunity to implement async calls to process multiple queries
                MySqlDataReader? reader = ((UsageAnalysisDashboardDataAccess)_dataAccess).SelectIndicatorData();
                if (reader == null) refinedData.Add(null);
                else refinedData.Add(RefineData(reader));
            }
            // Call RefineData() to refine data


            // FINISH REFINE DATA
            return refinedData;
        }
        public bool UpdateAnalytics()
        {
            // Call SqlGenerator
            string[] queries = SqlGenerator();
            // Call Data Access with update query
            for (int i = 0; i < queries.Length; ++i)
            {
                _dataAccess = new UsageAnalysisDashboardDataAccess(queries[i]);
                if (!((UsageAnalysisDashboardDataAccess)_dataAccess).UpdateIndicatorData()) return false;
            }
            // Return bool based on Data Access result
            // Defaults to true, security vulnerability?
            return true;
        }
        private string[] SqlGenerator()
        {
            // Use operation to determine which query to make
            // string? query;
            string table = "ViewAnalytics";
            string[] indicators = { "displayTotal", "durationAvg" };
            string[] queries = new string[2];
            if (_operation.Equals("GET"))
            {
                //SELECT displayTotal FROM ViewAnalytics ORDER BY displayTotal DESC LIMIT 5;
                for (int i = 0; i < indicators.Length; ++i)
                {
                    queries[i] = $"SELECT {indicators[i]} FROM {table} ORDER BY {indicators[i]} DESC LIMIT 5;";
                }
            }
            else if (_operation.Equals("UPDATE"))
            {
                // DataInfo needs to be extracted
                // UPDATE ViewAnalytics
                // SET displayTotal = displayTotal + 1,
                //     -- durationAvg = (durationAvg + [duration]) / displayTotal
                //     durationAvg = (durationAvg + 5) / displayTotal
                // WHERE viewTitle LIKE "View2";
                if (_dataInfo != null
                    && _dataInfo.ContainsKey("indicator1")
                    && _dataInfo.ContainsKey("indicator2"))
                {
                    queries[0] = $"UPDATE {table} SET {_dataInfo["indicator1"]} = {_dataInfo["indicator1"]} + {_dataInfo["modifier1"]} WHERE {_dataInfo["title"]} LIKE \'{_dataInfo["titleName"]}\';";
                    queries[1] = $"UPDATE {table} SET {_dataInfo["indicator2"]} = ({_dataInfo["indicator2"]} + {_dataInfo["modifier2"]}) / {_dataInfo["indicator1"]} WHERE {_dataInfo["title"]} LIKE \'{_dataInfo["titleName"]}\';";
                }
            }
            else
            {
                throw new Exception("Empty operation");
            }

            return queries;
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
    }
}