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
        private DateTime localDate {get;}

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
            Dictionary<string, string> informationLog = new Dictionary<string, string>();
            if (this.operation == "CREATE") 
            {
                DateTime dateTime = DateTime.Now;
                string commandSql = $@"INSERT INTO Log (logId, categoryId, levelId, timestamp, userID, DSCRIPTION)
                                VALUES (NULL, '{log["categoryname"].ToUpper()}', '{log["levelname"].ToUpper()}', {dateTime},
                                {log["userid"]}, '{operation} : {(isSuccess ? "Success" : "Failure")} {log["description"]}');";
                Console.WriteLine(commandSql);
                this.loggingDataAccess = new LoggingDataAccess(commandSql);
                if (this.loggingDataAccess.LogAccess() == false) {
                    informationLog.Add("categoryname", "DATA STORE");
                    informationLog.Add("userid", "TEMP USER"); //temp user created for userid
                    informationLog.Add("levelname", "ERROR");
                    informationLog.Add("description","Account Selection ERROR, Information in CRUD Operation Queries Not Executed!!");
                    //ILogService logFailure = new LogService("CREATE", informationLog, false);
                    //logFailure.SqlGenerator();

                    return false;
                }  
            }
            informationLog.Add("categoryname", "DATA STORE");
            informationLog.Add("userid", "TEMP USER"); //temp user created for userid
            informationLog.Add("levelname", "INFO");
            informationLog.Add("description","LOG CREATION SUCCESS, Information Successfully Logged!!");
            //ILogService logSuccess = new LogService("CREATE", informationLog, true);
            //logSuccess.SqlGenerator();
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