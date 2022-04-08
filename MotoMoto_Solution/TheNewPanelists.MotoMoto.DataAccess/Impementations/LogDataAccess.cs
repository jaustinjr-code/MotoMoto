using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class LogDataAccess
    {
        private MySqlConnection? mySqlConnection { get; set; }
        private string _connectionString = "server=localhost;user=dev_moto;database=dev_log;port=3306;password=motomoto;";//write config so this only appears once
        public LogDataAccess() { }
        
        public LogDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
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
        public bool InsertNewLogEntity(LogModel logModel)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.CommandText = $"INSERT INTO LOG (logId, categoryName, levelName, timeStamp, userId, DSCRIPTION) " +
                                      $"VALUES (@v1, @v2, @v3, @v4, @v5, @v6);";
                var parameters = new MySqlParameter[6];
                parameters[0] = new MySqlParameter("@v1", logModel!.LogId);
                parameters[1] = new MySqlParameter("@v2", logModel!.CategoryName);
                parameters[2] = new MySqlParameter("@v3", logModel!.LevelName);
                parameters[3] = new MySqlParameter("@v4", logModel!._dateTime);
                parameters[4] = new MySqlParameter("@v5", logModel!.UserID);
                parameters[5] = new MySqlParameter("@v6", logModel!.Description);

                command.Parameters.AddRange(parameters);
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                return (ExecuteQuery(command));
            }
        }

        public bool DropThirtyDayOldLogs()
        {
            throw new NotImplementedException();
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
        }
    }
}
