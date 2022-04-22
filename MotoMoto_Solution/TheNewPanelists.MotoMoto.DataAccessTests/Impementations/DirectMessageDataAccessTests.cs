using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheNewPanelists.MotoMoto.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataAccess.Tests
{
    [TestClass()]
    public class DirectMessageDataAccessTests
    {
        [TestMethod()]
        public void CreateNewDirectMessageTest()
        {
            string sender = "ran";
            string receiver = "user4";
            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            bool result = directMessageDataAccess.CreateNewDirectMessage(sender, receiver);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void SendMessageTest()
        {
            string sender = "ran";
            string receiver = "kchu";
            string message = "this is a test";
            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            bool result = directMessageDataAccess.SendMessage(sender, receiver, message);
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void GetMessageHistoryTest()
        {
            string user = "kchu";
            List<string> expected = new List<string>() { "ran", "user1", "user2"};
            List<string> actual = new List<string>();
            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            actual = directMessageDataAccess.GetMessageHistory(user);
            CollectionAssert.AreEqual(expected, actual, "GetMessageHistoryTest Error");

        }

        /*
        [TestMethod()]
        public void GetMessagesTest()
        {
            string sender = "kchu";
            string receiver = "user1";
            List<List<string>> actual = new List<List<string>>();
            List<List<string>> expected =  new List<List<string>>();
            List<string> messages = new List<string>() { "kchu", "message1", "4/19/2022 6:27:00 AM"};
            expected.Add(messages);
            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            actual = directMessageDataAccess.GetMessages(sender, receiver);
            System.Diagnostics.Debug.WriteLine("actual: " + actual.ElementAt(0).Count + actual.ElementAt(0).ElementAt(0) + actual.ElementAt(0).ElementAt(1) + actual.ElementAt(0).ElementAt(2));
            System.Diagnostics.Debug.WriteLine("actual: " + expected.ElementAt(0).Count + expected.ElementAt(0).ElementAt(0) + expected.ElementAt(0).ElementAt(1) + expected.ElementAt(0).ElementAt(2));   
            CollectionAssert.AreEqual(expected, actual, "GetMessageTest Error");
        }
        */
       
        [TestMethod()]
        public void GetRequestTest()
        {
            string user = "user1";
            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            List<string> actual = new List<string>();
            actual = directMessageDataAccess.GetRequest(user);
            List<string> expected = new List<string>();
            expected.Add("user2");
            expected.Add("user3");
            System.Diagnostics.Debug.WriteLine("actual request: " + actual.Count + actual.ElementAt(0) + " " + actual.ElementAt(1));
            System.Diagnostics.Debug.WriteLine("expected request: " + actual.Count + expected.ElementAt(0) + " " + expected.ElementAt(1));
            CollectionAssert.AreEqual(expected, actual, actual.ToString());
        }

        [TestMethod()]
        public void AcceptRequestTest()
        {
            string sender = "user3";
            string receiver = "user4";

            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            bool result = directMessageDataAccess.AcceptRequest(sender, receiver);

            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void DeclineRequestTest()
        {
            string sender = "user3";
            string receiver = "user2";

            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            bool result = directMessageDataAccess.DeclineRequest(sender, receiver);

            Assert.IsTrue(result);
        }
    }
}