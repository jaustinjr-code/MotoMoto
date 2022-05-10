using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models.NoteDashboardModels;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    /// <summary>
    /// The service layer for NoteDashabord
    /// </summary>
    public class NoteDashboardService
    {
        /// <summary>
        /// Creates a NoteDashboardDataAccess object and calls the GetNotes method
        /// </summary
        /// <param name="username"></param>
        /// <param name="order"></param>
        /// <returns>List<NoteModel></returns>
        public List<NoteModel> GetNotes(string username, string order)
        {
            NoteDashboardDataAccess dataAccess = new NoteDashboardDataAccess();
            return dataAccess.GetNotes(username, order);
        }
        /// <summary>
        /// Creates a NoteDashboardDataAccess object and calls the AddNotes method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddNotes(NoteModel model)
        {
            NoteDashboardDataAccess dataAccess = new NoteDashboardDataAccess();
            return dataAccess.AddNotes(model);
        }
        /// <summary>
        /// Creates a NoteDashboardDataAccess object and calls the DeleteNotes method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteNotes(NoteModel model)
        {
            NoteDashboardDataAccess dataAccess = new NoteDashboardDataAccess();
            return dataAccess.DeleteNotes(model);
        }

        /// <summary>
        /// Creates a NoteDashboardDataAccess object and calls the UpdateNotes method
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateNotes(NoteModel model)
        {
            NoteDashboardDataAccess dataAccess = new NoteDashboardDataAccess();
            return dataAccess.UpdateNotes(model);
        }
    }
}
