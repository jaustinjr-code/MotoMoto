using MySql.Data.MySqlClient;

namespace TheNewPanelists.ServiceLayer.Logging 
{
    class LogService : ILogService 
    { 
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
        }
        public string SqlGenerator()
        {
            string commandSql = "";

            if (this.operation == "CREATE") 
            {
                commandSql = $@"INSERT INTO Log (logId, categoryName, levelName, userID, DSCRIPTION)
                                VALUES (NULL, '{log["categoryname"].ToUpper()}', '{log["levelname"].ToUpper()}',
                                {log["userid"]}, '{operation} : {(isSuccess ? "Success" : "Failure")}')";
                Console.WriteLine(commandSql);
                return commandSql;
            } 
            else if (this.operation == "ARCHIVE")
            {
                commandSql = $"SELECT * FROM Log WHERE DATEDIFF(NOW(), timeStamp) > 30";
                return commandSql;
            }
            return "";
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
