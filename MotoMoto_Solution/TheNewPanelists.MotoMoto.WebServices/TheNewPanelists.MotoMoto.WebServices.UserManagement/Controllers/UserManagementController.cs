using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using Microsoft.AspNetCore.Cors;

namespace TheNewPanelists.MotoMoto.WebServices.UserManagement.Controllers
{
    [Route("api/[controller]")]
    public class UserManagementController : ControllerBase
    { 
        private readonly UserManagementDataAccess _userManagementDataAccess = new UserManagementDataAccess();
        private readonly LogService _logService = new LogService();

        public IActionResult PreFlightRoute()
        {
            return NoContent();
        }

        public async Task<ISet<AccountModel>> GetUserAccounts(string username, CancellationToken token = default(CancellationToken))
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            ISet<AccountModel> retrieveAllAccounts = manager.RetrieveAllUsers(username);
            await Task.Delay(10_000, token);
               
            return retrieveAllAccounts;
        }

        public IActionResult RetrieveAllUsers(DataStoreUser user, CancellationToken token = default(CancellationToken))
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            try
            {
                ISet<AccountModel> account = manager.RetrieveAllUsers(user!.username!);
                return Ok(account);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
        public IActionResult DeleteAccount(string _username, string _password, CancellationToken token = default(CancellationToken))
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            try
            {
                var deleteAccountModel = new DeleteAccountModel()
                {
                    username = _username,
                    verifiedPassword = _password
                };
                bool result = manager.PerminateDeleteAccountManager(deleteAccountModel);
                return Ok();
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}