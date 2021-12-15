using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheNewPanelists.ServiceLayer.UserManagement;

class UserManagementManager
{
    private List<string> request;
    private UserManagementService userManagementService;
    
    public UserManagementManager() {}
    public UserManagementManager(List<string> request)
    {
        this.request = request;
        this.userManagementService = new UserManagementService();
    }

    // public UserManagementManager(string filepath)
    // {
    //     this.request = ParseFile(filepath);
    //     this.userManagementService = new UserManagementService();
    // }

    // public List<string> ParseFile(string path)
    // {
    //     List<string> requests = new List<string>();
    //     string[] allLines = File.ReadAllLines(path);

    //     foreach (string line in allLines)
    //     {
    //         requests.Add(line);
    //     }

    //     return requests;
    // }

    // public bool IsValidRequest(Dictionary<String, String> request)
    // {
    //     bool containsOperation = request.ContainsKey("operation");
    //     bool containsUsername = request.ContainsKey("username");
    //     bool containsPassword = request.ContainsKey("password");

    //     bool hasValidOperation = false;
    //     if (containsOperation)
    //     {
    //         hasValidOperation = request["operation"].ToUpper() == "CREATE" || request["operation"].ToUpper() == "DELETE"
    //                            || request["operation"].ToUpper() == "UPDATE" || request["operation"].ToUpper() == "ENABLE"
    //                            || request["operation"].ToUpper() == "DISABLE";
    //     }
    //     if (containsUsername && containsPassword && hasValidOperation)
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    // public void ParseAndCall()
    // {
    //     foreach (string line in this.request)
    //     {
    //         Dictionary<String,String> requestDictionary = JsonSerializer.Deserialize<Dictionary<String,String>>(line) ?? throw new ArgumentException();
    //         string operation = requestDictionary["operation"];
    //         requestDictionary.Remove("operation");
    //         if (IsValidRequest(requestDictionary))
    //         {
    //             switch (operation.ToUpper())
    //             {
    //                 case "CREATE":
    //                     CallCreateAccount(requestDictionary);
    //                     break;

    //                 case "DELETE":
    //                     CallDeleteAccount(requestDictionary);
    //                     break;

    //                 case "UPDATE":
    //                     CallUpdateAccount(requestDictionary);
    //                     break;

    //                 case "ENABLE":
    //                     CallEnableAccount(requestDictionary);
    //                     break;

    //                 case "DISABLE":
    //                     CallDisableAccount(requestDictionary);
    //                     break;

    //                 default:
    //                     Console.WriteLine("**INVALID OPERATION**");
    //                     break;
    //             }
    //         }
            
    //     }
        
    // }

    // public bool CallCreateAccount(Dictionary<string, string> accountInfo)
    // {
    //     if (userManagementService.CreateAccountRequest(accountInfo))
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    // public bool CallDeleteAccount(Dictionary<string, string> accountInfo)
    // {
    //     if (userManagementService.DeleteAccountRequest(accountInfo))
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    // public bool CallUpdateAccount(Dictionary<string, string> accountInfo)
    // {
    //     if (userManagementService.UpdateAccountRequest(accountInfo))
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    // public bool CallEnableAccount(Dictionary<string, string> accountInfo)
    // {
    //     if (userManagementService.EnableAccountRequest(accountInfo))
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

    // public bool CallDisableAccount(Dictionary<string, string> accountInfo)
    // {
    //     if (userManagementService.DisableAccountRequest(accountInfo))
    //     {
    //         return true;
    //     }
    //     else
    //     {
    //         return false;
    //     }
    // }

}