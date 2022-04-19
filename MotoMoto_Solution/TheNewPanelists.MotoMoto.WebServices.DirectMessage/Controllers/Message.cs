using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.MotoMoto.WebServices.DirectMessage.Controllers
{
    public class Message
    {
        private int id;
        public string sender { get; set; }
        public string receiver { get; set; }
        public string message { get; set; }
        public void setSender(string sender)
        {
            this.sender = sender;  
        }

        public void setReceiver(string receiver)
        {
            this.receiver = receiver;
        }

        public void setMessage(string message)
        {
            this.message = message;
        }

        public string GetSender()
        {
            return sender;
        }
        public string GetReceiver()
        {
            return receiver;
        }
        public string GetMessage()
        {
            return message;
        }
    }
}
