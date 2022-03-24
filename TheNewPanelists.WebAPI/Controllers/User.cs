namespace VueJsToNetCore.ViewModel
{
    public class User
    {
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
    }
}