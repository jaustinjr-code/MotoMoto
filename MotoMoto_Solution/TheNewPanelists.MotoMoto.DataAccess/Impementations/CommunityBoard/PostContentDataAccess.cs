/*s
using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.Models;
using System.Configuration;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class PostContentDataAccess : IContentDataAccess
    {
        private MySqlConnection? _mySqlConnection { get; }
        private string _connectionString; // Need config file to store connection string

        public PostDataAccess()
        {
            // _connectionString = Configurations.connectionstrings([moto_db]);
        }
        public bool EstablishMariaDBConnection()
        {
            return false;
        }
        public string SqlGenerator()
        {
            return "";
        }
        public IFeedEntity GetPost(IFeedModel postInput);
        public bool PutPost(IFeedModel postInput);
        // Get Upvotes
        // Get Comments
        // Get Images
        // Get Content
    }
}
*/