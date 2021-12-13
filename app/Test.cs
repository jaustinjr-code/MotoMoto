using TheNewPanelists.ApplicationLayer.Authentication;
using TheNewPanelists.DataAccessLayer;

namespace app
{
    class Test
    {
        static void Main(string[] args)
        {
            UserManagementAuthentication t = new UserManagementAuthentication();
            t.UserManagementAuthenticationTest();
            IDataAccess l = new LoggingDataAccess("CREATE");
            Console.WriteLine(l.EstablishMariaDBConnection());
        }
    }
}