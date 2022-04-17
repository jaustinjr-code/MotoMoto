using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;
using Microsoft.AspNetCore.Cors;

namespace TheNewPanelists.MotoMoto.WebServices.UserManagement.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
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
                    Username = _username,
                    VerifiedPassword = _password
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