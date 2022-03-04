using TheNewPanelists.ServiceLayer.UserManagement;
using TheNewPanelists.DataAccessLayer;

namespace TheNewPanelists.ApplicationLayer.Authorization
{
    public class UserManagementAuthorization : IAuthorization
    {
        private string username;
        private string password;

        private string authType;

        public UserManagementAuthorization() {
            username = "";
            password = "";
            authType = null;
            setCredentials();
            
        }

        public void setCredentials() {
            Console.WriteLine("Enter Credentials");
            
            Console.Write("Username: ");
            this.username = Console.ReadLine();
            
            Console.Write("Password: ");
            this.password = Console.ReadLine();

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
                Console.WriteLine("*** USER IS NOT AUTHORIZED FOR THIS OPERATION ***\n");
            }

            return isAuthorized;
        }
    }
}