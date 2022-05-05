using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.WebServices;
using TheNewPanelists.MotoMoto.Models.NoteDashboardModels;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.WebServices.NoteDashboard.Controllers;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class NoteDashboardWebServicesUnitTestcs
    {
        [Fact]
        public void AddNotes_True()
        {
            NoteController unitTest = new NoteController();
            string username = "user2";
            string title = "Note for Business Testing";
            IActionResult response = unitTest.AddNotes(username, title);
            OkObjectResult OKResponse = response as OkObjectResult;
            Assert.NotNull(OKResponse);
            Assert.Equal(true, OKResponse.Value);
            NoteDeleteController delete = new NoteDeleteController();
            IActionResult temp = delete.DeleteNotes(username, title);
        }

        [Fact]
        public void AddNotes_False()
        {
            NoteController unitTest = new NoteController();
            string username = "NoteAUser";
            string title = "Note for Controller Testing";
            IActionResult response = unitTest.AddNotes(username, title);
            OkObjectResult OKResponse = response as OkObjectResult;
            Assert.NotNull(OKResponse);
            Assert.Equal(false, OKResponse.Value);
        }
        [Fact]
        public void DeleteNotes_True()
        {
            NoteController addController = new NoteController();
            NoteDeleteController unitTest = new NoteDeleteController();
            string username = "user2";
            string title = "Note for Delete Function In Controller Testing";
            IActionResult add= addController.AddNotes(username, title);
            IActionResult response = unitTest.DeleteNotes(username, title);
            OkObjectResult OKResponse = response as OkObjectResult;
            Assert.NotNull(OKResponse);
            Assert.Equal(true, OKResponse.Value);
        }
        [Fact]
        public void DeleteNotes_False()
        {
            NoteDeleteController unitTest = new NoteDeleteController();
            string username = "user300";
            string title = "Unit Test Note Service layer";
            IActionResult response = unitTest.DeleteNotes(username, title);
            OkObjectResult OKResponse = response as OkObjectResult;
            Assert.NotNull(OKResponse);
            Assert.Equal(false, OKResponse.Value);
        }

        [Fact]
        public void UpdateNote_True()
        {
            NoteUpdateController unitTest = new NoteUpdateController();
            NoteController addController = new NoteController();
            string username = "user2";
            string title = "Update Note Controller Layer";
            IActionResult add = addController.AddNotes(username, title);
            string note = "Note is updated";
            IActionResult response = unitTest.UpdateNotes(username, title, note);
            OkObjectResult OKResponse = response as OkObjectResult;
            Assert.NotNull(OKResponse);
            Assert.Equal(true, OKResponse.Value);
        }

        [Fact]
        public void UpdateNote_False()
        {
            NoteUpdateController unitTest = new NoteUpdateController();
            string username = "";
            string title = "";
            string note = "empty";
            IActionResult response = unitTest.UpdateNotes(username, title, note);
            OkObjectResult OKResponse = response as OkObjectResult;
            Assert.NotNull(OKResponse);
            Assert.Equal(false, OKResponse.Value);
        }
    }
}
