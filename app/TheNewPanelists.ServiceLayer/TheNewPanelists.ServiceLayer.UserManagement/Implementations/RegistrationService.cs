using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
using System.Collections.Generic;


namespace TheNewPanelists.ServiceLayer.UserManagement 
{
    class RegistrationService : IUserManagementService
    {
        private Dictionary<string, string> accountInfo { get; set; }

        private string operation;

        public RegistrationService()
        {
            this.operation = string.Empty;
            this.accountInfo = new Dictionary<string, string>();
        }
        public RegistrationService(string operation, Dictionary<string, string> acctInfo)
        {
            this.operation = operation;
            this.accountInfo = acctInfo;
        }

        public bool SqlGenerator()
        {
            string query = "";
            if (this.operation == "ISVALID")
            {
                query = this.ValidatedEmail();
            }
            else if (operation == "DROPREG")
            {
                query = this.DropRegistration();
            }
            else if (this.operation == "ACCOUNT REGISTRATION")
            {
                query = this.RegisterUser();
            }
            else if (this.operation == "REGDOESNOTEXIST")
            {
                query = this.ReturnRegistration();
            }
            RegistrationDataAccess registrationDataAccess = new RegistrationDataAccess(this.operation, query);
            return registrationDataAccess.SelectAccount();
        }

        public Dictionary<string, string> ReturnSqlGenerator()
        {
            Dictionary<string, string> result;
            string query ="";
            if (this.operation == "RETURNREG")
            {
                query = this.ReturnRegistration();
            }
            else if (operation == "CONFIRMREG")
            {
                query = this.ConfirmRegistration();
            }
            RegistrationDataAccess registrationDataAccess = new RegistrationDataAccess(this.operation, query);
            result = registrationDataAccess.SingleRowQuery();
            return result;
        }

        public string getQuery()
        {
            string query = "";
            switch (this.operation)
            {
                case "ISVALID":
                    query = this.ValidatedEmail();
                    break;
                case "DROPREG":
                    query = this.DropRegistration();
                    break;
                case "ACCOUNT REGISTRATION":
                    query = this.RegisterUser();
                    break;
                case "RETURNREG":
                    query = this.ReturnRegistration();
                    break;
                case "CONFIRMREG":
                    query = this.ConfirmRegistration();
                    break;
                case "REGDOESNOTEXIST":
                    query = this.ReturnRegistration();
                    break;
            }
            return query;
        }

        public string ReturnRegistration()
        {
            return "SELECT * FROM Registration r WHERE r.email = '" + this.accountInfo["email"] + "';";
        }

        private string ValidatedEmail()
        {
            return "UPDATE Registration r SET r.validated = TRUE WHERE r.email = '" + this.accountInfo["email"] + "';";
        }

        private string ConfirmRegistration()
        {
            return "SELECT r.email, r.password FROM Registration r WHERE r.url = '" + this.accountInfo["url"]
                        + "' AND r.email = '" + this.accountInfo["email"] + "' AND NOW() < r.expiration AND r.validated = false;";
        }
        
        private string DropRegistration()
        {
            return "DELETE r FROM REGISTRATION r WHERE r.email = '" + this.accountInfo["email"] + "';";
        }

        private string RegisterUser()
        {
            return $@"INSERT INTO REGISTRATION (email, password, expiration) VALUES ('{this.accountInfo["email"]}','{this.accountInfo["password"]}', DATE_ADD(NOW(), INTERVAL 24 HOUR));";
        }
     }
}
