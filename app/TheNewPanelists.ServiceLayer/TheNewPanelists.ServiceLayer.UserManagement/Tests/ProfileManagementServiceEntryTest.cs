using Xunit;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheNewPanelists.ServiceLayer.UserManagement
{
    public class ProfileManagementServiceTest 
    {
        private string operation;
        private bool result;

        [Fact]
        public void IsValidProfile_WithValidDropProfile_ReturnTrue()
        {
            Dictionary<string, string> userProf = new Dictionary<string, string>();
            operation = "DROP";
            userProf.Add("username", "test");
            ProfileManagementService profManagement = new ProfileManagementService(operation, userProf);

            result = profManagement.IsValidRequest();
            Assert.True(result, "Valid Result For Valid Input");
        }

        [Fact]
        public void IsInvalidProfile_WithInvalidOperation_ReturnFalse()
        {
            Dictionary<string, string> userProf = new Dictionary<string, string>();
            operation = "TESTINGERROR";
            userProf.Add("username", "test");
            userProf.Add("status", "FALSE");

            ProfileManagementService profManagement = new ProfileManagementService(operation, userProf);

            result = profManagement.IsValidRequest();
            Assert.False(result, "Invalid Operation For Valid Profile");
        }
    }
}