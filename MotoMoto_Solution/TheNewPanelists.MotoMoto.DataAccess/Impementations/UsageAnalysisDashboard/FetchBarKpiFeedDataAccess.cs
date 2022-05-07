using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class FetchBarKpiFeedDataAccess : MariaDBConnectionBase, IFetchKpiDataAccess
    {
        private MySqlConnection? _mySqlConnection;
        private string _connectionString;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FetchBarKpiFeedDataAccess()
        {
            // Use App.config here
            // Development
            _connectionString = "server=localhost;user=dev_moto;database=dev_UAD;port=3306;password=motomoto;";
            // Production
            //_connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";
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
                // xTitle represents the column title which will be "viewTitle" for ViewAnalytics
                // yTitle represents the column title which will be "durationAvg" or "displayTotal" for View Analytics
                // string xTitle = analyticModel.x_axis;
                string yTitle = analyticModel.y_axis;
                IEnumerable<IAxisDetailsEntity> metrics = new List<IAxisDetailsEntity>();
                try
                {
                    while (reader.Read())
                    {
                        //((List<string>)xMetrics).Add(reader.GetString("viewTitle"));
                        //((List<string>)yMetrics).Add(reader.GetInt32(yTitle).ToString());
                        string x = reader.GetString("feedName");
                        string y = reader.GetString(yTitle).ToString();
                        IAxisDetailsEntity metric = new DataStoreAxisDetails(x, y);
                        ((List<IAxisDetailsEntity>)metrics).Add(metric);
                    }
                    reader.Close();
                }
                catch (Exception)
                {
                    reader.Close();
                    throw new Exception("Failed to Add Result");
                    //throw e;
                }
                //IAxisDetailsEntity xDetails = new DataStoreXAxisDetails(xTitle, xMetrics);
                //IAxisDetailsEntity yDetails = new DataStoreYAxisDetails(yTitle, yMetrics);
                string yAxisTitle;
                if (yTitle.Equals("feedPostTotal"))
                    yAxisTitle = "Post Total";
                else
                    yAxisTitle = "#";

                IUsageAnalyticEntity analytic = new DataStoreUsageAnalyticBar("Community Feed", yAxisTitle, metrics);
                return analytic;
            }
            throw new Exception("No Rows Found");
        }

        /// <summary>
        /// Fetch the Bar Chart metrics from Feed Analytics
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

            // Use once Stored Procedures work
            // string procedure = "";
            // using (MySqlCommand command = new MySqlCommand(procedure, _mySqlConnection))

            // Downside is the procedure call is buggy with MySql, to ensure request processes use commandText
            // X Axis is the viewTitle, Y Axis is the metric either displayTotal or durationAvg
            string commandText;
            // COUNT(*) because only need to count the row itself nothing specific
            if (analyticModel.y_axis.Equals("feedPostTotal"))
                commandText = "SELECT feedName, COUNT(*) as feedPostTotal FROM Post GROUP BY feedName ORDER BY feedPostTotal DESC LIMIT 5;";
            else
                throw new Exception("Unknown Request");

            // Execute query, return the IUsageAnalyticEntity after data is refined into the object
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                //command.CommandType = System.Data.CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("@metricName", analyticModel.y_axis);
                command.Transaction = _mySqlConnection.BeginTransaction();
                try
                {
                    IUsageAnalyticEntity result = RefineData(command.ExecuteReader(), analyticModel);
                    command.Transaction.Commit();
                    _mySqlConnection.Close();
                    return result;
                    //foreach (var item in (List<IAxisDetailsEntity>)result.metricList)
                    //{
                    //Console.WriteLine(item.xData);
                    //Console.WriteLine(item.yData);
                    //}
                }
                catch (Exception)
                {
                    command.Transaction.Rollback();
                    _mySqlConnection.Close();
                    throw new Exception("Failed to Process Request");
                    //throw e;
                }
            }
        }
    }
}