using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models.NoteDashboardModels;
using Xunit;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class NoteDashboardDataAccessUnitTest
    {

        /// <summary>
        /// Checks to ensure that addNotes notes is working in the DataAccess layer
        /// Should return true
        /// </summary>

        [Fact]
        public void AddNotes_True()
        {
            NoteDashboardDataAccess unitTest = new NoteDashboardDataAccess();
            NoteModel model = new NoteModel();
            model.SetUsername("user2");
            model.SetNotes("This Note is for the unit Test");
            model.SetTitle("Unit Test Note");
            bool actual = unitTest.AddNotes(model);
            Assert.True(actual);    
            unitTest.DeleteNotes(model);
        }
        /// <summary>
        /// Checks to ensure that addNotes notes is working in the DataAccess layer
        /// Should return false
        /// </summary>

        [Fact]
        public void AddNotes_False()
        {
            NoteDashboardDataAccess unitTest = new NoteDashboardDataAccess();
            NoteModel model = new NoteModel();
            model.SetUsername("");
            model.SetNotes("This Note is for the unit Test: should fail");
            model.SetTitle("FailedUnit Test Note");
            bool actual = unitTest.AddNotes(model);
            Assert.False(actual);  
        }

        /// <summary>
        /// Checks to ensure that deleteNotes notes is working in the DataAccess layer
        /// Should return true
        /// </summary>
        [Fact]
        public void DeleteNotes_True()
        {
            NoteDashboardDataAccess unitTest = new NoteDashboardDataAccess();
            NoteModel model = new NoteModel();
            model.SetUsername("user2");
            model.SetTitle("Unit Test Note Delete");
            model.SetNotes("Should be deleted");
            unitTest.AddNotes(model);
            NoteModel model2 = new NoteModel();
            model2.SetUsername("user2");
            model2.SetTitle("Unit Test Note Delete");
            bool actual = unitTest.DeleteNotes(model2);
            Assert.True(actual);
        }

        /// <summary>
        /// Checks to ensure that deleteNotes notes is working in the DataAccess layer
        /// Should return false
        /// </summary>
        [Fact]
        public void DeleteNotes_False()
        {
            NoteDashboardDataAccess unitTest = new NoteDashboardDataAccess();
            NoteModel model = new NoteModel();
            model.SetUsername("user300");
            model.SetTitle("Unit Test Note");
            bool actual = unitTest.DeleteNotes(model);
            Assert.False(actual);
        }

        /// <summary>
        /// Checks to ensure that updateNotes notes is working in the DataAccess layer
        /// Should return true
        /// </summary>
        [Fact]
        public void UpdateNote_True()
        {
            NoteDashboardDataAccess unitTest = new NoteDashboardDataAccess();
            NoteModel model = new NoteModel();
            model.SetUsername("user2");
            model.SetTitle("Unit Test Note Update");
            model.SetNotes("Should be updated");
            unitTest.AddNotes(model);
            NoteModel model2 = new NoteModel();
            model2.SetUsername("user2");
            model2.SetTitle("Unit Test Note Update");
            model2.SetNotes("Is Updated");
            bool actual = unitTest.UpdateNotes(model2);
            Assert.True(actual);
        }
        /// <summary>
        /// Checks to ensure that updateNotes notes is working in the Data Access layer
        /// Should return false
        /// </summary>
        [Fact]
        public void UpdateNote_False()
        {
            NoteDashboardDataAccess unitTest = new NoteDashboardDataAccess();
            NoteModel model = new NoteModel();
            model.SetUsername("user300");
            model.SetTitle("Unit Test Note");
            bool actual = unitTest.UpdateNotes(model);
            Assert.False(actual);
        }

        /// <summary>
        /// Checks to ensure that getNotes notes is working in the Data Access layer
        /// Should return true
        /// </summary>
        [Fact]
        public void GetNote_True()
        {
            NoteDashboardDataAccess unitTest = new NoteDashboardDataAccess();
            NoteModel model = new NoteModel();
            model.SetUsername("user2");
            model.SetTitle("Unit Test GetNote");
            model.SetNotes("Test Get Note");
            unitTest.AddNotes(model);
            List<NoteModel> list = unitTest.GetNotes("user2", "timeStamp ASC");
            bool actual = false;
            foreach(NoteModel note in list)
            {
                if(note.GetTitle().Equals(model.GetTitle()) && note.GetUsername().Equals(model.GetUsername()))
                {
                    actual = true;
                }
            }
            unitTest.DeleteNotes(model);
            Assert.True(actual);
        }

        /// <summary>
        /// Checks to ensure that getNotes notes is working in the Data Access layer
        /// Should return false
        /// </summary>
        [Fact]
        public void GetNote_False()
        {
            NoteDashboardDataAccess unitTest = new NoteDashboardDataAccess();
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
