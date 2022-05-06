using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models.NoteDashboardModels;
using Xunit;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class NoteDashboardServiceLayerUnitTest
    {
        [Fact]
        public void AddNotes_True()
        {
            NoteDashboardService unitTest = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername("user2");
            model.SetNotes("This Note is for the unit Test Service layer");
            model.SetTitle("Unit Test Note Service layer");
            bool actual = unitTest.AddNotes(model);
            Assert.True(actual);
            unitTest.DeleteNotes(model);
        }

        [Fact]
        public void AddNotes_False()
        {
            NoteDashboardService unitTest = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername("");
            model.SetNotes("This Note is for the unit Test: should fail");
            model.SetTitle("Failed Unit Test Note Service layer");
            bool actual = unitTest.AddNotes(model);
            Assert.False(actual);
        }
        [Fact]
        public void DeleteNotes_True()
        {
            NoteDashboardService unitTest = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername("user2");
            model.SetTitle("Unit Test Note Delete Service layer");
            model.SetNotes("Should be deleted");
            unitTest.AddNotes(model);
            NoteModel model2 = new NoteModel();
            model2.SetUsername("user2");
            model2.SetTitle("Unit Test Note Delete Service layer");
            bool actual = unitTest.DeleteNotes(model2);
            Assert.True(actual);
        }
        [Fact]
        public void DeleteNotes_False()
        {
            NoteDashboardService unitTest = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername("user300");
            model.SetTitle("Unit Test Note Service layer");
            bool actual = unitTest.DeleteNotes(model);
            Assert.False(actual);
        }

        [Fact]
        public void UpdateNote_True()
        {
            NoteDashboardService unitTest = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername("user2");
            model.SetTitle("Unit Test Note Update Service layer");
            model.SetNotes("Should be updated");
            unitTest.AddNotes(model);
            NoteModel model2 = new NoteModel();
            model2.SetUsername("user2");
            model2.SetTitle("Unit Test Note Update Service layer");
            model2.SetNotes("Is Updated");
            bool actual = unitTest.UpdateNotes(model2);
            Assert.True(actual);
        }

        [Fact]
        public void UpdateNote_False()
        {
            NoteDashboardService unitTest = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername("user300");
            model.SetTitle("Unit Test Note Service layer");
            bool actual = unitTest.UpdateNotes(model);
            Assert.False(actual);
        }

        [Fact]
        public void GetNote_True()
        {
            NoteDashboardService unitTest = new NoteDashboardService();
            NoteModel model = new NoteModel();
            model.SetUsername("user2");
            model.SetTitle("Unit Test GetNote Service layer");
            model.SetNotes("Test Get Note");
            unitTest.AddNotes(model);
            List<NoteModel> list = unitTest.GetNotes("user2", "timeStamp ASC");
            bool actual = false;
            foreach (NoteModel note in list)
            {
                if (note.GetTitle().Equals(model.GetTitle()) && note.GetUsername().Equals(model.GetUsername()))
                {
                    actual = true;
                }
            }
            unitTest.DeleteNotes(model);
            Assert.True(actual);
        }

        [Fact]
        public void GetNote_False()
        {
            NoteDashboardService unitTest = new NoteDashboardService();
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
