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
    public class DirectMessageWebServiceTests
    {

        [TestMethod()]
        public void SendMessages()
        {
            string sender = "user9";
            string receiver = "user11";
            string message = "this is a test for web services";
            Message myMessage = new Message();
            myMessage.setMessage(message);
            myMessage.setSender(sender);
            myMessage.setReceiver(receiver);


            DirectMessageController directMessageController = new DirectMessageController();
            
            var result = directMessageController.SendMessage(myMessage);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

        }

    }
}