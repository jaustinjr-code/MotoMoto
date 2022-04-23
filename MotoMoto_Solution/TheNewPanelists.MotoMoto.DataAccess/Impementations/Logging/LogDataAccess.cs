using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Data;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class LogDataAccess
    {
        private MySqlConnection? mySqlConnection { get; set; }
        private string _connectionString = "server=localhost;user=dev_moto;database=dev_log;port=3306;password=motomoto;";//write config so this only appears once
        public LogDataAccess() { }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public LogDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool ExecuteQuery(MySqlCommand command)
        {
            if (command.ExecuteNonQuery() == 1)
            {
                mySqlConnection!.Close();
                return true;
            }
            mySqlConnection!.Close();
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EstablishMariaDBConnection()
        {
            try
            {
                mySqlConnection = new MySqlConnection(_connectionString);
                mySqlConnection.Open();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logModel"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool InsertNewLogEntity(DataStoreLog dataStoreLog)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "INSERT INTO LOG (categoryName, levelName, userId, username, description) " +
                                      "VALUES ('@v2', '@v3', '@v5, @v6);";
                var parameters = new MySqlParameter[6];
                parameters[1] = new MySqlParameter("@v1", dataStoreLog!._categoryName);
                parameters[2] = new MySqlParameter("@v2", dataStoreLog!._levelName);
                parameters[4] = new MySqlParameter("@v3", dataStoreLog!._userId);
                parameters[5] = new MySqlParameter("@v4", dataStoreLog!._username);
                parameters[5] = new MySqlParameter("@v6", dataStoreLog!._description);

                command.Parameters.AddRange(parameters);
                
                return (ExecuteQuery(command));
            }
        }
        public bool RemoveUsernameFromLogFiles(LogEntryModel logEntryModel)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "UPDATE Log L SET L.username = NULL WHERE L.username = '@v1'";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", logEntryModel._username);
                return (ExecuteQuery(command));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        private void DropThirtyDayOldLogs()
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.CommandText = "DELETE LOG FROM LOG WHERE DATEDIFF(NOW(), TIMESTAMP) < 30;";
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                ExecuteQuery(command);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CSVDirectory"></param>
        /// <exception cref="NullReferenceException"></exception>
        public void SendThirtyDayOldInformationToArchive(string CSVDirectory)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.CommandText = "SELECT logId, categoryName, levelName, timeStamp, userID, DSCRIPTION"
                                    + " FROM log"
                                    + " WHERE log.timeStamp <= DATE_ADD(CURDATE(), INTERVAL -30 DAY)"
                                    + " INTO OUTFILE '@v1'"
                                    + " FIELDS ENCLOSED BY '\"'"
                                    + " TERMINATED BY \';\'"
                                    + " ESCAPED BY \'\"\'"
                                    + " LINES TERMINATED BY \'\\r\\n\';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", CSVDirectory);
                command.Parameters.AddRange(parameters);
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                ExecuteQuery(command);
            }
            DropThirtyDayOldLogs();
        }
    }
}
