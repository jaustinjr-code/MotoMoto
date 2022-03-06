using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace TheNewPanelists.ServiceLayer.EventAccountVerificationa 
{
    class EvntAccntVerifService : IEvntAccntService
    {
        
        private string operation {get; set;}
        private UserManagementDataAccess evntAccntVerifDataAccess;
        private UserManagementManager EvntAcctVerifManager;
        private Dictionary<string, string> userProfile {get; set;}
        
        public EvntAccntVerifService() {}
        public EvntAccntVerifService(string operation, Dictionary<string, string>  userProfile) {
            this.operation = operation;
            this.userProfile = userProfile;
            this.evntAccntVerifDataAccess = new UserManagementDataAccess;
            this.evntAccntVerifManager = new UserManagementManager;
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
            }
            this.userManagementDataAccess = new UserManagementDataAccess(query);
            if (this.userManagementDataAccess.SelectAccount() == false) 
            {
                return false;
            }
            return true;
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

                // case "CREATE":
                //     query = this.CreateUser();
                //     break;
                
                // case "DROP":
                //     query = this.DropUser();
                //     break;

                // case "UPDATE":
                //     query = this.UpdateOptions();
                //     break;
                // case "ACCOUNT RECOVERY":
                //     query = this.AccountRecovery();
                //     break;
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
                // case "CREATE":
                //     hasValidAttributes = query.Contains("INSERT INTO USER (username, password, email)");
                //     break;
                // case "DROP":
                //     hasValidAttributes = query.Contains("DELETE u FROM USER u WHERE u.username = ") 
                //                         && query.Contains("AND u.password =");
                //     break;
                // case "UPDATE":
                //     hasValidAttributes = (query.Contains("UPDATE USER u SET") && (query.Contains("u.username")
                //                         || query.Contains("password") || query.Contains("email")));
                //     break;
                // case "ACCOUNT RECOVERY":
                //     //hasValidAttributes = query.Contains();
                //     break;
            }
            return hasValidAttributes;
        }

    }
}