using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
//using TheNewPanelists.BusinessLayer.UserManagement;

namespace TheNewPanelists.ServiceLayer.UserManagement 
{
    class UserManagementService : IUserManagementService 
    {
        private string operation {get; set;}
        private UserManagementDataAccess userManagementDataAccess;
        private UserManagementManager userManagementManager;
        private Dictionary<string, string> userAccount {get; set;}
        
        public UserManagementService() {}
        
        public UserManagementService(string operation, Dictionary<string, string> userAccount) 
        {
            this.operation = operation;
            this.userAccount = userAccount;
            this.userManagementDataAccess = new UserManagementDataAccess();
            this.userManagementManager = new UserManagementManager();
        }
        
        public bool SqlGenerator()
        {   
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
            else if (this.operation == "ACCOUNT RECOVERY")
            {
                query = this.AccountRecovery();
                Console.WriteLine(query);
            }
            this.userManagementDataAccess = new UserManagementDataAccess(query);
            if (this.userManagementDataAccess.SelectAccount() == false) 
            {
                return false;
            }
            return true;
        }
        
        private string FindUser()
        {
            return "SELECT u.userId FROM User u WHERE u.username =" + this.userAccount["username"] + ";";
        }

        //Danny work on this query to ensure user insertion
        private string CreateUser()
        {
            //return "INSERT INTO USER (typeID, username, password, email, able, eventAccount) VALUES (2, '" 
            //        + this.userAccount["username"] + "', '" + this.userAccount["password"] + "', '" 
            //        + this.userAccount["email"] + "', false, false);";
            return "INSERT INTO USER (username, password, email) VALUES ('"
                    + this.userAccount["username"] + "', '" + this.userAccount["password"] + "', '"
                    + this.userAccount["email"] + "');";
        }

        private string DropUser()
        {
            return "DELETE u FROM USER u WHERE u.username = '" + this.userAccount["username"] + "' AND u.password = '"
                     + this.userAccount["password"] + "';";
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
            }
            string queryWhere = $" WHERE u.username= '{this.userAccount["username"]}';";
            query = query + queryWhere;
            return query;
        }

        private string UpdateStatus()
        {
            return "UPDATE USER u SET u.status = '" + this.userAccount["newstatus"] +
                    "' WHERE u.username= '" + this.userAccount["status"]+"';";
        }

        private string AccountRecovery()
        {
            if (this.userAccount.ContainsKey("username"))
            {
                return "SELECT u.email FROM User u WHERE u.username = '" + this.userAccount["username"] + "';";
            }
            else if (this.userAccount.ContainsKey("email"))
            {
                return "SELECT u.username FROM User u WHERE u.email = '" + this.userAccount["email"] + "';";
            }
            return String.Empty;
        }

        public bool IsValidRequest()
        {
            bool containsOperation = this.operation.Contains("FIND") ||  this.operation.Contains("CREATE")
                                     || this.operation.Contains("DROP") || this.operation.Contains("UPDATE") 
                                     || this.operation.Contains("ACCOUNT RECOVERY");
            if (containsOperation) {
                return HasValidAttributes();
            }
            return false;
        }

        public string getQuery()
        {
            string query = "";
            switch (this.operation) 
            {
                case "FIND":
                    query = this.FindUser();
                    break;

                case "CREATE":
                    query = this.CreateUser();
                    break;
                
                case "DROP":
                    query = this.DropUser();
                    break;

                case "UPDATE":
                    query = this.UpdateOptions();
                    break;
                case "ACCOUNT RECOVERY":
                    query = this.AccountRecovery();
                    break;
            }
            return query;
        }
        public bool HasValidAttributes()
        {
            bool hasValidAttributes = false;
            string query = this.getQuery();

            switch (this.operation) 
            {
                case "FIND":
                    hasValidAttributes = query.Contains("SELECT u.username FROM User u WHERE u.username =");
                    break;
                case "CREATE":
                    hasValidAttributes = query.Contains("INSERT INTO USER (username, password, email)");
                    break;
            
                case "DROP":
                    hasValidAttributes = query.Contains("DELETE u FROM USER u WHERE u.username = ") 
                                        && query.Contains("AND u.password =");
                    break;
                case "UPDATE":
                    hasValidAttributes = (query.Contains("UPDATE USER u SET") && (query.Contains("u.username")
                                        || query.Contains("password") || query.Contains("email")));
                    break;
                case "ACCOUNT RECOVERY":
                    hasValidAttributes = query.Contains("SELECT u.email FROM User u WHERE u.username = ") || query.Contains("SELECT u.username FROM User u WHERE u.email = ");
                    break;
            }
            return hasValidAttributes;
        }
    }
}