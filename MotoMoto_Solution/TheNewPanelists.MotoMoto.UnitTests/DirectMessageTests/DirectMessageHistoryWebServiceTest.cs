using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.WebServices.DirectMessage.Controllers;
using Xunit;

namespace TheNewPanelists.MotoMoto.UnitTests.DirectMessageTests
{
    public class DirectMessageHistoryWebServiceTest
    {
        [Fact]
        public void GetMessageHistory()
        {
            string sender = "user9";

            DirectMessageHistoryController directMessageHistoryController = new DirectMessageHistoryController();

            var result = directMessageHistoryController.GetMessageHistory(sender);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
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
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

        }
    }
}
