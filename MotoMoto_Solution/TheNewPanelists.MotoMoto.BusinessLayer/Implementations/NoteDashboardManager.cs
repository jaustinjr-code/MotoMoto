using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models.NoteDashboardModels;
using TheNewPanelists.MotoMoto.ServiceLayer;
namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    /// <summary>
    /// The business layer for note dashabord
    /// </summary>
    public class NoteDashboardManager
    {
        /// <summary>
        /// Creates the NoteDashboardService object and calls the GetNotes method
        /// </summary>
        /// <param name="username"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public List<NoteModel>GetNotes(string username, string order)
        {
            NoteDashboardService noteDashboard = new NoteDashboardService();
            return noteDashboard.GetNotes(username, order);
        }

        /// <summary>
        /// Creates the NoteDashboardService object and calls the AddNotes method
        /// Creates and set a NoteModel object to pass into the NoteDashboardService Object
        /// </summary>
        /// <param name="username"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool AddNotes(string username, string title)
        {
            NoteDashboardService noteDashboard = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername(username);
            model.SetTitle(title);
            model.SetNotes("");
            return noteDashboard.AddNotes(model);
        }

        /// <summary>
        /// Creates the NoteDashboardService object and calls the DeleteNotes method
        /// Creates and set a NoteModel object to pass into the NoteDashboardService Object
        /// </summary>
        /// <param name="username"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool DeleteNotes(string username, string title)
        {
            NoteDashboardService notesDashboard = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername(username);
            model.SetTitle(title);
            model.SetNotes("");
            return notesDashboard.DeleteNotes(model);
        }

        /// <summary>
        /// Creates the NoteDashboardService object and calls the UpdateNotes method
        /// Creates and set a NoteModel object to pass into the NoteDashboardService Object
        /// </summary>
        /// <param name="username"></param>
        /// <param name="title"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
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
