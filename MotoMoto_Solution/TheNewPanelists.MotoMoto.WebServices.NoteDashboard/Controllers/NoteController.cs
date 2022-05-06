using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class NoteController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Web API call to get the notes from the front end and send it back to the datastore where it will be stored
        /// </summary>
        /// <param name="username"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        [HttpGet("AddNotes")]
        public IActionResult AddNotes(string username, string notes)
        {
            try
            {
                NoteDashboardManager manager = new NoteDashboardManager();
                return Ok(manager.AddNotes(username, notes));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
