using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheNewPanelists.ServiceLayer.UserManagement;

class UserManagementManager
{
    private List<string> request;
    
    public UserManagementManager() 
    {
        request = new List<string>();
    }
    public UserManagementManager(List<string> request)
    {
        this.request = request;
    }

    public UserManagementManager(string filepath)
    {
        this.request = ParseFile(filepath);
    }

    public List<string> ParseFile(string path)
    {
        List<string> requests = new List<string>();
        string[] allLines = File.ReadAllLines(path);

        foreach (string line in allLines)
        {
            requests.Add(line);
        }

        return requests;
    }

    public bool IsValidRequest(Dictionary<String, String> request)
    {
        bool containsOperation = request.ContainsKey("operation");
        if  (containsOperation)
        {
            return HasValidAttributes(request["operation"].ToUpper(), request);
        }
        return false;
    }

    public bool HasValidAttributes(string operation, Dictionary<String, String> attributes)
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

        }
        return hasValidAttributes;

    }

    public void ParseAndCall()
    {
        foreach (string line in this.request)
        {
            Dictionary<String,String> requestDictionary = JsonSerializer.Deserialize<Dictionary<String,String>>(line) ?? throw new ArgumentException();
            string operation = requestDictionary["operation"];
            requestDictionary.Remove("operation");
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