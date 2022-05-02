using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.DataAccess;

namespace TheNewPanelists.MotoMoto.WebServices
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteDashboardController : Controller
    {
        [HttpOptions]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("GetNotes")]
        public IActionResult GetNotes()
        {
            NoteDashboardDataAccess note = new NoteDashboardDataAccess();
            return Ok(note.GetNotes("user1", "order"));
        }

        [HttpPut]
        [Route("InsertNewNote")]
        public IActionResult  AddNotes()
        {
            NoteDashboardDataAccess note = new NoteDashboardDataAccess();
            return Ok(note.AddNotes("user1", "Test Adding New Note"));
        }

        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult DeleteNotes()
        {
            NoteDashboardDataAccess note = new NoteDashboardDataAccess();
            return Ok(note.DeleteNotes("user1", "Delete Note"));
        }

        [HttpPost]
        [Route("UpdateNote")]
        public IActionResult UpdateNotes()
        {
            NoteDashboardDataAccess note = new NoteDashboardDataAccess();
            return Ok(note.UpdateNotes("user1", "Update Note", "This Note Has been updated"));
        }
       
    }
}
