using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheNewPanelists.ServiceLayer.UserManagement;

namespace TheNewPanelists.BusinessLayer
{
    public class UserManagementManager
    {
        private string requestPath;
        public UserManagementManager() 
        {
            requestPath = "";
        }

        public UserManagementManager(string filepath)
        {
            this.requestPath = filepath;
        }

        public bool IsValidRequest(Dictionary<string, string> request)
        {
            bool containsOperation = request.ContainsKey("operation");
            if  (containsOperation)
            {
                return HasValidAttributes(request["operation"].ToUpper(), request);
            }
            return false;
        }

        public bool HasValidAttributes(string operation, Dictionary<string, string> attributes)
        {
            bool hasValidAttributes = false;
            switch (operation.ToUpper()) 
            {
                case "FIND":
                    hasValidAttributes = attributes.ContainsKey("username");
                    break;

                case "CREATE":
                    hasValidAttributes = attributes.ContainsKey("username") && attributes.ContainsKey("password")
                                            && attributes.ContainsKey("email");
                    break;
                
                case "DROP":
                    hasValidAttributes = attributes.ContainsKey("username");
                    break;

            case "UPDATE":
                hasValidAttributes = (attributes.ContainsKey("newusername") || attributes.ContainsKey("newpassword")
                                        || attributes.ContainsKey("newemail")) && attributes.ContainsKey("username");
                break;
            case "ACCOUNT RECOVERY":
                hasValidAttributes = attributes.ContainsKey("username") || attributes.ContainsKey("email");
                break;
        }
        return hasValidAttributes;

        }

        public void ParseAndCall()
        {
            string requestPath = this.requestPath;
            foreach (string line in System.IO.File.ReadLines(@requestPath))
            {
                Dictionary<string,string> requestDictionary = JsonSerializer.Deserialize<Dictionary<string,string>>(line) ?? throw new ArgumentException();
                string operation = requestDictionary["operation"];
                if (IsValidRequest(requestDictionary))
                {
                    CallOperation(operation, requestDictionary);
                }
            }
        }

        public bool CallOperation(string operation, Dictionary<string, string> accountInfo)
        {
            bool returnVal = false;
            if (HasValidAttributes(operation, accountInfo))
            {
                UserManagementService userManagmementServiceObject = new UserManagementService(operation, accountInfo);
                if (userManagmementServiceObject.SqlGenerator())
                {
                    returnVal = true;
                }
            }
            
            return returnVal;
        }

    }
}
