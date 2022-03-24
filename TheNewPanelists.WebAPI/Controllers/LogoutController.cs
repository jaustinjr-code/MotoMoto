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
        public async Task<IActionResult> Logout(String logout)
        {

            await HttpContext.SignOutAsync();
            return NoContent();
        }


    }
}
