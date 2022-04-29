using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.WebServices.UserManagement.Controllers
{
    [Route("api/[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly UserManagementDataAccess _userManagementDataAccess = new UserManagementDataAccess();
        [HttpOptions]
        public IActionResult PreFlightRoute()
        {
            return NoContent();
        }

        [HttpGet]
        public IActionResult GetUserAccounts(string username)
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            try
            {
                ISet<AccountModel> retrieveAllAccounts = manager.RetrieveAllUsers(username);
                return Ok(retrieveAllAccounts);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        /*
        [HttpPost]
        public async Task<IActionResult<AccountModel>> RegisterUserAccount(string username, string password, string email)
        {
            UserManagementService service = new UserManagementService(_userManagementDataAccess);
            UserManagementManager manager = new UserManagementManager(service);

            throw new NotImplementedException();
        } */

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