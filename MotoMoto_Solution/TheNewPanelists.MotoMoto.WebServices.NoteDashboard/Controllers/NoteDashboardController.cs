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
        public IActionResult GetNotes(string username, string option)
        {
            string order = "";
            NoteDashboardDataAccess note = new NoteDashboardDataAccess();
            if (option == "none" || option == "By Date: Ascending Order")
            {
                order = "timeStamp ASC";
            }
            else if (option == "By Date: Descending Order")
            {
                order = "timeStamp DESC";
            }
            else if (option == "Alphabetical: Ascending Order")
            {
                order = "title ASC";
            }
            else if (option == "Alphabetical: Descending Order")
            {
                order = "title DESC";
            }
            else
            {
                order = "timeStamp ASC";
            }

            List < NoteModel > noteList = note.GetNotes(username, order);
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
