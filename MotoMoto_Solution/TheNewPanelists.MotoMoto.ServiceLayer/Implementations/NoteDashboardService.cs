using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models.NoteDashboardModels;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class NoteDashboardService
    {
        public List<NoteModel> GetNotes(string username, string order)
        {
            NoteDashboardDataAccess dataAccess = new NoteDashboardDataAccess();
            return dataAccess.GetNotes(username, order);
        }
        public bool AddNotes(NoteModel model)
        {
            NoteDashboardDataAccess dataAccess = new NoteDashboardDataAccess();
            return dataAccess.AddNotes(model);
        }

        public bool DeleteNotes(NoteModel model)
        {
            NoteDashboardDataAccess dataAccess = new NoteDashboardDataAccess();
            return dataAccess.DeleteNotes(model);
        }

        public bool UpdateNotes(NoteModel model)
        {
            NoteDashboardDataAccess dataAccess = new NoteDashboardDataAccess();
            return dataAccess.UpdateNotes(model);
        }
    }
}
