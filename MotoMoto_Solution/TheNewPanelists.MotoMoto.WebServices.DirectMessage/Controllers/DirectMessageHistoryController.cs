using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
namespace TheNewPanelists.MotoMoto.WebServices.DirectMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectMessageHistoryController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetMessageHistory(string sender)
        {
            try
            {
                DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
                List<string> users = new List<string>();
                users = directMessageBusinessLayer.GetMessageHistory(sender);
                return Ok(users);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);

            }
        }

        [HttpPut]
        public IActionResult CreateNewDirectMessage(string sender, string receiver)
        {
            try
            {
                DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
                return Ok(directMessageBusinessLayer.CreateNewDirectMessage(sender, receiver));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
