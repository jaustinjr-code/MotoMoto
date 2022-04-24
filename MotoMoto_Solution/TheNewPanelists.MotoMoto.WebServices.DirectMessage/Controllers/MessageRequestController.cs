using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
namespace TheNewPanelists.MotoMoto.WebServices.DirectMessage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageRequestController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("GetRequest")]
        public IActionResult GetRequests(string currentUser)
        {
            try
            {
                DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
                return Ok(directMessageBusinessLayer.GetRequest(currentUser));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("AcceptRequest")]
        public IActionResult AcceptRequest(MessageHistory messageHistory)
        {
            string receiver = messageHistory.GetReceiver();
            string sender = messageHistory.GetSender();
            try
            {
                DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
                return Ok(directMessageBusinessLayer.AcceptRequest(sender, receiver));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeclineRequest")]
        public IActionResult DeclineRequest(string sender, string receiver)
        {
            try
            {
                DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
                return Ok(directMessageBusinessLayer.DeclineRequest(sender, receiver));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
