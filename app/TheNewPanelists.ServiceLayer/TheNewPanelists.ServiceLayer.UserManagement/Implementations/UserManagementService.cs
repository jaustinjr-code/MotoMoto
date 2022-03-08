using System.Net;
using System.Net.Mail;
using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using System.Collections.Generic;
//using TheNewPanelists.BusinessLayer.UserManagement;

namespace TheNewPanelists.ServiceLayer.UserManagement 
{
    public class UserManagementService : IUserManagementService 
    {
        private bool accountRecoveryFlag = false;
        private string operation {get; set;}
        private UserManagementDataAccess userManagementDataAccess;
        //private UserManagementManager userManagementManager;
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
            else if (this.operation == "ISVALID")
            {
                query = this.EmailValidated();
            }
            else if (operation == "DROPREG")
            {
                query = this.DropRegistration();
            }
            else if (this.operation == "ACCOUNT REGISTRATION")
            {
                query = this.RegisterUser();
            }
            else if (this.operation == "ISVALID")
            {
                query = this.EmailValidated();
            }
            else if (operation == "DROPREG")
            {
                query = this.DropRegistration();
            }
            else if (this.operation == "ACCOUNT REGISTRATION")
            {
                query = this.RegisterUser();
            }
            this.userManagementDataAccess = new UserManagementDataAccess(query);
            //if (this.userManagementDataAccess.SelectAccount(accountRecoveryFlag) == false)
            //{
            //    return false;
            //}
            return this.userManagementDataAccess.SelectAccount(accountRecoveryFlag);            
        }

        public Dictionary<string, string> ReturnUser()
        {
            string query = "SELECT u.userId FROM User u WHERE u.username = '" + this.userAccount["username"] + "';";
            this.userManagementDataAccess = new UserManagementDataAccess(query);
            return this.userManagementDataAccess.GetAccountInformation();
        }

        public Dictionary<string, string> ReturnRegistrationEntry()
        {
            Dictionary<string, string> result;
            string query = "";
            if (operation == "VALIDATE")
            {
                query = "SELECT r.email, r.password FROM Registration r WHERE r.url = '" + this.userAccount["url"]
                    + "' AND r.email = '" + this.userAccount["email"] + "' AND r.expiration < NOW() AND r.validated = false;";
            }
            else if (operation == "FINDREG")
            {
                query = "SELECT * FROM Registration r WHERE r.email = '" + this.userAccount["email"] + "';";
            }
            this.userManagementDataAccess = new UserManagementDataAccess(query);
            result = this.userManagementDataAccess.GetRegInformation();
            return result;
        }

        private string EmailValidated()
        {
            return "UPDATE Registration r SET r.validated = TRUE WHERE r.email = '" + this.userAccount["email"] + "';";
        }

        private string DropRegistration()
        {
            return "DELETE r FROM REGISTRATION r WHERE r.email = '" + this.userAccount["email"] + "';";
        }

        private string RegisterUser()
        {
            return $@"INSERT INTO REGISTRATION (email, password, expiration) VALUES ('{this.userAccount["email"]}','{this.userAccount["password"]}', DATE_ADD(NOW(), INTERVAL 24 HOUR));";
        }

       private string FindUser()
        {
            return $"SELECT * FROM User u WHERE u.username = \'{this.userAccount!["username"]}\';";
        }

        private string CreateUser()
        {
            string type1 = "ADMIN";
            string type2 = "REGISTERED";
            string type3 = "DEFAULT";
            //return "INSERT INTO USER (typeID, username, password, email, able, eventAccount) VALUES (2, '" 
            //        + this.userAccount["username"] + "', '" + this.userAccount["password"] + "', '" 
            //        + this.userAccount["email"] + "', false, false);";
            return $@"INSERT INTO USER (typeName, username, password, email) VALUES ('REGISTERED',
                    '{this.userAccount?["username"]}', 
                    '{this.userAccount?["password"]}', 
                    '{this.userAccount?["email"]}');";
        }

        private string DropUser()
        {
            return $"DELETE u FROM USER u WHERE u.username = '{this.userAccount!["username"]}' AND u.password = " 
                 + $"'{ this.userAccount!["password"]}';";
        }

        private string UpdateOptions()
        {   
            string query = "UPDATE USER u SET ";
            for (int i = 0; i < this.userAccount!.Count; i++) {
                if (this.userAccount.ContainsKey("newusername"))
                {
                    query = query + " u.username = '" + this.userAccount!["newusername"]+"'";
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
            string queryWhere = $" WHERE u.username= '{this.userAccount!["username"]}';";
            query = query + queryWhere;
            return query;
        }

        private string UpdateStatus()
        {
            return "UPDATE USER u SET u.status = '" + this.userAccount!["newstatus"] +
                    "' WHERE u.username= '" + this.userAccount["status"]+"';";
        }


        private string AccountRecovery()
        {
            string message = string.Empty; //instantiate a string that will hold the message for the email
            string query = string.Empty; //instantiate a string that will hold the query we want to return
            if (this.userAccount.ContainsKey("username")) //If the user chose 'Forgot Password' they will input their username, if they input their username they qualify for this case
            {
                string email = "SELECT u.email FROM User u WHERE u.username = '" + this.userAccount["username"] + "';"; //Return the email of the user that is selected HOWEVER THIS DOESN'T WORK YET, the email is hardcoded below
                message = "Please reset your password: "; //Email message to be sent, will eventually include front - end link and expiration time
                sendEmail(message); //Call sendEmail function to email given message
                Console.Write("New Password: "); //Ask user for new password
                string password = Console.ReadLine(); //Read in the new password
                if (!string.IsNullOrEmpty(password)) //Make sure the string in not empty
                {
                    this.userAccount["newpassword"] = password; //
                    query = UpdateOptions(); //Call the update option 
                }
            }
            else if (this.userAccount.ContainsKey("email")) //If the user chose 'Forgot Username' they will input their email, if they input their email they qualify for this case
            {
                accountRecoveryFlag = true; //Set boolean to true
                query = "SELECT u.username FROM User u WHERE u.email = '" + this.userAccount["email"] + "';"; //Set query to get username from user with the specified email
            }
            return query; //Return the query
        }

        public void sendEmail(string message)
        {
            using (MailMessage mail = new MailMessage()) //Create an email
            {
                mail.From = new MailAddress("projmotomoto@gmail.com"); //Who you are sending the email from
                mail.To.Add("projmotomoto@gmail.com"); //Who you are sending the email to
                mail.Subject = "MotoMoto Account Recovery"; //What the subject of the email says
                mail.Body = message; //What the body of the email says
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)) //Use built in SmtpClient to send email to/from gmail accounts
                {
                    smtp.Credentials = new NetworkCredential("projmotomoto@gmail.com", "Tester491!"); //Email and password of email you want to send from
                    smtp.EnableSsl = true;
                    smtp.Send(mail); //Send email
                }
            }
        }
        
        public bool IsValidRequest()
        {
            bool containsOperation = this.operation!.Contains("FIND") ||  this.operation!.Contains("CREATE")
                                     || this.operation!.Contains("DROP") || this.operation!.Contains("UPDATE") 
                                     || this.operation!.Contains("ACCOUNT RECOVERY");
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
                    //query = this.AccountRecovery();
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