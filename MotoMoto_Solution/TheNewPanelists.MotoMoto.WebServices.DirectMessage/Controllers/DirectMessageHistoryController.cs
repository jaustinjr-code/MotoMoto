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
        [Route("GetMessageHistory")]
        public IActionResult GetMessageHistory(string ? sender)
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
        [Route("CreateNewChat")]
        public IActionResult CreateNewDirectMessage(MessageHistory messageHistory)
        {
            string sender = messageHistory.GetSender();
            string receiver = messageHistory.GetReceiver();
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
