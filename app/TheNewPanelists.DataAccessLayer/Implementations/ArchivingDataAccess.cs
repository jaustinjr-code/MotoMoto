using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TheNewPanelists.DataAccessLayer
{
    class ArchivingDataAccess : IDataAccess
    {
        private string operation {get; set;}
        private DateTime localDate{get;}
        private List<Dictionary<string, string>> logList {get; set;}
        public ArchivingDataAccess(string operation, List<Dictionary<string, string>> logList)
        {
            this.operation = operation;
            this.logList = logList;
            this.localDate = DateTime.Now;
        }
        // public async bool ArchiveRequest() 
        // {
        //     if (localDate.Day == 1){
                
        //     }
        // }
        public bool ExtractLogs() 
        {
            try 
            {
                while (logList.Count != 0){
                    this.SqlGenerator();
                    EstablishMariaDBConnection();
                }
            }
            catch (Exception e) {
                Console.WriteLine("Error Message: "+e.Message);
                return false;
            }
            return true;
        }
        public bool EstablishMariaDBConnection()
        {
            // This is a hardcoded string, it will be different based on your naming
            string connectionString = "server=localhost;user=MotoMotoA;database=mm_archives;port=3306;password=password;";
            // string connectionString = "server=localhost;user=tempuser;database=logs_MM_test;port=3306;";
            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);
            try
            {
                mySqlConnection.Open();          
                Console.WriteLine("Connection open");
                // SqlGenerator
                string sql = SqlGenerator();
                Console.WriteLine(sql);
                MySqlCommand command = new MySqlCommand(sql, mySqlConnection);
                command.ExecuteNonQuery();
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
            if (this.operation.Equals("BUILD")) 
            {
                this.operation = "INSERT";
                return BuildArchiveTable();
            } 
            else if (this.operation.Equals("INSERT"))
            {
                return InsertArchiveInformation();
            }
            return ""; 
        }
        private string InsertArchiveInformation() 
        {
            string ld = this.localDate.Date.ToString("d");
            ld = ld.Replace("/","_");
            string query = "";
            for (int i = 0; i < logList.Count; i++) {
                for (int j = 0; j < logList[i].Count; j++) {
                    query = "INSERT INTO "+ld+" VALUES ("+logList[i]["logId"]+", "+
                            "'"+logList[i]["categoryName"]+"', '"+logList[i]["levelName"]+"', '"+
                            logList[i]["timeStamp"]+"', "+logList[i]["userID"]+", '"+logList[i]["DSCRIPTION"]+"');";
                    logList.RemoveAt(i);
                    break;
                }
            }
            return query;
        }
        private string BuildArchiveTable() 
        {
            string ld = this.localDate.Date.ToString("d");
            ld = ld.Replace("/","_");

            return "CREATE TABLE "+ld+" (logId INT NOT NULL, categoryName VARCHAR(100) NOT NULL, levelName VARCHAR(50) NOT NULL, "+
                    "timeStamp DATETIME NOT NULL, userID INT NOT NULL, DSCRIPTION VARCHAR(1000) NOT NULL, "+
                    "CONSTRAINT Log_PK PRIMARY KEY (logId)) ENGINE=InnoDB;";
        }


    }
}