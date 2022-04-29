using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;
using Xunit;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class DirectMessageDataAccessUnitTest
    {
        [Fact]
        public void CreateNewDirectMessageTest()
        {
            string sender = "ran";
            string receiver = "user4";
            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            bool result = directMessageDataAccess.CreateNewDirectMessage(sender, receiver);

            Assert.True(result);
        }

        [Fact]
        public void SendMessageTest()
        {
            string sender = "ran";
            string receiver = "kchu";
            string message = "this is a test";
            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            bool result = directMessageDataAccess.SendMessage(sender, receiver, message);
            Assert.True(result);
        }

        [Fact]
        public void GetMessageHistoryTest()
        {
            string user = "kchu";
            List<string> expected = new List<string>() { "ran", "user1", "user2" };
            List<string> actual = new List<string>();
            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            actual = directMessageDataAccess.GetMessageHistory(user);
            Assert.Equal(expected, actual);

        }


        [Fact()]
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
            Assert.Equal(expected, actual);
        }

        [Fact()]
        public void AcceptRequestTest()
        {
            string sender = "user3";
            string receiver = "user4";

            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            bool result = directMessageDataAccess.AcceptRequest(sender, receiver);

            Assert.True(result);
        }

        [Fact()]
        public void DeclineRequestTest()
        {
            string sender = "user3";
            string receiver = "user2";

            DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
            bool result = directMessageDataAccess.DeclineRequest(sender, receiver);

            Assert.True(result);
        }
    }
}
