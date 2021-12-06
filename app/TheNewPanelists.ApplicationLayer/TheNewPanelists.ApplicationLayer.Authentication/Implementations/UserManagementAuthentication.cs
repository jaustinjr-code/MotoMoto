using TheNewPanelists.ApplicationLayer.Authorization;

namespace TheNewPanelists.ApplicationLayer.Authentication
{
    class UserManagementAuthentication
    {
        public void UserManagementAuthenticationTest()
        {
            Console.WriteLine("Test");
            UserManagementAuthorization t = new UserManagementAuthorization();
            t.UserManagementAuthorizationTest();
        }
    }
}