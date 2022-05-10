namespace TheNewPanelists.MotoMoto.Models
{
    public class BaseUser
    {
        public string username { get; }
        public BaseUser(string username)
        {
            this.username = username;
        }
    }
}