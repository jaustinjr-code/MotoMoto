using Xunit;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataStoreEntities;

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
            var testRegistrationId = 105;

            RegistrationService registrationService = new RegistrationService();
            RegistrationRequestModel model = new RegistrationRequestModel () {
                Email = testEmail,
                RegistrationId = testRegistrationId
            };

            result = registrationService.SendEmailConfirmationRequest(model);
            Assert.True(result, "SendEmailConfirmationRequest() Test Failure.");
        }

        [Fact]
        public void Generate_Unique_Name_ReturnTrue()
        {
            var testEmail = "motomoto1ca@gmail.com";
            var testRegistrationId = 35;
            var expected = "035motomoto1ca";

            RegistrationService registrationService = new RegistrationService();
            RegistrationRequestModel model = new RegistrationRequestModel () {
                Email = testEmail,
                RegistrationId = testRegistrationId
            };

            string result = registrationService.GenerateUniqueName(model);

            var emailSplitString = model.Email.Split('@');
            var beforeAtSymbol = emailSplitString[0];

            Assert.True((result == expected), "GenerateUniqueName() Test Failure.");
        }
    }
}