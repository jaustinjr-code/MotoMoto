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
    // [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        public UserManagementDataAccess AccountDAO = new UserManagementDataAccess();

        [HttpGet]
        public IActionResult GetRegistrationt(string email)
        {
            RegistrationManager registrationManager = new RegistrationManager();
            Dictionary<string, string> regInfo = new Dictionary<string, string>();
            string? checkedEmail = email;

            try
            {
                var eAddr = new MailAddress(checkedEmail);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            regInfo.Add("email", checkedEmail);
            try
            {
                Dictionary<string, string> response;
                response = registrationManager.ReceiveOperation("RETURNREG", regInfo);
                return Ok(true);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
        }

        [HttpPost]
        public IActionResult RegisterUserAccount(string email, string password)
        {
            string checkedEmail = email;

            try 
            { 
                var eAddr = new MailAddress(checkedEmail); 
            }
            catch 
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            Regex letter = new Regex(@"[a-zA-Z]");
            Regex num = new Regex(@"[0-9]");
            Regex specialChar = new Regex(@"[. ,@!]");

            bool passwordValid = letter.IsMatch(password) && num.IsMatch(password)
                && specialChar.IsMatch(password) && (password.Length > 8);

            if (!passwordValid)
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);

            Dictionary<string, string> regAcct = new Dictionary<string, string>();
            regAcct.Add("email", checkedEmail);
            regAcct.Add("pass", password);

            IEntry entry;
            entry = new RegistrationEntry("ACCOUNT REGISTRATION", regAcct);
            string result = ((RegistrationEntry)entry).RegistrationRequest();

            // Console.WriteLine(result);

            if (result.StartsWith("Registration successful"))
                return Ok(true);

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
