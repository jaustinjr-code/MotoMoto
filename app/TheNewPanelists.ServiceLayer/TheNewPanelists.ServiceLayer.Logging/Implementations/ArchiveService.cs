using MySql.Data.MySqlClient;
using System;
using System.Globalization;
using TheNewPanelists.DataAccessLayer;

namespace TheNewPanelists.ServiceLayer.Logging 
{
    public class ArchiveService : IArchiveService 
    { 
        private string? operation {get; set;}
        private List<Dictionary<string, string>>? log {get; set;}
        private ArchivingDataAccess? archivingDataAccess;
        private DateTime localDate{get;}

        public ArchiveService() {}

        public ArchiveService(string operation, List<Dictionary<string, string>> log) {
            this.operation = operation;
            this.log = log;
            this.localDate = DateTime.Now;
            this.archivingDataAccess = new ArchivingDataAccess();
        }

        public bool SqlGenerator()
        {   
            Dictionary<string, string> informationLog = new Dictionary<string, string>();
            List<string> queries = InsertArchiveInformation(); 
            for (int i = 0; i < queries.Count; i++) {
                archivingDataAccess = new ArchivingDataAccess(queries[i]);
                if (archivingDataAccess.RunArchiveStorage() == false) 
                {
                    return false;
                }
            }
            return true;
        }
        private string BuildArchiveTable(DateTime localDate) 
        {
            string createTable;
            createTable = "CREATE TABLE '" + localDate
                        + "' ('logId' int(11) NOT NULL,"
                        + "'categoryName' varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,"
                        + "'levelName' varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,"
                        + "'timeStamp' datetime NOT NULL,"
                        + "'userID int(11) NOT NULL,"
                        + "'DSCRIPTION' varchar(1000) COLLATE utf8bm4_unicode_ci NOT NULL,"
                        + "`userID` int(11) NOT NULL)"
                        + " ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;";
            return createTable;
        }

        private string LoadCSVData(string Directory, DateTime localDate)
        {
            return "LOAD DATA INFILE '" + Directory
                    + "' INTO TABLE '" + localDate + "' "
                    + " FIELDS ENCLOSED BY '\"'"
                    + " TERMINATED BY \';\'"
                    + " ESCAPED BY \'\"\'"
                    + " LINES TERMINATED BY \'\\r\\n\';";
        }

        private List<string> InsertArchiveInformation() 
        {   
            List<string> storeArchive = new List<String>();

            string localdateDay = this.localDate.Date.ToString("d");

            localdateDay = localdateDay.Replace("/","_");

            for (int i = 0; i < log!.Count; i++) {
                string query = @"INSERT INTO "+localdateDay+" VALUES ("+log[i]["logId"]+", '"+
                log[i]["levelName"]+"', '"+log[i]["categoryName"]+"', '"+log[i]["timeStamp"]+"', "+log[i]["userID"]+", '"+
                log[i]["DSCRIPTION"]+"');";
                Console.WriteLine(query);
                storeArchive.Add(query);
            }
            
            return storeArchive;
        }
    }
}