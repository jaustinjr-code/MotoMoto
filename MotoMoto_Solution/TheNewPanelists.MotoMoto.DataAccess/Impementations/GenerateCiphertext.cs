using System.Security.Cryptography;
using System.Text;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class GenerateCiphertext
    {
        public string? OriginalString { get; set; }
        public string? HashedString { get; set; }
        public string? Salt { get; set; }

        public GenerateCiphertext() { }
        /// <summary>
        /// 
        /// </summary>
        private void GenerateSalt()
        {
            RandomNumberGenerator random = RandomNumberGenerator.Create();
            int maxLength = 32;
            byte[] salt = new byte[maxLength];
            random.GetBytes(salt);

            Salt = Convert.ToBase64String(salt);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="email"></param>
        public void GenerateUsernameHash(string username)
        {
            OriginalString = $"{username}";
            StringBuilder stringBuilder = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoder = Encoding.UTF8;
                byte[] resultant = hash.ComputeHash(encoder.GetBytes(OriginalString!));

                foreach (byte b in resultant)
                    stringBuilder.Append(b.ToString("x2"));
            }
            HashedString = stringBuilder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        public void GeneratePasswordHash(string password)
        {
            GenerateSalt();
            StringBuilder stringBuilder = new StringBuilder();
            string saltAndPwd = string.Concat(password, Salt);

            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoder = Encoding.UTF8;
                byte[] resultant = hash.ComputeHash(encoder.GetBytes(saltAndPwd));

                foreach (byte b in resultant)
                    stringBuilder.Append(b.ToString("x2"));
            }
            HashedString = stringBuilder.ToString();
        }
        
        public string ValidatePassword(string password, string salt)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string saltAndPwd = string.Concat(password, Salt);

            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoder = Encoding.UTF8;
                byte[] resultant = hash.ComputeHash(encoder.GetBytes(saltAndPwd));

                foreach (byte b in resultant)
                    stringBuilder.Append(b.ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
