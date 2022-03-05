using TheNewPanelists.ServiceLayer.UserManagement;
using TheNewPanelists.ServiceLayer.Logging;
using TheNewPanelists.DataAccessLayer;

namespace TheNewPanelists.ApplicationLayer.Authorization
{
    public class UserManagementAuthorization : IAuthorization
    {
        private string username;
        private string password;

        private string authType;

        private Dictionary<string, string> accountDict;

        public UserManagementAuthorization() {
            username = "";
            password = "";
            authType = "";
            accountDict = new Dictionary<string, string>();
            setCredentials();
            
        }

        public void setCredentials() {
            Console.WriteLine("*** AUTHORIZATION ***");
            Console.WriteLine("Enter Credentials");
            try
            {
                Console.Write("Username: ");
                this.username = Console.ReadLine() ?? "";
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
           
            try
            {
                Console.Write("Password: ");
                this.password = Console.ReadLine() ?? "";
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            

            setAccessLevel();
        }

        private string setAccessLevel() {
            while (username == "" || password == "") {
                Console.WriteLine("Credentials Must Be Set Before Determining Access Level");
                setCredentials();
            }
            Dictionary<string, string> accountInfo = new Dictionary<string, string>() {
                {"username", this.username},
                {"password", this.password}
            };
            UserManagementService userManagmementServiceObject = new UserManagementService("FIND", accountInfo);
            string queryString = userManagmementServiceObject.getQuery();
            
            UserManagementDataAccess userManagementDataObject = new UserManagementDataAccess(queryString);
            accountInfo = userManagementDataObject.GetAccountInformation();
            this.accountDict = accountInfo;

            if (accountInfo == null) {
                return "ERROR";
            }


            if (!accountInfo.ContainsKey("userId")) {
                Console.WriteLine("** INVALID USERNAME ENTERED ** ");
                return "ERROR";
            }

            if (this.password != accountInfo["password"]) {
                Console.WriteLine("** ERROR INVALID PASSWORD ** ");
                return "ERROR";
            }

            this.authType = accountInfo["typeName"];
            return accountInfo["typeName"];
        }

        public string getAuthType() {
            return this.authType;
        }

         public bool checkAuthorized(string operation) {
            bool isAuthorized = false;
            if (operation == null || this.authType == null) 
            {
                Dictionary<string, string> log = new Dictionary<string, string>() {
                    {"categoryname", "BUSINESS"},
                    {"levelname", "ERROR"},
                    {"userid", "-1"},
                    {"description", "Error completing authorization..."}
                };
                LogService logging = new LogService(operation ?? "", log, false);
                if (!logging.SqlGenerator()) 
                {
                    Console.WriteLine("** ERROR LOGGING FAILED **");
                }
                Console.WriteLine("*** Authorization has not yet been set successfully, authorization check aborted... *** ");
                return false;
            }
            string upperOperation = operation.ToUpper();
            string upperAuthType = this.authType.ToUpper();
            
            if (upperOperation == "FIND") {
                if (upperAuthType == "ADMIN") {
                    isAuthorized = true;
                }
            }

            else if (upperOperation == "CREATE") {
                if (upperAuthType == "ADMIN") {
                    isAuthorized = true;
                }
            }

            else if (upperOperation == "DROP") {
                if (upperAuthType == "ADMIN") {
                    isAuthorized = true;
                }
            }

            else if (upperOperation == "UPDATE") {
                if (upperAuthType == "ADMIN") {
                    isAuthorized = true;
                }
            }

            else if (upperOperation == "ACCOUNT RECOVERY") {
                if (upperAuthType == "ADMIN" || upperAuthType == "REGISTERED") {
                    isAuthorized = true;
                }
            }

            if (isAuthorized) 
            {
                Console.WriteLine("*** USER SUCCESSFULLY AUTHORIZED FOR THIS OPERATION ***\n");
            }
            else 
            {
                Dictionary<string, string> log = new Dictionary<string, string>() {
                    {"categoryname", "BUSINESS"},
                    {"levelname", "INFO"},
                    {"userid", this.accountDict["userId"]},
                    {"description", "User tried to complete an unauthorized operation"}
                };
                LogService logging = new LogService(operation, log, false);
                if (!logging.SqlGenerator()) 
                {
                    Console.WriteLine("** ERROR LOGGING FAILED **");
                }
                Console.WriteLine("*** USER IS NOT AUTHORIZED FOR THIS OPERATION ***\n");
            }

            return isAuthorized;
        }
    }
}