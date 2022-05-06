using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models.NoteDashboardModels;
using TheNewPanelists.MotoMoto.ServiceLayer;
namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class NoteDashboardManager
    {
        public List<NoteModel>GetNotes(string username, string order)
        {
            NoteDashboardService noteDashboard = new NoteDashboardService();
            return noteDashboard.GetNotes(username, order);
        }

        public bool AddNotes(string username, string title)
        {
            NoteDashboardService noteDashboard = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername(username);
            model.SetTitle(title);
            model.SetNotes("");
            return noteDashboard.AddNotes(model);
        }

        public bool DeleteNotes(string username, string title)
        {
            NoteDashboardService notesDashboard = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername(username);
            model.SetTitle(title);
            model.SetNotes("");
            return notesDashboard.DeleteNotes(model);
        }

        public bool UpdateNotes(string username, string title, string notes)
        {
            NoteDashboardService notesDashboard = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername(username);
            model.SetTitle(title);
            model.SetNotes(notes);
            return notesDashboard.UpdateNotes(model);

        }

    }
}
