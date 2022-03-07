using System;
using System.Collections;
using TheNewPanelists.ApplicationLayer;
using TheNewPanelists.ServiceLayer;
using TheNewPanelists.BusinessLayer;

namespace TheNewPanelists.ApplicationLayer
{
    public class UserManagementEntry : IEntry
    {
        private string? operation { get; set; }
        private Dictionary<string, string>? request { get; set; }

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
            try
            {

                userManagementManager.CallOperation(this.operation!, request!);

                return "UM operation was successful";
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