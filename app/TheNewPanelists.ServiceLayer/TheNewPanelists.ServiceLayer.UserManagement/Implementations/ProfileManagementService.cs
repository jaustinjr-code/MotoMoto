using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
//using TheNewPanelists.BusinessLayer.UserManagement;

namespace TheNewPanelists.ServiceLayer.UserManagement 
{
    class ProfileManagementService : IUserManagementService 
    {
        private string operation {get; set;}
        private UserManagementDataAccess profileManagementDataAccess;
        private UserManagementManager profileManagementManager;
        private Dictionary<string, string> userProfile {get; set;}
        public ProfileManagementService() {}
        
        public ProfileManagementService(string operation, Dictionary<string, string> userProfile) 
        {
            this.operation = operation;
            this.userProfile = userProfile;
            this.profileManagementDataAccess = new UserManagementDataAccess();
            this.profileManagementManager = new UserManagementManager();
        }
        
        public bool SqlGenerator()
        {   
            string query = "";
            
            if (this.operation == "FIND")
            {
                //query = this.FindProfile();
                Console.WriteLine("Find Operation");
            }
            else if (this.operation == "CREATE")
            {
                query = this.CreateProfile();
            }
            else if (this.operation == "DROP")
            {
                query = this.DropProfile();
            }
            else if (this.operation == "UPDATE")
            {
                //query = this.UpdateProfileUN();
                Console.WriteLine("UPDATE OP");
            } 
            this.profileManagementDataAccess = new UserManagementDataAccess(query);
            if (this.profileManagementDataAccess.SelectAccount() == false) 
            {
                return false;
            }
            return true;
        }
        private string DropProfile() 
        {
            return "DELETE p FROM PROFILE p WHERE p.username = '" + this.userProfile["username"] + "';";
        }

        private string FindProfile()
        {
            return "SELECT p FROM PROFILE p WHERE p.username = '" + this.userProfile["username"] + "';";
        }

        private string CreateProfile()
        {
            // return "INSERT INTO USER (username, password, email) VALUES ('"
            //         + this.userAccount["username"] + "', '" + this.userAccount["password"] + "', '"
            //         + this.userAccount["email"] + "');";
            return "INSERT INTO PROFILE (typeId, userId, username, status, eventAccount) VALUES (2, '"
                    + this.userProfile["userid"] + "', '" + this.userProfile["username"] + "', '"
                    + "true', 'false'";   
        }

        private string UpdateProfileOP() 
        {
            String query = "";
            for (int i = 0; i < this.userProfile.Count; i++) {
                if (this.userProfile.ContainsKey("eventaccount"))
                {
                    query = query + " u.email = '" + this.userProfile["eventaccount"]+"'";
                    if(i + 1 < this.userProfile.Count-1) 
                    {
                        query = query + ", ";
                        this.userProfile.Remove("eventaccount");
                        continue;
                    } 
                    else this.userProfile.Remove("eventaccount");       
                }
                if (this.userProfile.ContainsKey("username"))
                {
                    query = query + " u.username = '" + this.userProfile["username"] + "'";
                    if (i + 1 < this.userProfile.Count-1) 
                    {
                        query = query + ", ";
                        this.userProfile.Remove("username");
                        continue;
                    }
                    else this.userProfile.Remove("username");
                }
            }
            return "";    
        }
    private string UpdateStatus()
        {
            return "UPDATE USER u SET u.status = '" + this.userProfile["newstatus"] +
                    "' WHERE u.username= '" + this.userProfile["status"]+"';";
        }

        public bool IsValidRequest()
        {
            bool containsOperation = this.operation.Contains("FIND") ||  this.operation.Contains("CREATE")
                                     || this.operation.Contains("DROP") || this.operation.Contains("UPDATE");
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
                    query = this.FindProfile();
                    break;

                case "CREATE":
                    query = this.CreateProfile();
                    break;
                
                case "DROP":
                    query = this.DropProfile();
                    break;

                case "UPDATE":
                    query = this.UpdateProfileOP();
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
                    hasValidAttributes = query.Contains("SELECT p.username FROM Profile p WHERE p.username =");
                    break;
                case "CREATE":
                    hasValidAttributes = query.Contains("INSERT INTO PROFILE (username, password, email)");
                    break;
            
                case "DROP":
                    hasValidAttributes = query.Contains("DELETE p FROM PROFILE p WHERE p.username = ");
                    break;
                case "UPDATE":
                    hasValidAttributes = (query.Contains("UPDATE PROFILE p SET") && (query.Contains("p.username")
                                        || query.Contains("password") || query.Contains("p.email")));
                    break;

            }
            return hasValidAttributes;
        }    
    }
}