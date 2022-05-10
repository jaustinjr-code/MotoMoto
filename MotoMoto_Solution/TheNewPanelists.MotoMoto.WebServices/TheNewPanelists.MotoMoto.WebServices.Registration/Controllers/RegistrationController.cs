using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.WebServices.Registration.Controllers
{
    ///<summary>Registration controller. Responsible for communicating registration requests from frontend to backend</summary>
    [Route("Api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        ///<summary>Route for registering an account from the registration view in the frontend</summary>
        ///<param name="email">represents the new user's email.</param>
        ///<param name="password">represents the new user's password.</param>
        ///<returns>If the request status is set to true, returns Ok(RegistrationRequestModel) Json object. 
        ///Returns BadRequest(RegistrationRequestModel) Json object otherwise.</returns>
        [Route("Register")]
        public IActionResult RegisterAccount(string email, string password)
        {
            RegistrationRequestModel model = new RegistrationRequestModel()
            {
                Email = email,
                Password = password,
                status = false,
                message = "No message recorded."
            };

            RegistrationService registrationService = new RegistrationService();
            registrationService.AccountRegistrationRequest(ref model);
            
            if(model.status)
                return Ok(model);
            else
                return BadRequest(model);
        }

        ///<summary>Route for confirming a registration from the registration view in the frontend</summary>
        ///<param name="email">represents the new user's email.</param>
        ///<param name="registrationId">represents the new user's registration ID.</param>
        ///<returns>If the request status is set to true, returns Ok(RegistrationRequestModel) Json object. 
        ///Returns BadRequest(RegistrationRequestModel) Json object otherwise.</returns>
        [Route("Confirmation")]
        public IActionResult ConfirmEmail(string email, int registrationId)
        {
            RegistrationRequestModel model = new RegistrationRequestModel()
            {
                Email = email,
                RegistrationId = registrationId,
                status = false,
                message = "No message recorded."
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
