using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models.NoteDashboardModels;

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
        public IActionResult GetNotes(string username)
        {
            NoteDashboardDataAccess note = new NoteDashboardDataAccess();
            List < NoteModel > noteList = note.GetNotes(username, "option");
            List<string> temp = new List<string>();
            List<List<string>> noteParse = new List<List<string>>();
            foreach(NoteModel item in noteList)
            {
                temp.Add(item.title);
                temp.Add(item.notes);
                noteParse.Add(temp);
                temp = new List<string>();
            }
            return Ok(noteParse);
        }

            
    }
}
