using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace TheNewPanelists.ServiceLayer.Authentication
{
    class AuthenticationService : IAuthenticationService
    {
        private string operation {get; set;}
        private UserManagementDataAccess userManagementDataAccess;
        private Dictionary<string, string> userAccount {get; set;}
        
        public AuthenticationService() {}
        
        public AuthenticationService(string operation, Dictionary<string, string> userAccount) 
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

        public string CreateOTP()
        {
            // A - Z: ASCII 65 - 90 rand.Next(65, 91)
            // a - z: ASCII 97 - 122 rand.Next(97, 123)
            // 0 - 9: ASCII 48 - 57 rand.Next(48, 58)
            Random rand = new Random();
            char[] chArr = new char[9];
            string otp = "";

            for (int i = 0; i < chArr.Length; i++)
            {
                int num =  i < 3? num = i : num = rand.Next(0, 3);

                if (num == 0) 
                {
                    chArr[i] = (char) rand.Next(65, 91);     // upper case
                }

                else if (num == 1)
                {
                    chArr[i] = (char) rand.Next(97, 123);   // lower case
                }

                else if (num == 2)
                {
                    chArr[i] = (char) rand.Next(48, 58);    // number 0 - 9
                }
            }

            Console.Write("before shuffle: ");
            foreach (char ch in chArr)
            {
                Console.Write(ch);
            }

            for (int i = 0; i < 100; i++)
            {
                int randNum1 = rand.Next(chArr.Length);
                int randNum2 = rand.Next(chArr.Length);
                char temp = chArr[randNum1];
                chArr[randNum1] = chArr[randNum2];
                chArr[randNum2] = temp;
            }
            
            foreach (char ch in chArr)
            {
                otp += ch;
            }
            Console.WriteLine("\notp: " + otp);
            return otp;
        }
    }
}