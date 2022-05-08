using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.NoteDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteUpdateController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  Web API call to get the updated notes from the front end and store it into the backend database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="title"></param>
        /// <param name="notes"></param>
        /// <returns>IAction Result</returns>
        [HttpGet]
        [Route("UpdateNote")]
        public IActionResult UpdateNotes(string username, string title, string notes)
        {
            NoteDashboardManager note = new NoteDashboardManager();
            return Ok(note.UpdateNotes(username, title, notes));
        }
    }
}
