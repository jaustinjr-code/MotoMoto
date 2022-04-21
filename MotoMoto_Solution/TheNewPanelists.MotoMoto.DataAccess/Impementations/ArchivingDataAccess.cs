using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class ArchivingDataAccess
    {
        private MySqlConnection? mySqlConnection = null;
        private readonly string _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;";

        public ArchivingDataAccess() { }
        public ArchivingDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        private bool ExecuteQuery(MySqlCommand command)
        {
            switch (command.ExecuteNonQuery())
            {
                case 1:
                    mySqlConnection!.Close();
                    return true;
                default:
                    mySqlConnection!.Close();
                    return false;
            }
        }
        private bool EstablishMariaDBConnection()
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
        public bool BuildArchiveTable(DateTime localDate)
        {
            if (!EstablishMariaDBConnection())
                throw new NullReferenceException();
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandType = CommandType.Text;

                command.CommandText = $"CREATE TABLE '@v1"
                                    + "' ('logId' int(11) NOT NULL,"
                                    + "'categoryName' varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,"
                                    + "'levelName' varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,"
                                    + "'timeStamp' datetime NOT NULL,"
                                    + "'userID int(11) NOT NULL,"
                                    + "'DSCRIPTION' varchar(1000) COLLATE utf8bm4_unicode_ci NOT NULL,"
                                    + "`userID` int(11) NOT NULL)"
                                    + " ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", localDate);
                command.Parameters.AddRange(parameters);
                return ExecuteQuery(command);
            }
        }

        public bool LoadCSVDataIntoThirtyDayOldArchiveTable(string Directory, DateTime localDate)
        {
            if (!EstablishMariaDBConnection())
                throw new NullReferenceException();
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandType = CommandType.Text;
                command.CommandText = "LOAD DATA INFILE '@v1"
                                    + "' INTO TABLE '@v2' "
                                    + " FIELDS ENCLOSED BY '\"'"
                                    + " TERMINATED BY \';\'"
                                    + " ESCAPED BY \'\"\'"
                                    + " LINES TERMINATED BY \'\\r\\n\';";
                var parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@v1", localDate);
                parameters[1] = new MySqlParameter("@v2", Directory);
                command.Parameters.AddRange(parameters);
                return ExecuteQuery(command);
            }
        }
    }
}
