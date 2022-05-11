using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.BusinessLayer;



namespace TheNewPanelists.MotoMoto.WebServices.Login.Controllers;

[ApiController]
[Route("[controller]")]

public class LoginController
{
    [Route("Login")]
    // public Login Login()
    // {

    // }

}

    // public class LoginController
    // {
    //     [HttpOptions]
    //     public IActionResult Index()
    //     {
    //         return NoContent();
    //     }

    //     [Route("LoginTest")]
    //     [HttpPost]
    //     public IActionResult Login(string username)
    //     {
    //         /*
    //         Console.WriteLine("Console");
    //         AuthenticationService authService = new AuthenticationService();
    //         try
    //         {
    //             bool service = authService.CheckUser(username, "pass");
    //             return Ok(service);
    //         }catch(Exception ex)
    //         {
    //             Console.WriteLine(ex.Message);
    //             return BadRequest(ex.Message);
    //         }
    //         */
    //         return Ok();

    //     }
