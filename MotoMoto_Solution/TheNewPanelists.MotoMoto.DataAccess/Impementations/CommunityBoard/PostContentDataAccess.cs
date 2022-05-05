using MySql.Data.MySqlClient;
using System.Configuration;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class PostContentDataAccess : IContentDataAccess
    {
        private MySqlConnection? _mySqlConnection { get; }
        // private string? _connectionString; // Need config file to store connection string

        public bool PostDataAccess()
        {
            // _connectionString = "";
            //_connectionString = Configurations.connectionstrings([moto_db]);
            return false;
        }
        public bool EstablishMariaDBConnection()
        {
            return false;
        }
        public string SqlGenerator()
        {
            return "";
        }
        public IFeedEntity? GetPost(IFeedModel postInput)
        {
            // IFeedModel is used for SqlGenerator input
            // Update this nullable object
            IFeedEntity? entity = null;
            // IFeedEntity is being returned
            return entity;
        }
        public bool PutPost(IFeedModel postInput)
        {
            return false;
        }

        public IEnumerable<IPostEntity>? FetchAllPosts(IFeedModel feedInput)
        {
            // IFeedModel is used for SqlGenerator input
            // IEnumerable should assigned to postList in IFeedModel
            IEnumerable<IPostEntity> postList = new List<IPostEntity>();
            return null;
        }
        // Get Upvotes
        // Get Comments
        // Get Images
        // Get Content
    }
}