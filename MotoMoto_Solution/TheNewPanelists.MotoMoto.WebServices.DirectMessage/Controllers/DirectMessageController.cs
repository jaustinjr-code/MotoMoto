using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.BusinessLayer;

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

        [HttpGet]
        [Route("GetMessage")]
        public IActionResult GetMessages(string sender, string receiver)
        {
            try
            {
                DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
                return Ok(directMessageBusinessLayer.GetMessages(sender, receiver));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

       

        [HttpPut]
        public IActionResult SendMessage(string sender, string receiver, string message)
        {
            try
            {
                DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
                return Ok(directMessageBusinessLayer.SendMessage(sender, receiver, message));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }




    }
}
