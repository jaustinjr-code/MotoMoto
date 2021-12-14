using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace TheNewPanelists.DataAccessLayer
{
    class ArchivingDataAccess : IDataAccess
    {
        private string operation;
        public ArchivingDataAccess(string operation)
        {
            this.operation = operation;
        }
        public bool EstablishMariaDBConnection()
        {
            MySqlConnection mySqlConnection;
            // This is a hardcoded string, it will be different based on your naming
            string connectionString = "server=localhost;user=MotoMotoA;database=logs;port=3306;password=password;";
            // string connectionString = "server=localhost;user=tempuser;database=logs_MM_test;port=3306;";
            mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();          
                Console.WriteLine("Connection open");
                // SqlGenerator
                MySqlCommand command = new MySqlCommand(SqlGenerator(), mySqlConnection);
                MySqlDataReader reader = command.ExecuteReader();

                getAllLogs(reader);
                // Console.WriteLine(command.ExecuteNonQuery());
                Console.WriteLine("Close");
                mySqlConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                // Console.WriteLine("ERROR - Creating new user...");
                // BuildTempUser(mySqlConnection);
            }
            return false;                                
        }
        public string SqlGenerator()
        {
            return queryAllLogs();          
        }
        private string QueryAllLogs()
        {
            return "SELECT * FROM Log l;";
        }

        private string InsertArchiveInformation(List<Dictionary<string, string>> logList) 
        {
            for (int i = 0; i < logList.Count; i++) {
                for (int j = 0; k < logDic.Count; j++) {
                    Console.WriteLine(logList[i].ElementAt(j));
                }
            }
            return "";
        }

        private string BuildArchiveTable() 
        {
            DateTime localDate = DateTime.Now;
            string ld = localDate.ToString();
            return "CREATE TABLE "+ld+"categoryName VARCHAR(100) NOT NULL, levelName VARCHAR(50) NOT NULL, "+
                    "timeStamp DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP, userID INT NOT NULL, "+
                    "DSCRIPTION VARCHAR(1000) NOT NULL, CONSTRAINT Log_PK PRIMARY KEY (logId);";
        }
        private List<Dictionary<string, string>> GetAllLogs(MySqlDataReader reader)  
        {
            Dictionary<string, string> logDic;
            List<Dictionary<string,string>> logList = new List<Dictionary<string,string>>();

            while(reader.Read()) 
            {
                logDic = new Dictionary<string, string>();
                logDic.Add("logId", reader.GetString("logId"));
                logDic.Add("categoryName", reader.GetString("categoryName"));
                logDic.Add("timeStamp", reader.GetString("timeStamp"));
                logDic.Add("userID", reader.GetString("userID"));
                logDic.Add("DSCRIPTION", reader.GetString("DSCRIPTION"));
                logList.Add(logDic);
            }
            return logList;
        }
    }
}