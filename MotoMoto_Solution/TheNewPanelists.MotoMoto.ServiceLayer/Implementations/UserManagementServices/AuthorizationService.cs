using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataAccess;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    /// <summary>
    /// Boundary between business logic for application and external services required
    /// </summary>
    public class AuthorizationService
    {

        // /// <summary>
        // /// Data Access Layer Entity for Part Flagging
        // ///</summary>
        // private readonly IAuthorizationDataAccess __authorizationDataAccess;

        // /// <summary> 
        // /// Default constructor that creates the data access entity for part flagging.
        // /// </summary>
        // public PartFlaggingService()
        // {
        //     __authorizationDataAccess = new AuthorizationDataAccess();
        // }

        public bool CheckAuthorized(string username) {
            return true;
        }

        public bool CheckAuthorized(string username, string featureName) {
            UserManagementDataAccess userManagementDataAccess = new UserManagementDataAccess();
            AuthorizationDataAccess authorizationDataAccess = new AuthorizationDataAccess();
            AccountModel account = new AccountModel();
            account.username = username;

            account = userManagementDataAccess.RetrieveSpecifiedUserEntity(account);
            if (account.accountType is not null) {
                return authorizationDataAccess.GetAuthorizationLevel(featureName, account.accountType).FeatureFound;
            }
            return false;
        }
    }
}
