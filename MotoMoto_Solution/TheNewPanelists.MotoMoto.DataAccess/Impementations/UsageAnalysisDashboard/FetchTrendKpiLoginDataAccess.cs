using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class FetchTrendKpiLoginDataAccess : MariaDBConnectionBase, IFetchKpiDataAccess
    {
        private MySqlConnection? _mySqlConnection;
        private string _connectionString;

        public FetchTrendKpiLoginDataAccess()
        {
            // Use App.config here
            _connectionString = "server=localhost;user=dev_moto;database=dev_UAD;port=3306;password=motomoto;";
        }

        /// <summary>
        /// Refine the chart metrics from the MySqlDataReader into an IUsageAnalyticEntity 
        /// </summary>
        /// <param name="reader">MySqlDataReader</param>
        /// <param name="analyticModel">IUsageAnalyticModel</param>
        /// <returns>IUsageAnalyticEntity</returns>
        private IUsageAnalyticEntity RefineData(MySqlDataReader reader, IUsageAnalyticModel analyticModel)
        {
            if (reader.HasRows)
            {
                IEnumerable<IAxisDetailsEntity> metrics = new List<IAxisDetailsEntity>();
                try
                {
                    while (reader.Read())
                    {
                        string x = Convert.ToDateTime(reader["accessDate"]).ToString("MM/dd/yyyy");
                        string y = reader.GetInt32("loginTotal").ToString();
                        IAxisDetailsEntity metric = new DataStoreAxisDetails(x, y);
                        ((List<IAxisDetailsEntity>)metrics).Add(metric);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                    throw new Exception("Failed to Add Result");
                }
                IUsageAnalyticEntity analytic = new DataStoreUsageAnalyticTrend(analyticModel.x_axis, analyticModel.y_axis, metrics);
                return analytic;
            }
            throw new Exception("No Rows Found");
        }

        /// <summary>
        /// Fetch the Trend Chart metrics from Login Analytics
        /// </summary>
        /// <param name="analyticModel">IUsageAnalyticModel</param>
        /// <returns>IUsageAnalyticEntity</returns>
        public IUsageAnalyticEntity FetchChartMetrics(IUsageAnalyticModel analyticModel)
        {
            // Establish MariaDB Connection
            try
            {
                if (_mySqlConnection != null && _mySqlConnection.State == System.Data.ConnectionState.Open)
                    _mySqlConnection.Close();
                _mySqlConnection = MariaDBConnectionBase.EstablishConnection(_connectionString);
                _mySqlConnection.Open();
            }
            catch (Exception)
            {
                throw new Exception("Failed to Process Request");
            }

            string commandText = "SELECT accessDate, loginTotal FROM AdmissionAnalytics WHERE accessDate >= NOW() - INTERVAL 3 MONTH AND accessDate < NOW();";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Transaction = _mySqlConnection.BeginTransaction();
                try
                {
                    IUsageAnalyticEntity result = RefineData(command.ExecuteReader(), analyticModel);
                    return result;
                    //foreach (var item in (List<IAxisDetailsEntity>)result.metricList)
                    //{
                    //Console.WriteLine(item.xData);
                    //Console.WriteLine(item.yData);
                    //}
                }
                catch (Exception)
                {
                    throw new Exception("Failed to Process Request");
                    //throw e;
                }
            }
        }
    }
}