using TheNewPanelists.ApplicationLayer.Authentication;
using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.ServiceLayer.UserManagement;

namespace app
{
    class Test
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("username", "test1");
            dic.Add("newusername", "test");
            dic.Add("password", "test");
            dic.Add("newpassword", "password1");
            dic.Add("email", "test");
            dic.Add("newemail", "testemail1");
            IUserManagementService i = new UserManagementService("UPDATE", dic);
            i.SqlGenerator();

        }
    }
}

// UserManagementAuthentication t = new UserManagementAuthentication();
//             t.UserManagementAuthenticationTest();
//             IDataAccess l = new LoggingDataAccess("CREATE", false);
//             Console.WriteLine(l.EstablishMariaDBConnection());