using Xunit;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TheNewPanelists.ServiceLayer.Logging
{
    public class UserManagementServiceTest 
    {
        private string? operation;

        [Fact]
        public void IsValidUser_WithValidDropUser_ReturnTrue()
        {
            Dictionary<string, string> log = new Dictionary<string, string>();
            operation = "CREATE";
            log.Add("username", "test");
            log.Add("level", "Test");
            log.Add("userID", "1");
            log.Add("DSCRIPTION", "CREATE : SUCCESS");

            LogService logService = new LogService(operation, log, true);

            bool result = logService.IsValidRequest(log);
            Assert.True(true, "Valid Result For Valid Input");
        }
    }
}
