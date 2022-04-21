using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.WebServices.DirectMessage.Controllers;

namespace TheNewPanelists.MotoMoto.WebServices.Tests
{
    [TestClass()]
    public class DirectMessageHistoryWebServiceTests
    {

        [TestMethod()]
        public void GetMessageHistory()
        {
            string sender = "user9";

            DirectMessageHistoryController directMessageHistoryController = new DirectMessageHistoryController();

            var result = directMessageHistoryController.GetMessageHistory(sender);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod()]
        public void CreateNewDirectMessage()
        {
            string sender = "user13";
            string receiver = "user14";
            MessageHistory messageHistory = new MessageHistory();
            messageHistory.sender = sender;
            messageHistory.receiver = receiver;


            DirectMessageHistoryController directMessageHistoryController = new DirectMessageHistoryController();

            var result = directMessageHistoryController.CreateNewDirectMessage(messageHistory);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }
    }
}