using Xunit;
using TheNewPanelists.MotoMoto.Models;
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
            RegistrationRequestModel model = new RegistrationRequestModel () {
                Email = testEmail,
                RegistrationId = testRegistrationId
            };

            result = registrationService.SendEmailConfirmationRequest(model);
            Assert.True(result, "Send Email Test Failure.");
        }
    }
}