using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.WebServices.Registration.Controllers
{
    [Route("/Api")]
    [ApiController]
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Register")]
        public IActionResult RegisterAccount(string email, string password)
        {
            RegistrationRequestModel model = new RegistrationRequestModel()
            {
                Email = email,
                Password = password
            };

            try
            {
                RegistrationService registrationService = new RegistrationService();
                return Ok(registrationService.AccountRegistrationRequest(model));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
