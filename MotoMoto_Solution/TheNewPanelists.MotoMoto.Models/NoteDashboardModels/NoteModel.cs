using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models.NoteDashboardModels
{
    public class NoteModel
    {
        private int id; 
        public string title { get; set; }
        public string notes { get; set; }

        public DateTime timeStamp { get; set; }
        public NoteModel(string title, string notes, DateTime timestamp)
        {
            this.title = title;
            this.notes = notes;
            this.timeStamp = timestamp;
        }
    }
}
