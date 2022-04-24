using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using TheNewPanelists.MotoMoto.ServiceLayer;
using System;



namespace TheNewPanelists.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return NoContent();
        }

        [Route("LoginTest")]
        [HttpPost]
        public IActionResult Login(string username)
        {
            /*
            Console.WriteLine("Console");
            AuthenticationService authService = new AuthenticationService();
            try
            {
                bool service = authService.CheckUser(username, "pass");
                return Ok(service);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            */
            return Ok();

        }





    }
}