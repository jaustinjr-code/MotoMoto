using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.DataAccess;

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
        [HttpGet("AddNotes")]
        public IActionResult AddNotes(string username, string notes)
        {
            try
            {
                NoteDashboardDataAccess note = new NoteDashboardDataAccess();
                return Ok(note.AddNotes(username, notes));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
