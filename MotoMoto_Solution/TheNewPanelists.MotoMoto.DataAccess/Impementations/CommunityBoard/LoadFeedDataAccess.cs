using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class LoadFeedDataAccess : IContentDataAccess
    {
        private MySqlConnection? _mySqlConnection;
        private string? _connectionString;
        //private Object? _input;
        //MySqlCommand? _command;

        public LoadFeedDataAccess() { }

        public bool EstablishMariaDBConnection()
        {
            // Production code
            // ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MotoMotoRDSConnection"];
            // Development code
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MotoMotoDevDBConnection"];
            if (settings != null)
            {
                _connectionString = settings.ConnectionString;
            }
            //else
            //{
            //Console.WriteLine("False");
            //return false;
            //}

            _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;";

            try
            {
                if (_mySqlConnection != null && _mySqlConnection.State == System.Data.ConnectionState.Open)
                    _mySqlConnection.Close();

                _mySqlConnection = new MySqlConnection(_connectionString);
                _mySqlConnection.Open();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        // Unused because it is not generic enough
        //public string SqlGenerator()
        //{
        //if (_mySqlConnection == null) throw new Exception("MySqlConnection is null");
        //_command = new MySqlCommand();

        //return "";
        //}

        public IEnumerable<IPostEntity> RefineData(MySqlDataReader reader)
        {
            //int col = reader.FieldCount;
            if (reader.HasRows)
            {
                IEnumerable<IPostEntity> postList = new List<IPostEntity>();
                while (reader.Read())
                {
                    int postId = reader.GetInt32("postId");
                    string postTitle = reader.GetString("postTitle");
                    string postUsername = reader.GetString("postUsername");
                    string postDescription = reader.GetString("postDescription");
                    // Requires MySqlDataReader on Images table
                    //IEnumerable<byte[]> imageList = reader.GetByte("imageList");
                    IPostEntity post = new DataStorePost(postId, postTitle, postUsername, postDescription, null);
                    ((List<IPostEntity>)postList).Add(post);
                    //Console.WriteLine(post);
                    //Console.WriteLine(post.postId + " " + post.postTitle);
                }
                //Console.WriteLine(postList.Count());
                return postList;
            }
            throw new Exception("No records");
        }

        //public MySqlCommand SqlGeneratorGeneric(MySqlParameter[] paramaters, string commandText)
        //{
        //MySqlCommand command = new MySqlCommand();
        //return command;
        //}

        public IPostEntity? FetchPost(IPostModel postInput)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            // The post id will be visible on the client side so using this info is okay
            string commandText = "SELECT * FROM Post WHERE postID = @postID;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Parameters.AddWithValue("@postID", postInput.postID);
                try
                {
                    _mySqlConnection!.BeginTransaction();
                    IPostEntity result = ((List<IPostEntity>)RefineData(command.ExecuteReader())).ElementAt(0);
                    _mySqlConnection.Close();
                    //Console.WriteLine(result);
                    return result;
                }
                catch (Exception e)
                {
                    e.ToString();
                }
            }

            IPostEntity? entity = null;
            return entity;
        }

        public IFeedEntity? FetchAllPosts(IFeedModel feedInput)
        {
            // Establish connection assigns connection string
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            //else _mySqlConnection!.BeginTransaction();

            // Select most recent posts within set number of days
            string commandText = "SELECT * FROM Post WHERE feedName LIKE @feedname AND DATEDIFF(NOW(), submitUTC) < @diff ORDER BY submitUTC DESC;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                //MySqlParameter[] parameters = new MySqlParameter[1];
                //parameters[0] = new MySqlParameter("@feedname", feedInput.feedName);
                int diff = 7;
                command.Parameters.AddWithValue("@feedname", feedInput.feedName);
                command.Parameters.AddWithValue("@diff", diff);
                try
                {
                    command.Transaction = _mySqlConnection!.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    IEnumerable<IPostEntity> result = RefineData(command.ExecuteReader());
                    _mySqlConnection.Close();
                    IFeedEntity feed = new DataStoreCommunityFeed(feedInput.feedName, result, null);
                    //Console.WriteLine(feed.feedName + " " + feed.postList.ElementAt(0).postTitle);
                    //Console.WriteLine(feed);
                    return feed;
                }
                catch (Exception e)
                {
                    // Trigger Logging Service
                    e.ToString();
                }
            }
            return null;

            // Connect to MariaDB
            // Assumes valid connection string
            // Temporary connection disposed after query completion
            //using (MySqlConnection connection = new MySqlConnection(_connectionString))
            //_mySqlConnection = new MySqlConnection(_connectionString);

            // Check for an already open connection
            // NOTE: Most likely don't need this if contained within this using block






            // IFeedModel is used for SqlGenerator input
            // Use FetchPost to return each post individually and add into postList
            // IEnumerable should assigned to postList in IFeedModel
            //IEnumerable<IPostEntity> postList = new List<IPostEntity>();
            //return null;
        }
    }
}