using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    
    public class DirectMessageBusinessLayer
    {
        private DirectMessageServices directMessageServices = new DirectMessageServices();
        public bool CreateNewDirectMessage(string sender, string receiver)
        {
            //validate user
            //check if blocked
            return directMessageServices.CreateNewDirectMessage(sender, receiver);
        }

        public bool SendMessage(string sender, string receiver, string message)
        {
            //check if blocked
            return directMessageServices.SendMessage(sender, receiver, message);
        }

        public List<string> GetMessageHistory(string sender)
        {
            //check if blocked?
            return directMessageServices.GetMessageHistory(sender);
        }

        public List<List<string>> GetMessages(string sender, string receiver)
        {
            return directMessageServices.GetMessages(sender, receiver);
        }

        public List<string>GetRequest(string currentUser)
        {
            return directMessageServices.GetRequests(currentUser);
        }

        public bool AcceptRequest(string sender, string receiver)
        {
            return directMessageServices.AcceptRequest(sender, receiver);
        }

        public bool DeclineRequest(string sender, string receiver)
        {
            return directMessageServices.DeclineRequest(sender, receiver);
        }
    }
}
