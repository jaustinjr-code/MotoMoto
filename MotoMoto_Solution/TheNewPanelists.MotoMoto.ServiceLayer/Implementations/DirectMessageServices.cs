using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class DirectMessageServices
    {
        private DirectMessageDataAccess directMessageDataAccess = new DirectMessageDataAccess();
        public bool CreateNewDirectMessage(string sender, string receiver)
        {
            return directMessageDataAccess.CreateNewDirectMessage(sender, receiver);
        }

        public bool SendMessage(string sender, string receiver, string message)
        {
            return directMessageDataAccess.SendMessage(sender, receiver, message);
        }

        public List<string> GetMessageHistory(string sender)
        {
            return directMessageDataAccess.GetMessageHistory(sender);
        }

        public List<List<string>> GetMessages(string sender, string receiver)
        {
            return directMessageDataAccess.GetMessages(sender, receiver);
        }

    }
}
