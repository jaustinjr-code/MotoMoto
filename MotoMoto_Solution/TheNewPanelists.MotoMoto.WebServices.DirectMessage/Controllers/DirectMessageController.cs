using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.DataAccess;

namespace TheNewPanelists.MotoMoto.WebServices.DirectMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectMessageController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult validUser(string sender, string reciever)
        {
            DirectMessageDataAccess dataAccess = new DirectMessageDataAccess();
            List<List<string>> userData = dataAccess.GetMessages(sender, reciever);
            Console.WriteLine(userData.ToString());
            return Ok();

        }

        
    }
}
