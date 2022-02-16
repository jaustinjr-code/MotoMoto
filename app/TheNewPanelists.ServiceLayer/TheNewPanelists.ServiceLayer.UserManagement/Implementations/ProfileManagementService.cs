using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.Logging;
//using TheNewPanelists.BusinessLayer.UserManagement;

namespace TheNewPanelists.ServiceLayer.UserManagement 
{
    class ProfileManagementService : IUserManagementService 
    {
        private string operation {get; set;}
        //private ProfileManagementDataAccess profileManagementDataAccess;
        //private ProfileManagementManager profileManagementManager;
        private Dictionary<string, string> userProfile {get; set;}
        public ProfileManagementService() {}
        
        public ProfileManagementService(string operation, Dictionary<string, string> userProfile) 
        {
            this.operation = operation;
            this.userProfile = userProfile;
            //this.profileManagementDataAccess = new ProfileManagementDataAccess();
            //this.profileManagementManager = new ProfileManagementManager();
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
            this.userManagementDataAccess = new UserManagementDataAccess(query);
            if (this.userManagementDataAccess.SelectAccount() == false) 
            {
                return false;
            }
            return true;
        }
