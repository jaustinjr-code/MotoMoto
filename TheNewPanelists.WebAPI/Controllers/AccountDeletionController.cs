using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.ApplicationLayer;
using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.BusinessLayer;
using TheNewPanelists.ServiceLayer.UserManagement;
using TheNewPanelists.ServiceLayer.Logging;
using Newtonsoft.Json;

namespace app.TheNewPanelists.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountDeletionController : ControllerBase
    {
        public UserManagementDataAccess AccountDAO = new UserManagementDataAccess();

        [HttpPost]
        public IActionResult DeleteUserAccount(string username, string password, Dictionary<string, string> userAcct)
        {
            
            userAcct.Add("username", username);
            userAcct.Add("password", password);
            UserManagementManager manager = new UserManagementManager();

            bool isValid = manager.CallOperation("DROP", userAcct);

            if (isValid == true)
            {
                return Ok(true);
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
