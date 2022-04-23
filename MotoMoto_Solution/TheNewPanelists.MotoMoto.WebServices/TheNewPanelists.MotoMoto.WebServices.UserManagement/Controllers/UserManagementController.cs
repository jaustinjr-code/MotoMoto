using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using Microsoft.AspNetCore.Cors;

namespace TheNewPanelists.MotoMoto.WebServices.UserManagement.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserManagementController : ControllerBase
    { 
        private readonly UserManagementDataAccess _userManagementDataAccess = new UserManagementDataAccess();
        private readonly LogService _logService = new LogService();

        [HttpOptions]
        public IActionResult PreFlightRoute()
        {
            return NoContent();
        }

        [HttpGet]
        public async Task<ISet<AccountModel>> GetUserAccounts(string username, CancellationToken token)
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            ISet<AccountModel> retrieveAllAccounts = manager.RetrieveAllUsers(username);
            await Task.Delay(10_000, token);
               
            return retrieveAllAccounts;
        }

        [HttpPost]
        public IActionResult RetrieveAllUsers(DataStoreUser user, CancellationToken token)
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            try
            {
                ISet<AccountModel> account = manager.RetrieveAllUsers(user!._username!);
                return Ok(account);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }
        [HttpDelete]
        public IActionResult DeleteAccount(string _username, string _password, CancellationToken token)
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            try
            {
                var deleteAccountModel = new DeleteAccountModel()
                {
                    _username = _username,
                    _verifiedPassword = _password
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


/*
 [HttpDelete]
        public IActionResult DeleteAccount(string _username, string _password)
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
 */