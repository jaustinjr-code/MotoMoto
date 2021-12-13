using TheNewPanelists.ApplicationLayer.Authentication;
using TheNewPanelists.DataAccessLayer.Logging;

namespace app
{
    class Test
    {
        static void Main(string[] args)
        {
            UserManagementAuthentication t = new UserManagementAuthentication();
            t.UserManagementAuthenticationTest();
            LoggingDataAccess l = new LoggingDataAccess("CREATE");
        }
    }
}