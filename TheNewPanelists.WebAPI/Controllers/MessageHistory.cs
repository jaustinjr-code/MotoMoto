namespace VueJsToNetCore.ViewModel
{
    public class MessageHistory
    {
        private int messageHistoryID;

        private int senderID;
        private int recieverID; 
 

        public int getSenderID()
        {
            return senderID;
        }

        public int getRecieverID()
        {
            return recieverID;
        }

        public int getMessageHistoryID()
        {
            return messageHistoryID;
        }

        public void setSenderID(int senderID)
        {
            this.senderID = senderID;
        }

        public void setRecieverID(int recieverID)
        {
            this.recieverID = recieverID;  
        }

    

    }
}