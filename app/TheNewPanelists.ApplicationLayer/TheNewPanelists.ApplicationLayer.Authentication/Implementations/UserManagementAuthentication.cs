using TheNewPanelists.ApplicationLayer.Authorization;

namespace TheNewPanelists.ApplicationLayer.Authentication
{
    class UserManagementAuthentication : IAuthentication
    {
        public void UserManagementAuthenticationTest()
        {
            Console.WriteLine("Test");
            UserManagementAuthorization t = new UserManagementAuthorization();
            t.UserManagementAuthorizationTest();
        }
    }
}