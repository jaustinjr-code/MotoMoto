
namespace TheNewPanelists.ServiceLayer.UserManagement 
{
    class UserManagementService : IUserManagementService 
    {

        private string operation {get; set;}
        private Dictionary<string, string> userAccount {get; set;}
        public UserManagementService() {}
        public UserManagementService(string operation, Dictionary<string, string> userAccount) 
        {
            this.operation = operation;
            this.userAccount = userAccount;
        }
        public string SqlGenerator()
        {
            if (this.operation == "FIND")
            {
                return this.FindUser();
            }
            else if (this.operation == "CREATE")
            {
                return this.CreateUser();
            }
            else if (this.operation == "DROP")
            {
                return this.DropUser();
            }
            else if (this.operation == "UPDATEU")
            {
                return this.UpdateUsername();
            } 
            else if (this.operation == "UPDATEP")
            {
                return this.UpdatePassword();
            }
            else if (this.operation == "UPDATEE") {
                return this.UpdateEmail();
            }
            return "";
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