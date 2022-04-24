using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.MotoMoto.WebServices.DirectMessage.Controllers
{
    public class MessageHistory 
    {
        private int id; 
        public string sender { get; set; }
        public string receiver { get; set; }
        public bool request { get; set; }

        public string GetSender()
        {
            return sender;
        }
        public string GetReceiver()
        {
            return receiver;
        }
        public bool GetRequest()
        {
            return request;
        }
    }
}
