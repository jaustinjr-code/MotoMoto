using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.BusinessLayer;
using Xunit;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class DirectMessageBusinessLayerUnitTest
    {
        [Fact]
        public void CreateNewDirectMessageTest()
        {
            string sender = "user9";
            string receiver = "user10";
            DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
            bool result = directMessageBusinessLayer.CreateNewDirectMessage(sender, receiver);

            Assert.True(result);
        }

        [Fact]
        public void SendMessageTest()
        {
            string sender = "user9";
            string receiver = "user11";
            string message = "this is a test";
            DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
            bool result = directMessageBusinessLayer.SendMessage(sender, receiver, message);
            Assert.True(result);

        }

        [Fact]
        public void GetMessageHistoryTest()
        {
            string user = "user9";
            List<string> expected = new List<string>() { "user11" };
            List<string> actual = new List<string>();
            DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
            actual = directMessageBusinessLayer.GetMessageHistory(user);
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void GetRequestTest()
        {
            string user = "user11";
            DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
            List<string> actual = new List<string>();
            actual = directMessageBusinessLayer.GetRequest(user);
            List<string> expected = new List<string>();
            expected.Add("user10");
            System.Diagnostics.Debug.WriteLine("actual : " + actual.Count);
            System.Diagnostics.Debug.WriteLine(" request: " + expected.Count);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AcceptRequestTest()
        {
            string sender = "user11";
            string receiver = "user12";

            DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
            bool result = directMessageBusinessLayer.AcceptRequest(sender, receiver);

            Assert.True(result);
        }

        [Fact]
        public void DeclineRequestTest()
        {
            string sender = "user12";
            string receiver = "user9";

            DirectMessageBusinessLayer directMessageBusinessLayer = new DirectMessageBusinessLayer();
            bool result = directMessageBusinessLayer.DeclineRequest(sender, receiver);

            Assert.True(result);
        }
    }
}
