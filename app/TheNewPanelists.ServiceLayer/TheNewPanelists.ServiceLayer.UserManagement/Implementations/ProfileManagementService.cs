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
            return "DELETE u FROM USER u WHERE u.username = '" + this.userProfile["username"] + "';";
        }

        private string CreateProfile()
        {
            return "";
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
                case "DROP":
                    hasValidAttributes = attributes.ContainsKey("username");
                    break;
                case "UPDATE":
                    hasValidAttributes = (attributes.ContainsKey("newusername") || attributes.ContainsKey("newpassword")
                                            || attributes.ContainsKey("newemail")) && attributes.ContainsKey("username");
                    break;
            }
            return hasValidAttributes;
        }

    }
}