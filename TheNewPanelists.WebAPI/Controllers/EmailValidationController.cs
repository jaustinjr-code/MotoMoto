using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.ApplicationLayer;
using TheNewPanelists.DataAccessLayer;
using TheNewPanelists.BusinessLayer;
using System.Net.Mail;
using System.Text.RegularExpressions;
using TheNewPanelists.ServiceLayer.UserManagement;
using TheNewPanelists.ServiceLayer.Logging;

namespace app.TheNewPanelists.API.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailValidationController : ControllerBase
    {
        public UserManagementDataAccess AccountDAO = new UserManagementDataAccess();

        [HttpPost]
        public IActionResult ValidateEmail(string email, string url)
        {
            try
            {
                var eAddr = new MailAddress(email);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
            Dictionary<string, string> regAcct = new Dictionary<string, string>();
            regAcct.Add("email", email);
            regAcct.Add("url", url);
            IEntry entry = new RegistrationEntry("VALIDATE EMAIL", regAcct);
            string result = ((RegistrationEntry)entry).EmailConfirmationRequest();

            if (result.StartsWith("Email confirmed."))
                return Ok(true);

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
