using Xunit;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class UserManagementDataAccessUnitTest
    {
        private bool result;

        [Fact]
        public void IsValidDeleteDataAccessOperation_WithValidDeleteUserEntity_ReturnTrue()
        {
            UserManagementDataAccess userManagementDAO = new UserManagementDataAccess();
            var userTestAccountDeletionModel = new DeleteAccountModel
            {
                Username = "testUsername",
                VerifiedPassword = "testVerifiedPassword"
            };

            result = userManagementDAO.PerminateDeleteAccountEntity(userTestAccountDeletionModel);
            Assert.True(result, "Invalid DeleteAccount Data Access for Valid User Account: User Account or Entity is invalid!!");
        }
    }
}