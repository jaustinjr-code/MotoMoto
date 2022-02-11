using Xunit;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheNewPanelists.BusinessLayer
{
    public class UserManagementManagerTest 
    {
        [Fact]
        public void IsValidRequest_WithValidFind_ReturnTrue()
        {
            TheNewPanelists.BusinessLayer.UserManagementManager userManagementManager = new TheNewPanelists.BusinessLayer.UserManagementManager();
            Dictionary<string, string> request = new Dictionary<string, string>();
            request.Add("operation", "find");
            request.Add("username", "bcdelrey");
            bool result = userManagementManager.IsValidRequest(request);
            Assert.True(true, "Valid Result For Valid Input");
        } 
    }
}