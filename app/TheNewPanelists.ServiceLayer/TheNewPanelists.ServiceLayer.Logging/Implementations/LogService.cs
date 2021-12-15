using MySql.Data.MySqlClient;
using TheNewPanelists.DataAccessLayer;

namespace TheNewPanelists.ServiceLayer.Logging 
{
    class LogService : ILogService 
    { 
        private LoggingDataAccess loggingDataAccess;
        private ArchiveService archiveService; 
        private string operation {get; set;}
        private bool isSuccess {get; set;}
        private Dictionary<string, string> log {get; set;}
        private DateTime localDate{get;}
        public LogService() {}
        public LogService(string operation, Dictionary<string, string> log, bool isSuccess) 
        {
            this.operation = operation;
            this.log = log;
            this.isSuccess = isSuccess;
            this.loggingDataAccess = new LoggingDataAccess();
            this.archiveService = new ArchiveService();
        }
        public bool SqlGenerator()
        {
            if (this.operation == "CREATE") 
            {
                string commandSql = $@"INSERT INTO Log (logId, categoryName, levelName, userID, DSCRIPTION)
                                VALUES (NULL, '{log["categoryname"].ToUpper()}', '{log["levelname"].ToUpper()}',
                                {log["userid"]}, '{operation} : {(isSuccess ? "Success" : "Failure")}');";
                Console.WriteLine(commandSql);
                this.loggingDataAccess = new LoggingDataAccess(commandSql);
                if (this.loggingDataAccess.LogAccess() == false) return false;  
            } 
            return true;
        }
        public void SendArchivalInformation() 
        {
            string commandSql = $"SELECT * FROM Log WHERE DATEDIFF(NOW(), timeStamp) > 30";
            this.loggingDataAccess = new LoggingDataAccess(commandSql);
            this.archiveService = new ArchiveService("WRITE", this.loggingDataAccess.ExtractLogs());
            this.archiveService.SqlGenerator();
        }
    }
}


//MySqlCommand mySqlCommand = new MySqlCommand()
            //string commandSql = "INSERT INTO Category VALUES (NULL,\"Business\")";
            //string commandSql = "SELECT * FROM Category"; 

            //string commandSql = $"INSERT INTO Log VALUES (NULL, {categoryName}, {levelName}, NULL, {userID}, \"{operation} : {(isSuccess ? "Success" : "Failure")}\")";


// string dateTime = DateTime.Now.ToString("G");
            // string commandSql = $"INSERT INTO Log VALUES (NULL, '{log["categoryname"].ToUpper()}', '{log["levelname"].ToUpper()}', '{dateTime}', {log["userid"]}, '{operation} : {(isSuccess ? "Success" : "Failure")}')";
            // string commandSql = "INSERT INTO Log (logId, categoryName, levelName, userID, DSCRIPTION) VALUES (NULL, '" +
            //                     log["categoryname"].ToUpper() + "', '" + log["levelname"].ToUpper() + "', " + log["userid"] + ", '" + operation + " : " +
            //                     (isSuccess ? "Success" : "Failure") + "')";
