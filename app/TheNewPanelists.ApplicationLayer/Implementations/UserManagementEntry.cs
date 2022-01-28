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
            this.userManagementManager = new UserManagementManager();
        }
        public UserManagementEntry(string operation, Dictionary<string, string> request)
        {
            this.operation = operation;
            this.request = request;
            this.userManagementManager = new UserManagementManager();
        }

        public string SingleOperationRequest()
        {

            if (userManagementManager.CallOperation(this.operation, request))
            {
                return "UM operation was successful";
            }
            return "UM operation was not successful";
        }

        public bool BulkOperationRequest(string filepath)
        {
            userManagementManager = new UserManagementManager(filepath);
            return userManagementManager.ParseAndCall();
        }
    }
}