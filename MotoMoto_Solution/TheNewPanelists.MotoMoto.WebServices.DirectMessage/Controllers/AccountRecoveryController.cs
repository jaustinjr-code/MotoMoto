using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer.Implementations;
using TheNewPanelists.MotoMoto.DataAccess.Impementations.UserManagement;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.WebServices.Login.Controllers
{
    //[Route("[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRecoveryController : ControllerBase
    {
        private readonly AccountRecoveryDataAccess _accountRecoveryDataAccess = new AccountRecoveryDataAccess();

        [HttpGet("GetUsername")]
        //[Route("GetUsername")]
        public IActionResult GetUsername(string email)
        {
            AccountRecoveryService service = new AccountRecoveryService(_accountRecoveryDataAccess); //Move this to top, so you're not remaking instances
            AccountRecoveryManager manager = new AccountRecoveryManager(service);
            try
            {
                bool retrieveUserUsername = manager.AccountRecoveryRetrieveUsername(email);
                return Ok(retrieveUserUsername);
            }
            catch(Exception e)
            {
                return BadRequest(e);
                //return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
