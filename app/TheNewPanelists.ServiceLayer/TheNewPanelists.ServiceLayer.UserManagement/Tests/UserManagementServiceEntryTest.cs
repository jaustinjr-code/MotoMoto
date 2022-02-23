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
            userAcct.Add("password", "test");

            UserManagementService service = new UserManagementService(operation, userAcct);
            
            result = service.IsValidRequest();
            Assert.True(result, "Valid Result For Valid Drop Input");
        }

        [Fact]
        public void IsInvalidUser_WithInvalidOperation_ReturnFalse()
        {
            Dictionary<string, string> userAcct = new Dictionary<string, string>();
            operation = "INVALID";
            userAcct.Add("username", "test");
            UserManagementService service = new UserManagementService(operation, userAcct);

            result = service.IsValidRequest();
            Assert.False(result, "Invalid Result For Invalid Operation");
        }

        [Fact]
        public void IsValidUser_WithValidCreateUser_ReturnTrue()
        {
            Dictionary<string, string> userAcct = new Dictionary<string, string>();
            operation = "CREATE";
            userAcct.Add("username", "test");
            userAcct.Add("password", "test");
            userAcct.Add("email", "test@test.com");
            UserManagementService service = new UserManagementService(operation, userAcct);

            result = service.IsValidRequest();
            Assert.True(result, "Valid Result For CREATE Operation");
        }

        [Fact]
        public void IsValidUser_WithValidUpdateUserPW_ReturnTrue()
        {
            Dictionary<string, string> userAcct = new Dictionary<string, string>();
            operation = "UPDATE";
            userAcct.Add("username", "test");
            userAcct.Add("password", "test");
            userAcct.Add("email", "test@test.com");
            userAcct.Add("newpassword", "test1");

            UserManagementService service = new UserManagementService(operation, userAcct);

            result = service.IsValidRequest();
            Assert.True(result, "Valid Result For UPDATE Operation");
        }

        [Fact]
        public void IsValidUser_WithValidUpdateUserEM_ReturnTrue()
        {
            Dictionary<string, string> userAcct = new Dictionary<string, string>();
            operation = "UPDATE";
            userAcct.Add("username", "test");
            userAcct.Add("password", "test");
            userAcct.Add("email", "test@test.com");
            userAcct.Add("newemail", "test1@test1.com");

            UserManagementService service = new UserManagementService(operation, userAcct);

            result = service.IsValidRequest();
            Assert.True(result, "Valid Result For UPDATE Operation");
        }
    }
}
    
