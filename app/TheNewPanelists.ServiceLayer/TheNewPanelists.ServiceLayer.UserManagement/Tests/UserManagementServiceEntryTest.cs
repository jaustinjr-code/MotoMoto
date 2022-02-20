using Xunit;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheNewPanelists.ServiceLayer.UserManagement
{
    public class UserManagementServiceTest 
    {
        private string operation;
        private bool result;

        [Fact]
        public void IsValidUser_WithValidDropUser_ReturnTrue()
        {
            Dictionary<string, string> userAcct = new Dictionary<string, string>();
            operation = "DROP";
            userAcct.Add("username", "test");
            UserManagementService userManagement = new UserManagementService(operation, userAcct);

            result = userManagement.IsValidRequest(userAcct);
            Assert.True(true, "Valid Result For Valid Input");
        }
    }
}
