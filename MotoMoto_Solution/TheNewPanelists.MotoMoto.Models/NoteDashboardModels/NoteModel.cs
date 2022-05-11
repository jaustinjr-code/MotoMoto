using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models.NoteDashboardModels
{
    public class NoteModel
    {
       // private int id; 
        public string username { get; set; }
        public string title { get; set; }
        public string notes { get; set; }

        //public DateTime timeStamp { get; set; }
        public string GetUsername()
        {
            return username;
        }

        public string GetTitle()
        {
            return title; 
        }
        
        public string GetNotes()
        {
            return notes;
        }

        public void SetUsername(string username)
        {
            this.username = username;
        }

        public void SetTitle(string title)
        {
            this.title = title;
        }

        public void SetNotes(string notes)
        {
            this.notes = notes;
        }
    }
}
