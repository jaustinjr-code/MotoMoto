using TheNewPanelists.DataAccessLayer;
using MySql.Data.MySqlClient;
using TheNewPanelists.ServiceLayer.Logging;

namespace TheNewPanelists.ServiceLayer.UsageAnalysisDashboard
{
    class CommunityBoardAnalyticService : IAnalysisService
    {
        IDictionary<string, string>? _dataInfo;
        string _operation;
        IDataAccess? _dataAccess;
        public CommunityBoardAnalyticService()
        {
            _operation = "GET";
        }
        public CommunityBoardAnalyticService(IDictionary<string, string> dataInfo)
        {
            _operation = "UPDATE";
            _dataInfo = dataInfo;
        }

        public IList<IList<IDictionary<string, string>>?> GetAnalytics()
        {
            IList<IList<IDictionary<string, string>>?> refinedData = new List<IList<IDictionary<string, string>>?>();
            string query;

            try
            {
                query = SqlGenerator();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                refinedData.Add(null);
                return refinedData;
            }

            _dataAccess = new UsageAnalysisDashboardDataAccess(query);
            MySqlDataReader? reader = ((UsageAnalysisDashboardDataAccess)_dataAccess).SelectIndicatorData();
            if (reader == null) refinedData.Add(null);
            else refinedData.Add(RefineData(reader));

            return refinedData;
        }

        public bool UpdateAnalytics()
        {
            if (_dataInfo == null) return false;
            //UPDATE CommunityBoardAnalytics
            //SET feedPostTotal = feedPostTotal + 20 -- SET feedPostTotal = feedPostTotal + [total new posts in database]
            //-- NOTE: can link to Community Board table to update this value w/ foreign key
            //WHERE feedTitle LIKE "Supercar";
            string table = "CommunityBoardAnalytics";
            string query = $"UPDATE {table} SET {_dataInfo["indicator"]} = {_dataInfo["indicator"]} + {_dataInfo["modifier"]};";
            _dataAccess = new UsageAnalysisDashboardDataAccess(query);
            return ((UsageAnalysisDashboardDataAccess)_dataAccess).UpdateIndicatorData();
        }

        private string SqlGenerator()
        {
            string table = "CommunityBoardAnalytics";
            string query;
            if (_operation.Equals("GET"))
            {
                //SELECT * FROM CommunityBoardAnalytics ORDER BY feedPostTotal DESC LIMIT 5;
                query = $"SELECT * FROM {table} ORDER BY feedPostTotal DESC LIMIT 5;";
            }
            else if (_operation.Equals("UPDATE"))
            {
                if (_dataInfo != null
                    && _dataInfo.ContainsKey("indicator"))
                {
                    query = $"UPDATE {table} SET {_dataInfo["indicator"]} = {_dataInfo["indicator"]} + {_dataInfo["modifier"]} WHERE {_dataInfo["title"]} = {_dataInfo["titleName"]};";
                }
                else
                {
                    throw new Exception("Data info incomplete");
                }
            }
            else
            {
                throw new Exception("No operation found");
            }
            return query;
        }

        private IList<IDictionary<string, string>>? RefineData(MySqlDataReader reader)
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