using System;
using System.Collections;
using TheNewPanelists.ApplicationLayer;
using TheNewPanelists.ServiceLayer;
using TheNewPanelists.BusinessLayer;
using TheNewPanelists.ApplicationLayer.Authorization;

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
            string failureMessage = "UM operation was not successful";
            string successMessage = "UM operation was successful";

            UserManagementAuthorization authorization = new UserManagementAuthorization();
            bool isAuthorizedOperation = authorization.checkAuthorized(this.operation);
            if (!isAuthorizedOperation) 
            {
                return failureMessage;
            }

            try
            {
                bool isSuccessful = userManagementManager.CallOperation(this.operation, request);
                if (isSuccessful) {
                    return successMessage;
                }
                else {
                    return failureMessage;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "ERROR - UM operation was not successful";
            }
        }

        public bool BulkOperationRequest(string filepath)
        {
            UserManagementAuthorization authorization = new UserManagementAuthorization();
            bool isAuthorizedOperation = authorization.checkAuthorized(this.operation);
            if (!isAuthorizedOperation) 
            {
                return false;
            }

            try
            {
                userManagementManager = new UserManagementManager(filepath);
                // userManagementManager.ParseAndCall();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("ERROR - Filepath not found");
            }
            return false;
        }
    }
}