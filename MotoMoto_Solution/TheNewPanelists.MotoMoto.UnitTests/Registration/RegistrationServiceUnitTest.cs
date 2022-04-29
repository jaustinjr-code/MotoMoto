using Xunit;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class RegistrationServiceUnitTest
    {
        private bool result;

        [Fact]
        public void Send_Email_Confirmation_Request_ReturnTrue()
        {
            // Password for gmail account: Secret#1
            var testEmail = "motomoto1ca@gmail.com";
            var testRegistrationId = 542356;

            RegistrationService registrationService = new RegistrationService();

            result = registrationService.SendEmailConfirmationRequest(testEmail, testRegistrationId);
            Assert.True(result, "Send Email Test Failure.");
        }
    }
}