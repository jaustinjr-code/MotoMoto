using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
//using TheNewPanelists.BusinessLayer.UserManagement;

namespace TheNewPanelists.ServiceLayer.UserManagement 
{
    class UserManagementService : IUserManagementService 
    {
        private string operation {get; set;}
        private UserManagementDataAccess userManagementDataAccess;
        // private UserManagementManager userManagementManager;
        private Dictionary<string, string> userAccount {get; set;}
        
        public UserManagementService() {}
        
        public UserManagementService(string operation, Dictionary<string, string> userAccount) 
        {
            this.operation = operation;
            this.userAccount = userAccount;
            this.userManagementDataAccess = new UserManagementDataAccess();
            // this.userManagementManager = new UserManagementManager();
        }
        
        public bool SqlGenerator()
        {   
            Dictionary<string, string> informationLog = new Dictionary<string, string>();
            string query = "";
            if (this.operation == "FIND")
            {
                query = this.FindUser();
            }
            else if (this.operation == "CREATE")
            {
                query = this.CreateUser();
            }
            else if (this.operation == "DROP")
            {
                query = this.DropUser();
            }
            else if (this.operation == "UPDATE")
            {
                query = this.UpdateOptions();
            } 
            this.userManagementDataAccess = new UserManagementDataAccess(query);
            if (this.userManagementDataAccess.SelectAccount() == false) 
            {
                informationLog.Add("categoryname", "DATA STORE");
                informationLog.Add("levelname", "ERROR");
                informationLog.Add("description","Account Selection ERROR, Information in CRUD Operation Queries Not Executed!!");
                ILogService loggingError = new LogService("CREATE", informationLog, false);
                loggingError.SqlGenerator();
                return false;
            }
            informationLog.Add("categoryname", "DATA STORE");
            informationLog.Add("levelname", "INFO");
            informationLog.Add("description","Account Selection COMPLETION, Information in CRUD Operation Queries Executed.");
            ILogService loggingSuccess = new LogService("CREATE", informationLog, true);
            loggingSuccess.SqlGenerator();
            return true;
        }

        private string FindUser()
        {
            return "SELECT u.usernameFROM User u WHERE u.username =" + this.userAccount["username"] + ";";
        }

        private string CreateUser()
        {
            return "INSERT INTO USER (typeId, username, password, email, able, eventAccount) VALUES (2, '" 
                    + this.userAccount["username"] + "', '" + this.userAccount["password"] + "', '" 
                    + this.userAccount["email"] + "', false, false);";
        }

        private string DropUser()
        {
            return "DELETE u FROM USER u WHERE u.username = '" + this.userAccount["username"] + "';";
        }

        private string UpdateOptions()
        {   
            string query = "UPDATE USER u SET ";
            for (int i = 0; i < this.userAccount.Count; i++) {
                if (this.userAccount.ContainsKey("newusername"))
                {
                    query = query + " u.username = '" + this.userAccount["newusername"]+"'";
                    if(i + 1 < this.userAccount.Count-1) 
                    {
                        query = query + ", ";
                        this.userAccount.Remove("newusername");
                        continue;
                    }
                    else this.userAccount.Remove("newusername");
                } 
                if (this.userAccount.ContainsKey("newpassword"))
                {
                    query = query + " u.password = '" + this.userAccount["newpassword"]+"'";
                    if(i + 1 < this.userAccount.Count-1) 
                    {
                        query = query + ", ";
                        this.userAccount.Remove("newpassword");
                        continue;
                    }
                    else this.userAccount.Remove("newpassword");
                    
                }
                if (this.userAccount.ContainsKey("newemail"))
                {
                    query = query + " u.email = '" + this.userAccount["newemail"]+"'";
                    if(i + 1 < this.userAccount.Count-1) 
                    {
                        query = query + ", ";
                        this.userAccount.Remove("newemail");
                        continue;
                    } 
                    else this.userAccount.Remove("newemail");       
                }
                if (this.userAccount.ContainsKey("newstatus"))
                {
                    query = query + " u.email = '" + this.userAccount["newstatus"]+"'";
                    if(i + 1 < this.userAccount.Count-1) 
                    {
                        query = query + ", ";
                        this.userAccount.Remove("newstatus");
                        continue;
                    } 
                    else this.userAccount.Remove("newstatus");       
                }
                if (this.userAccount.ContainsKey("eventaccount"))
                {
                    query = query + " u.email = '" + this.userAccount["eventaccount"]+"'";
                    if(i + 1 < this.userAccount.Count-1) 
                    {
                        query = query + ", ";
                        this.userAccount.Remove("eventaccount");
                        continue;
                    } 
                    else this.userAccount.Remove("eventaccount");       
                }
            }
            string queryWhere = $" WHERE u.username= '{this.userAccount["username"]}';";
            query = query + queryWhere;
            return query;
        }
    }
}