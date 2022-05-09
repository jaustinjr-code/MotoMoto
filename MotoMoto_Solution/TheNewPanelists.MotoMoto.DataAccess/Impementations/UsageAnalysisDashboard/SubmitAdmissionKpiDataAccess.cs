using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class SubmitAdmissionKpiDataAccess : MariaDBConnectionBase, ISubmitKpiDataAccess
    {
        private MySqlConnection? _mySqlConnection;
        private string _connectionString;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SubmitAdmissionKpiDataAccess()
        {
            // Use App.config here
            // Development
            // _connectionString = "server=localhost;user=dev_moto;database=dev_UAD;port=3306;password=motomoto;";
            // Production
            _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";
        }

        /// <summary>
        /// Fetch the Bar Chart metrics from Feed Analytics
        /// </summary>
        /// <param name="analyticModel">IUsageAnalyticModel</param>
        /// <returns>IUsageAnalyticEntity</returns>
        public async Task<bool> SubmitKpiMetricsAsync(IUsageMetricModel metricModel)
        {
            // Establish MariaDB Connection
            try
            {
                if (_mySqlConnection != null && _mySqlConnection.State == System.Data.ConnectionState.Open)
                    await _mySqlConnection.CloseAsync();
                _mySqlConnection = MariaDBConnectionBase.EstablishConnection(_connectionString);
                await _mySqlConnection.OpenAsync();
            }
            catch (Exception)
            {
                throw new Exception("Failed to Process Request");
            }

            string commandText;
            if (metricModel.type!.Equals("login"))
                commandText = "INSERT AdmissionAnalytics (accessDate, loginTotal) VALUES (CURDATE(), @metric) ON DUPLICATE KEY UPDATE loginTotal = loginTotal + @metric;";
            else if (metricModel.type!.Equals("registration"))
                commandText = "INSERT AdmissionAnalytics (accessDate, registrationTotal) VALUES (CURDATE(), @metric) ON DUPLICATE KEY UPDATE registrationTotal = registrationTotal + @metric;";
            else throw new Exception("Invalid Admission Type");
            // Execute query, return the IUsageAnalyticEntity after data is refined into the object
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Parameters.AddWithValue("@metric", metricModel.metric);
                command.Transaction = await _mySqlConnection.BeginTransactionAsync();
                try
                {
                    int result = await command.ExecuteNonQueryAsync();
                    await command.Transaction.CommitAsync();
                    await _mySqlConnection.CloseAsync();

                    if (result == (int)ISubmitKpiDataAccess.AnalyticResult.N0_ANALYTIC)
                        return false;
                    else if (result == (int)ISubmitKpiDataAccess.AnalyticResult.NEW_ANALYTIC ||
                             result == (int)ISubmitKpiDataAccess.AnalyticResult.UPDATED_ANALYTIC)
                        return true;
                    else if (result == (int)ISubmitKpiDataAccess.AnalyticResult.FAILED_UPDATE)
                        throw new Exception("Failed to Update Analytic");
                    throw new Exception("Failed to Process Request");
                }
                catch (Exception)
                {
                    await command.Transaction.RollbackAsync();
                    await _mySqlConnection.CloseAsync();
                    throw new Exception("Failed to Process Request");
                    //throw e;
                }
            }
        }
    }
}