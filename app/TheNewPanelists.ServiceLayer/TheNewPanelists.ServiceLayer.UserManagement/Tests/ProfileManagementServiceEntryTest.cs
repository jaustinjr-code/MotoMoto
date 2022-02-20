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

            result = profManagement.IsValidRequest(userProf);
            Assert.True(true, "Valid Result For Valid Input");
        }
    }
}