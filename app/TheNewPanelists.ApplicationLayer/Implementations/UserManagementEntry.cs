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
            UserManagementAuthorization authorization = new UserManagementAuthorization();
            try
            {
                bool isSuccessful = userManagementManager.CallOperation(this.operation, request, authorization);
                if (isSuccessful) {
                    return "UM operation was successful";
                }
                else {
                    return "UM operation was not successful";
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "ERROR - UM operation was not successful";
            }
            // if (userManagementManager.CallOperation(this.operation, request))
            // {
            //     return "UM operation was successful";
            // }
            // return "UM operation was not successful";
        }

        public bool BulkOperationRequest(string filepath)
        {
            UserManagementAuthorization authorization = new UserManagementAuthorization();
            try
            {
                userManagementManager = new UserManagementManager(filepath);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("ERROR - Filepath not found");
            }
            return false;
            //userManagementManager.ParseAndCall();
            // return true;
        }
    }
}