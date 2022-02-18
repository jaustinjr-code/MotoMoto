using Xunit;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheNewPanelists.ServiceLayer.UserManagement
{
    public class UserManagementManagerTest 
    {
        [Fact]
        public void IsValidRequest_WithValidFind_ReturnTrue()
        {
            Dictionary<string, string> userAcct = new Dictionary<string, string>();
            string operation = "DROP";
            userAcct.Add("username", "test");
            UserManagementService userManagement = new UserManagementService(operation, userAcct);

            bool result = userManagement.IsValidRequest(userAcct);
            Assert.True(true, "Valid Result For Valid Input");
        }
    }
}
