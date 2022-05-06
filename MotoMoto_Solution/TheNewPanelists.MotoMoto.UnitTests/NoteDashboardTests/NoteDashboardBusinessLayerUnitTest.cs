using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.Models.NoteDashboardModels;
using Xunit;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class NoteDashboardBusinessLayerUnitTest
    {
        [Fact]
        public void AddNotes_True()
        {
            NoteDashboardManager unitTest = new NoteDashboardManager();
            string username = "user2";
            string title = "Note for Business Testing";
            bool actual = unitTest.AddNotes(username, title);
            Assert.True(actual);
            unitTest.DeleteNotes(username, title);
        }

        [Fact]
        public void AddNotes_False()
        {
            NoteDashboardManager unitTest = new NoteDashboardManager();
            string username = "NoteAUser";
            string title = "Note for Business Testing";
            bool actual = unitTest.AddNotes(username, title);
            Assert.False(actual);
        }
        [Fact]
        public void DeleteNotes_True()
        {
            NoteDashboardManager unitTest = new NoteDashboardManager();
            string username = "user2";
            string title = "Note for Delete Function In Business Testing";
            unitTest.AddNotes(username, title);
            bool actual = unitTest.DeleteNotes(username, title);
            Assert.True(actual);
        }
        [Fact]
        public void DeleteNotes_False()
        {
            NoteDashboardManager unitTest = new NoteDashboardManager();
            string username = "user300";
            string title = "Unit Test Note Service layer";
            bool actual = unitTest.DeleteNotes(username, title);
            Assert.False(actual);
        }

        [Fact]
        public void UpdateNote_True()
        {
            NoteDashboardManager unitTest = new NoteDashboardManager();
            string username = "user2";
            string title = "Update Note Business Layer";
            unitTest.AddNotes(username, title);
            string note = "Note is updated";
            bool actual = unitTest.UpdateNotes(username, title, note);
            Assert.True(actual);
        }

        [Fact]
        public void UpdateNote_False()
        {
            NoteDashboardManager unitTest = new NoteDashboardManager();
            string username = "";
            string title = "";
            string note = "empty";
            bool actual = unitTest.UpdateNotes(username, title, note);
            Assert.False(actual);
        }

        [Fact]
        public void GetNote_True()
        {
            NoteDashboardManager unitTest = new NoteDashboardManager();
            unitTest.AddNotes("user2", "Get Note Businesslayer");
            List<NoteModel> list = unitTest.GetNotes("user2", "timeStamp ASC");
            bool actual = false;
            foreach (NoteModel note in list)
            {
                if (note.GetTitle().Equals("Get Note Businesslayer") && note.GetUsername().Equals("user2"))
                {
                    actual = true;
                }
            }
            unitTest.DeleteNotes("user2", "Get Note Businesslayer");
            Assert.True(actual);
        }

        [Fact]
        public void GetNote_False()
        {
            NoteDashboardManager unitTest = new NoteDashboardManager();
            List<NoteModel> list = unitTest.GetNotes("user2", "timeStamp ASC");
            bool actual = false;
            foreach (NoteModel note in list)
            {
                if (note.GetTitle().Equals("Not a Title") && note.GetUsername().Equals("Not a User"))
                {
                    actual = true;
                }
            }
            Assert.False(actual);
        }

    }
}
