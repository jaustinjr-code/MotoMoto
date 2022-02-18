using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace TheNewPanelists.ServiceLayer.UserAuthentication
{
    class UserAuthenticationService : IUserAuthenticationService
    {
        private string operation {get; set;}
        private UserManagementDataAccess userManagementDataAccess;
        private Dictionary<string, string> userAccount {get; set;}
        
        public UserAuthenticationService() {}
        
        public UserAuthenticationService(string operation, Dictionary<string, string> userAccount) 
        {
            this.operation = operation;
            this.userAccount = userAccount;
            this.userManagementDataAccess = new UserManagementDataAccess();
        }

        public bool validateRequest()
        {
            Regex letter = new Regex(@"[a-zA-Z]");
            Regex num = new Regex(@"[0-9]");
            Regex specialChar = new Regex(@"[.,@!]");
            Regex length = new Regex(@"[a-zA-Z0-9.,@!]{8,}");

            foreach(KeyValuePair<string, string> entry in userAccount){
                if (string.IsNullOrEmpty(entry.Value))
                {
                    return false;
                }
                else if (entry.Key == "username")
                {
                    bool IsValidPattern = letter.IsMatch(entry.Value) && num.IsMatch(entry.Value) 
                        && specialChar.IsMatch(entry.Value) && length.IsMatch(entry.Value);
                    if (!IsValidPattern)
                    {
                        Console.WriteLine("failed username test");
                        return false;
                    }
                    Console.WriteLine("passed username test");
                }
                else if (entry.Key == "password")
                {

                }
                else if (entry.Key == "otp")
                {
                    bool IsValidPattern = letter.IsMatch(entry.Value) && num.IsMatch(entry.Value) 
                        && specialChar.IsMatch(entry.Value) && length.IsMatch(entry.Value);
                    if (!IsValidPattern)
                    {
                        Console.WriteLine("failed OTP test");
                        return false;
                    }
                    Console.WriteLine("passed OTP test");
                }
            }
            return true;
        }
        
        public bool SqlGenerator()
        {   
            Dictionary<string, string> informationLog = new Dictionary<string, string>();
            string query = "";
        
            query = this.FindUser();
            

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
    }
}