using Xunit;
using TheNewPanelists.BusinessLayer;
using System.Collections.Generic;
namespace MotoMoto_Unit_Tests;

public class IsValidRequestTests 
{
    [Fact]
    public void IsValidRequest_WithValidFind_ReturnTrue()
    {
        TheNewPanelists.BusinessLayer.UserManagementManager userManagementManager = new TheNewPanelists.BusinessLayer.UserManagementManager();
        Dictionary<string, string> request = new Dictionary<string, string>();
        request.Add("operation", "find");
        request.Add("username", "bcdelrey");
        bool result = userManagementManager.IsValidRequest(request);
        Assert.True(result, "Valid Result For Valid Input");
    } 

    [Fact]
    public void IsValidRequest_WithInvalidFind_ReturnFalse()
    {
        TheNewPanelists.BusinessLayer.UserManagementManager userManagementManager = new TheNewPanelists.BusinessLayer.UserManagementManager();
        Dictionary<string, string> request = new Dictionary<string, string>();
        request.Add("operation", "find");
        bool result = userManagementManager.IsValidRequest(request);
        Assert.False(result, "Valid Result For Invalid Input");
    }

    [Fact]
    public void IsValidRequest_WithValidCreate_ReturnTrue()
    {
        TheNewPanelists.BusinessLayer.UserManagementManager userManagementManager = new TheNewPanelists.BusinessLayer.UserManagementManager();
        Dictionary<string, string> request = new Dictionary<string, string>();
        request.Add("operation", "create");
        request.Add("username", "bcdelrey");
        request.Add("password", "password");
        request.Add("email", "email");
        bool result = userManagementManager.IsValidRequest(request);
        Assert.True(result, "Valid Result For Valid Input");
    }

    [Fact]
    public void IsValidRequest_WithInvalidCreate_ReturnFalse()
    {
        TheNewPanelists.BusinessLayer.UserManagementManager userManagementManager = new TheNewPanelists.BusinessLayer.UserManagementManager();
        Dictionary<string, string> request = new Dictionary<string, string>();
        request.Add("operation", "create");
        request.Add("username", "bcdelrey");
        request.Add("email", "email");
        bool result = userManagementManager.IsValidRequest(request);
        Assert.False(result, "Valid Result For Valid Input");
    }

    [Fact]
    public void IsValidRequest_WithValidDrop_ReturnTrue()
    {
        TheNewPanelists.BusinessLayer.UserManagementManager userManagementManager = new TheNewPanelists.BusinessLayer.UserManagementManager();
        Dictionary<string, string> request = new Dictionary<string, string>();
        request.Add("operation", "drop");
        request.Add("username", "bcdelrey");
        bool result = userManagementManager.IsValidRequest(request);
        Assert.True(result, "Valid Result For Valid Input");
    }

    [Fact]
    public void IsValidRequest_WithInvalidDrop_ReturnFalse()
    {
        TheNewPanelists.BusinessLayer.UserManagementManager userManagementManager = new TheNewPanelists.BusinessLayer.UserManagementManager();
        Dictionary<string, string> request = new Dictionary<string, string>();
        request.Add("operation", "drop");
        request.Add("password", "password");
        bool result = userManagementManager.IsValidRequest(request);
        Assert.False(result, "Valid Result For Valid Input");
    }

    public static IEnumerable<object[]> dictListValidUpdate = new List<object[]> { 
                                                                        new object[] { new Dictionary<string, string> {{ "operation", "update"}, {"username", "bcdelrey"}, 
                                                                                                                        {"newusername", "bcdelrey1"}}},  
                                                                        
                                                                        new object[] { new Dictionary<string, string> { {"operation", "update"}, {"username", "bcdelrey"}, 
                                                                                                                        {"newpassword", "password1"}}}, 
                                                                        
                                                                        new object[] { new Dictionary<string, string> { {"operation", "update"}, {"username", "bcdelrey"}, 
                                                                                                                        {"newemail", "bcdelrey1@gmail.com"}}}, 
                                                                        
                                                                        new object[] { new Dictionary<string, string> { {"operation", "update"}, 
                                                                                                                        {"username", "bcdelrey"}, 
                                                                                                                        {"newusername", "bcdelrey1"}, 
                                                                                                                        {"newpassword", "password1"},
                                                                                                                        {"newemail", "bcdelrey1@gmail.com"}}}
                                                                                    };
    [Theory]
    [MemberData(nameof(dictListValidUpdate))]
    public void IsValidRequest_WithValidUpdate_ReturnTrue(Dictionary<string, string> request)
    {
        TheNewPanelists.BusinessLayer.UserManagementManager userManagementManager = new TheNewPanelists.BusinessLayer.UserManagementManager();
        bool result = userManagementManager.IsValidRequest(request);
        Assert.True(result, "Valid Result For Valid Input");
    }

    public static Dictionary<string, string> dict1 = new Dictionary<string, string> { { "operation", "update" }, { "username", "bcdelrey" }};
    public static Dictionary<string, string> dict2 = new Dictionary<string, string> { { "operation", "update" }, { "newusername", "bcdelrey" }};
    public static Dictionary<string, string> dict3 = new Dictionary<string, string> { { "operation", "update" }, { "newemail", "bcdelrey@gmail.com" }};
    public static Dictionary<string, string> dict4 = new Dictionary<string, string> { { "operation", "update" }, { "newpassword", "password1" }};
    public static Dictionary<string, string> dict5 = new Dictionary<string, string> {{ "newpassword", "bcdelrey" }};
    public static IEnumerable<object[]> dictListInvalidUpdate = new List<object[]> { new object[] { dict1 },  new object[] { dict2 }, 
                                                                        new object[] { dict3 }, new object[] { dict4 }, new object[] { dict5 }};
    [Theory]
    [MemberData(nameof(dictListInvalidUpdate))]
    public void IsValidRequest_WithInvalidUpdate_ReturnFalse(Dictionary<string, string> request)
    {
        TheNewPanelists.BusinessLayer.UserManagementManager userManagementManager = new TheNewPanelists.BusinessLayer.UserManagementManager();
        bool result = userManagementManager.IsValidRequest(request);
        Assert.False(result, "Valid Result For Valid Input");
    }
}

public class ParseAndCallTests 
{
    

}