using System;
using TheNewPanelists.ApplicationLayer;
using TheNewPanelists.ServiceLayer;

namespace TheNewPanelists.ApplicationLayer
{
    class UserManagementEntry : IEntry
    {
        private string operation {get; set;}

        private UserManagementManager userManagementManager;
    
        public UserManagementEntry()
        {

        }
        public UserManagementEntry(string operation)
        {
            this.operation = operation;
        }

        public string SingleOperationRequest()
        {
            if (operation.ToUpper() == "CREATE")
            {
                userManagementManager.CallCreateAccount();
                return "UM operation was successful";
            }
            else if (operation.ToUpper() == "DELETE")
            {
                userManagementManager.CallDeleteAccount();
                return "UM operation was successful";
            }
            else if (operation.ToUpper() == "UPDATE")
            {
                userManagementManager.CallUpdateAccount();
                return "UM operation was successful";
            }
            else if (operation.ToUpper() == "DISABLE")
            {
                userManagementManager.CallDisableAccount();
                return "UM operation was successful";
            }
            else if (operation.ToUpper() == "ENABLE")
            {
                userManagementManager.CallEnableAccount();
                return "UM operation was successful";
            }

            return "UM operation was not successful";
        }
    }
}