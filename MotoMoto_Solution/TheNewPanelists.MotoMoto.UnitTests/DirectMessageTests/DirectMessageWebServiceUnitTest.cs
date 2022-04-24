using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.WebServices.DirectMessage.Controllers;
using Xunit;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class DirectMessageWebServiceUnitTest
    {
        [Fact]
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
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

        }

    }
}
