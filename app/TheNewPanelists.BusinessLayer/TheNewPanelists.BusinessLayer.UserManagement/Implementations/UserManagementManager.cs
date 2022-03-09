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
                return HasValidAttributes(request!["operation"].ToUpper(), request);
            }
            return false;
        }

        public bool HasValidAttributes(string operation, Dictionary<string, string>? attributes)
        {
            bool hasValidAttributes = false;
            switch (operation!.ToUpper()) 
            {
                case "FIND":
                    hasValidAttributes = attributes!.ContainsKey("username");
                    break;

                case "CREATE":
                    hasValidAttributes = attributes!.ContainsKey("username") && attributes!.ContainsKey("password")
                                            && attributes!.ContainsKey("email");
                    break;
                
                case "DROP":
                    hasValidAttributes = attributes!.ContainsKey("username");
                    break;

                case "UPDATE":
                    hasValidAttributes = (attributes!.ContainsKey("newusername") || attributes!.ContainsKey("newpassword")
                                            || attributes!.ContainsKey("newemail")) && attributes!.ContainsKey("username");
                    break;
                case "ACCOUNT RECOVERY":
                    hasValidAttributes = attributes!.ContainsKey("username") || attributes!.ContainsKey("email");
                    break;
                case "BULK":
                    hasValidAttributes = attributes!.ContainsKey("bulk");
                    break;
                case "BULK_DELETE":
                    hasValidAttributes = attributes!.ContainsKey("bulk");
                    break;
                default:
                    hasValidAttributes=false;
                    break;

                case "DROPREG":
                    hasValidAttributes = attributes.ContainsKey("email");
                    break;

                case "FINDREG":
                    hasValidAttributes = attributes.ContainsKey("email") && attributes.ContainsKey("url");
                    break;

                case "ISVALID":
                    hasValidAttributes = attributes.ContainsKey("email");
                    break;

                case "ACCOUNT REGISTRATION":
                    hasValidAttributes = attributes.ContainsKey("email") && attributes.ContainsKey("password");
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

        public Dictionary<string, string> ReturnRegOperation(string operation, Dictionary<string, string> accountInfo)
        {
            Dictionary<string, string> result;
            UserManagementService userManagementService = new UserManagementService(operation, accountInfo);
            result = userManagementService.ReturnRegistrationEntry();

            return result;
        }

        public bool CallOperation(string operation, Dictionary<string, string> accountInfo)
        {
            bool returnVal = false;
            if (HasValidAttributes(operation, accountInfo))
            {
                UserManagementService userManagmementServiceObject = new UserManagementService(operation , accountInfo);
                ProfileManagementService profileManagementServiceObject = new ProfileManagementService(operation, accountInfo);

                if (operation == "DROP" || operation == "BULK_DELETE")
                {
                    if (!validateTrueUser(accountInfo)) return false;
                    profileManagementServiceObject.SqlGenerator();
                    userManagmementServiceObject.SqlGenerator();
                    returnVal = true;
                } 
                else
                {
                    userManagmementServiceObject.SqlGenerator();
                    profileManagementServiceObject.SqlGenerator();
                    returnVal = true;
                }
            }
            return returnVal;
        }

        private bool validateTrueUser(Dictionary<string, string> accountInfo)
        {
            UserManagementService userManagmementServiceObject = new UserManagementService("FIND", accountInfo);
            Dictionary<string, string> user = userManagmementServiceObject.ReturnUser();

            if (user.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
