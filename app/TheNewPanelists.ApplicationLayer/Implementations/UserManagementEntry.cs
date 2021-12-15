using System;
using System.Collections;
using TheNewPanelists.ApplicationLayer;
using TheNewPanelists.ServiceLayer;

namespace TheNewPanelists.ApplicationLayer
{
    class UserManagementEntry : IEntry
    {
        private string operation { get; set; }
        private Dictionary<string, string> request { get; set; }

        private UserManagementManager userManagementManager;

        public UserManagementEntry()
        {

        }
        public UserManagementEntry(string operation, Dictionary<string, string> request)
        {
            this.operation = operation;
            this.request = request;
        }

        public string SingleOperationRequest()
        {
            if (operation.ToUpper() == "CREATE")
            {
                userManagementManager.CallCreateAccount(request);
                return "UM operation was successful";
            }
            else if (operation.ToUpper() == "DELETE")
            {
                userManagementManager.CallDeleteAccount(request);
                return "UM operation was successful";
            }
            else if (operation.ToUpper() == "UPDATE")
            {
                userManagementManager.CallUpdateAccount(request);
                return "UM operation was successful";
            }
            else if (operation.ToUpper() == "DISABLE")
            {
                userManagementManager.CallDisableAccount(request);
                return "UM operation was successful";
            }
            else if (operation.ToUpper() == "ENABLE")
            {
                userManagementManager.CallEnableAccount(request);
                return "UM operation was successful";
            }

            return "UM operation was not successful";
        }

        public bool BulkOperationRequest(string filepath)
        {
            userManagementManager = new UserManagementManager(filepath);
            return true;
            // return userManagementManager.ParseAndCall();
        }
    }
}