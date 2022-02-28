using MySql.Data.MySqlClient;
using TheNewPanelists.DataAccessLayer;

namespace TheNewPanelists.ServiceLayer.Logging 
{
    class ArchiveService : IArchiveService 
    { 
        private string operation {get; set;}
        private List<Dictionary<string, string>> log {get; set;}
        private ArchivingDataAccess archivingDataAccess;
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

        public string BuildArchiveTable() 
        { 
            string createTable;
            createTable = "SELECT L FROM LOG L WHERE DATE(L.timeStamp) "
        }
        private List<string> InsertArchiveInformation() 
        {   
            List<string> storeArchive = new List<String>();

            string localdateDay = this.localDate.Date.ToString("d");

            localdateDay = localdateDay.Replace("/","_");

            for (int i = 0; i < log.Count; i++) {
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