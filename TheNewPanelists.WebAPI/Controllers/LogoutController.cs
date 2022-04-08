using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.WebAPI.Controllers
{
    public class LogoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet(Name = "Logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                Response.Cookies.Delete("UserId");
                return Ok(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
            
        }


    }
}
