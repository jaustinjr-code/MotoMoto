using TheNewPanelists.DataAccessLayer;
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
            else if (this.operation == "UPDATEU")
            {
                query = this.UpdateUsername();
            } 
            else if (this.operation == "UPDATEP")
            {
                query = this.UpdatePassword();
            }
            else if (this.operation == "UPDATEE") {
                query = this.UpdateEmail();
            }
            this.userManagementDataAccess = new UserManagementDataAccess(query);
            if (this.userManagementDataAccess.SelectAccount() == false) return false;
            return true;
        }
        private string FindUser()
        {
            return "SELECT u FROM User u WHERE u.username =" + this.userAccount["username"] + ";";
        }
        private string CreateUser()
        {
            return "INSERT INTO USER (typeId, username, password, email, able, eventAccount) VALUES (2, '" 
                    + this.userAccount["username"] + "', '" + this.userAccount["password"] + "', '" 
                    + this.userAccount["email"] + "', false, false);";
        }
        private string DropUser()
        {
            return "DELETE u FROM USER u WHERE u.username = '" + this.userAccount["username"] + "';";
        }
        private string UpdateUsername()
        {
            return "UPDATE USER u SET u.username = '" + this.userAccount["newusername"] +
                    "' WHERE u.username= '" + this.userAccount["username"]+"';";
        }
        private string UpdatePassword()
        {
            return "UPDATE USER u SET u.password = '" + this.userAccount["newpassword"] +
                    "' WHERE u.username= '" + this.userAccount["username"]+"';";
        }
        private string UpdateEmail()
        {
            return "UPDATE USER u SET u.email = '" + this.userAccount["newemail"] +
                    "' WHERE u.username= '" + this.userAccount["username"]+"';";
        }
    }
}