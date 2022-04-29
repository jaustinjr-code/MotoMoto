using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheNewPanelists.MotoMoto.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.ServiceLayer.Tests
{
    [TestClass()]
    public class DirectMessageServiceLayerTests
    {
        [TestMethod()]
        public void CreateNewDirectMessageTest()
        {
            string sender = "user5";
            string receiver = "user6";
            DirectMessageServices directMessageServiceLayer = new DirectMessageServices();
            bool result = directMessageServiceLayer.CreateNewDirectMessage(sender, receiver);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SendMessageTest()
        {
            string sender = "user5";
            string receiver = "user7";
            string message = "this is a test";
            DirectMessageServices directMessageServiceLayer = new DirectMessageServices();
            bool result = directMessageServiceLayer.SendMessage(sender, receiver, message);
            Assert.IsTrue(result);

        }

        [TestMethod()]
        public void GetMessageHistoryTest()
        {
            string user = "user5";
            List<string> expected = new List<string>() { "user7"};
            List<string> actual = new List<string>();
            DirectMessageServices directMessageServiceLayer = new DirectMessageServices();
            actual = directMessageServiceLayer.GetMessageHistory(user);
            CollectionAssert.AreEqual(expected, actual, "GetMessageHistoryTest Error");
        }


        [TestMethod()]
        public void GetRequestTest()
        {
            string user = "user7";
            DirectMessageServices directMessageServiceLayer = new DirectMessageServices();
            List<string> actual = new List<string>();
            actual = directMessageServiceLayer.GetRequests(user);
            List<string> expected = new List<string>();
            expected.Add("user6");
            CollectionAssert.AreEqual(expected, actual, actual.ToString());
        }

        [TestMethod()]
        public void AcceptRequestTest()
        {
            string sender = "user7";
            string receiver = "user8";

            DirectMessageServices directMessageServiceLayer = new DirectMessageServices();
            bool result = directMessageServiceLayer.AcceptRequest(sender, receiver);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeclineRequestTest()
        {
            string sender = "user8";
            string receiver = "user5";

            DirectMessageServices directMessageServiceLayer = new DirectMessageServices();
            bool result = directMessageServiceLayer.DeclineRequest(sender, receiver);

            Assert.IsTrue(result);
        }
    }
}