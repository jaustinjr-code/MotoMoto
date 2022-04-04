using System.Security.Cryptography;
using System.Text;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class GenerateCiphertext
    {
        public string? OriginalString { get; set; }
        public string? HashedString { get; set; }

        public GenerateCiphertext(string username, string email)
        {
            OriginalString = $"{username}_{email}";
            GenerateHash();
        }
        private void GenerateHash()
        {
            StringBuilder stringBuilder = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding encoder = Encoding.UTF8;
                Byte[] resultant = hash.ComputeHash(encoder.GetBytes(OriginalString!));

                foreach (Byte b in resultant)
                    stringBuilder.Append(b.ToString("x2"));
            }
            HashedString = stringBuilder.ToString();
        }
    }
}
