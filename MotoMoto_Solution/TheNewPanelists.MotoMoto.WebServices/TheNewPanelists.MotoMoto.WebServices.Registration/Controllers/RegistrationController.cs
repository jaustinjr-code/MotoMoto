using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.WebServices.Registration.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Register")]
        public IActionResult RegisterAccount(string email, string password)
        {
            RegistrationRequestModel model = new RegistrationRequestModel()
            {
                Email = email,
                Password = password,
                status = false,
                message = "N/A"
            };
           
            RegistrationService registrationService = new RegistrationService();
            registrationService.AccountRegistrationRequest(ref model);
            
            if(model.status)
                return Ok(model);
            else
                return BadRequest(model);
        }

        [HttpPost("Confirmation")]
        public IActionResult ConfirmEmail(string email, int registrationId)
        {
            RegistrationRequestModel model = new RegistrationRequestModel()
            {
                Email = email,
                RegistrationId = registrationId,
                Password = "N/A",
                status = false,
                message = "N/A"
            };
           
            RegistrationService registrationService = new RegistrationService();
            registrationService.EmailConfirmation(ref model);
            
            if(model.status)
                return Ok(model);
            else
                return BadRequest(model);
        }
    }
}
