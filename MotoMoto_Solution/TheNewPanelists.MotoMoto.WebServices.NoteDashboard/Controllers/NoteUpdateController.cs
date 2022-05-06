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
        [HttpGet]
        [Route("UpdateNote")]
        public IActionResult UpdateNotes(string username, string title, string notes)
        {
            NoteDashboardManager note = new NoteDashboardManager();
            return Ok(note.UpdateNotes(username, title, notes));
        }
    }
}
