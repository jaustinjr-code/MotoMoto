using MySql.Data.MySqlClient;
using TheNewPanelists.DataAccessLayer;
using System.Threading;


namespace TheNewPanelists.ServiceLayer.Logging 
{
    public class LogService : ILogService 
    { 
        private LoggingDataAccess? loggingDataAccess;
        private ArchiveService? archiveService; 
        private string? operation {get; set;}
        private bool isSuccess {get; set;}
        private Dictionary<string, string>? log {get; set;}
        private DateTime? localDate {get;}

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
                string commandSql = $@"INSERT INTO Log (logId, categoryId, levelId, timestamp, userID, DSCRIPTION
                                VALUES (NULL, '{log!["categoryname"].ToUpper()}', '{log!["levelname"].ToUpper()}', {dateTime},
                                {log!["userid"]}, '{operation} : {(isSuccess! ? "Success" : "Failure")} {log!["description"]}');";
                Console.WriteLine(commandSql);
                this.loggingDataAccess = new LoggingDataAccess(commandSql);
                if (this.loggingDataAccess.LogAccess() == false) {
                    return false;
                }  
            }
            return true;
        }

        public void SendArchivalInformation(string CSVDirectory) 
        {
            string commandSql = $"SELECT logId, categoryName, levelName, timeStamp, userID, DSCRIPTION"
                                + " FROM log" 
                                + " WHERE log.timeStamp <= DATE_ADD(CURDATE(), INTERVAL -30 DAY)"
                                + $" INTO OUTFILE '{CSVDirectory}'"
                                + " FIELDS ENCLOSED BY '\"'"
                                + " TERMINATED BY \';\'"
                                + " ESCAPED BY \'\"\'"
                                + " LINES TERMINATED BY \'\\r\\n\';";
            this.loggingDataAccess = new LoggingDataAccess(commandSql);
            this.archiveService = new ArchiveService("WRITE", this.loggingDataAccess.ExtractLogs());
            this.archiveService.SqlGenerator();
        }

        public bool IsValidRequest(Dictionary<String, String> userAcct)
        {
            bool containsOperation = userAcct.ContainsKey("operation");
            if (containsOperation) {
                return HasValidAttributes(userAcct["operation"].ToUpper(), userAcct);
            }
            return false;
        }

        public bool HasValidAttributes(string operation, Dictionary<String, String> attributes)
        {
            bool hasValidAttributes = false;
            switch (operation.ToUpper()) 
            {
                case "FIND":
                    hasValidAttributes = attributes.ContainsKey("username");
                    break;

                case "CREATE":
                    hasValidAttributes = attributes.ContainsKey("username") && attributes.ContainsKey("password")
                                            && attributes.ContainsKey("email");
                    break;
            }
            return hasValidAttributes;
        }
    }
}