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
    public class RegistrationController : ControllerBase
    {
        public UserManagementDataAccess AccountDAO = new UserManagementDataAccess();

        [HttpGet]
        public IActionResult GetUserAccount(string operation, Dictionary<string, string> userAcct)
        {
            UserManagementManager manager = new UserManagementManager();

            try
            {
                bool isValid = manager.HasValidAttributes(operation, userAcct);
                Dictionary<string, string> result = AccountDAO.GetAccountInformation();
                return Ok(isValid);
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
                var eAddr = new MailAddress(email); 
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

            UserManagementManager manager = new UserManagementManager();
            Dictionary<string, string> regAcct = new Dictionary<string, string>();
            regAcct.Add("email", email);
            regAcct.Add("pass", password);

            IEntry entry;
            entry = new RegistrationEntry("ACCOUNT REGISTRATION", regAcct);
            string result = ((RegistrationEntry)entry).RegistrationRequest();

            Console.WriteLine(result);

            if (result.StartsWith("Registration successful"))
                return Ok(true);

            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}
