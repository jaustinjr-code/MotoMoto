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
    public class MessageRequestWebServiceTests
    {

        [TestMethod()]
        public void GetRequest()
        {
            string currentUser = "user15";

            MessageRequestController messageRequestController = new MessageRequestController();

            var result = messageRequestController.GetRequests(currentUser);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod()]
        public void DeclineRequest()
        {
            string sender = "user16";
            string receiver = "user13";

            MessageRequestController messageRequestController = new MessageRequestController();

            var result = messageRequestController.DeclineRequest(sender, receiver);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod()]
        public void AcceptRequest()
        {
            string sender = "user15";
            string receiver = "user16";
            MessageHistory messageHistory = new MessageHistory();
            messageHistory.sender = sender;
            messageHistory.receiver = receiver;


            MessageRequestController directMessageRequestController = new MessageRequestController();

            var result = directMessageRequestController.AcceptRequest(messageHistory);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }
    }
}