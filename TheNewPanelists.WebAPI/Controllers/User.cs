namespace VueJsToNetCore.ViewModel
{
    public class User
    {
        private int id;
        public string Username { get; set; }
        public string Password { get; set; }

        public string getUsername()
        {
            return Username;
        }
        public string getPassword()
        {
                return Password;
        }
        public int getID()
        {
            return id; 
        }
    }
}