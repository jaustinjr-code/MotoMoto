using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class LoadFeedDataAccess : IDataAccess
    {
        private MySqlConnection? _mySqlConnection;
        private string? _connectionString;
        //private Object? _input;
        //MySqlCommand? _command;

        // I opted out of using this because I may not need to define _connectionString
        // or create a _mySqlConnection if something goes wrong in the process of the operation.
        /// <summary>
        /// Default Empty Constructor
        /// </summary>
        public LoadFeedDataAccess() { }

        /// <summary>
        /// Establishes the MariaDB connection for cleaner code
        /// </summary>
        /// <returns></returns>
        public bool EstablishMariaDBConnection()
        {
            // I try to use the ConfigurationManager to avoid hardcoding the connection
            // but it does not work
            // Production code
            //ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MotoMotoRDSConnection"];
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

            // NOTE: this is not ideal and should be assigned using .config file attributes
            //       but the ConfigurationManager always returns null and I don't know why
            _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;";

            // Creates a SqlConnection and opens it
            try
            {
                // Close the connection if one is already opened
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

        /// <summary>
        /// Refines the data retrieved by the MySqlDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public IEnumerable<IPostEntity> RefineData(MySqlDataReader reader)
        {
            // I opted of using a for-loop with FieldCount b/c I know what I want from the record
            // Although the database can be altered in the future, the hassle of doing it makes it
            // unlikely that these changes would be done often, but it is still not robust in my opinion
            //int col = reader.FieldCount;

            // Checks if there are any records to refine
            if (reader.HasRows)
            {
                IEnumerable<IPostEntity> postList = new List<IPostEntity>();
                // Continue to read until there are no more records
                while (reader.Read())
                {
                    int postId = reader.GetInt32("postId");
                    string postTitle = reader.GetString("postTitle");
                    string postUsername = reader.GetString("postUsername");
                    string postDescription = reader.GetString("postDescription");
                    // Requires MySqlDataReader on Images table
                    //IEnumerable<byte[]> imageList = reader.GetByte("imageList");
                    IPostEntity post = new DataStorePost(postId, postTitle, postUsername, postDescription);
                    // Append from IEnumerable does not work so must use a List object casting for Add
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

        /// <summary>
        /// Fetches only one post according to the post ID
        /// The post ID will be available on the client side because it is the only unqiue attribute
        /// for a post at the moment
        /// As long as the post ID is not modifiable throughout this process then nothing should break
        /// </summary>
        /// <param name="postInput"></param>
        /// <returns></returns>
        // public IPostEntity? FetchPost(IPostModel postInput)
        // {
        //     if (!EstablishMariaDBConnection())
        //     {
        //         throw new NullReferenceException();
        //     }

        //     // The post ID will be visible on the client side so using this info is okay
        //     string commandText = "SELECT * FROM Post WHERE postID = @postID;";
        //     using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
        //     {
        //         command.Parameters.AddWithValue("@postID", postInput.postID);
        //         try
        //         {
        //             // _mySqlConnection must not be null when beginning the transaction
        //             // Beginning the transaction will improve finding bugs in data integrity
        //             _mySqlConnection!.BeginTransaction();
        //             IPostEntity result = ((List<IPostEntity>)RefineData(command.ExecuteReader())).ElementAt(0);
        //             _mySqlConnection.Close();
        //             //Console.WriteLine(result);
        //             return result;
        //         }
        //         catch (Exception e)
        //         {
        //             e.ToString();
        //         }
        //     }

        //     IPostEntity? entity = null;
        //     return entity;
        // }

        /// <summary>
        /// Fetches all the posts of a specified community feed name
        /// Feed names are unique, that's why it is input for this operation
        /// </summary>
        /// <param name="feedInput"></param>
        /// <returns></returns>
        public IFeedEntity? FetchAllPosts(IFeedModel feedInput)
        {
            // Establish connection assigns connection string
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            //else _mySqlConnection!.BeginTransaction();

            // Select most recent posts within set number of days
            // Order by the most recent to the latest because that is how the feed
            // should be organized
            string commandText = "SELECT * FROM Post WHERE feedName LIKE @feedname AND DATEDIFF(NOW(), submitUTC) < @diff ORDER BY submitUTC DESC;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                // Difference of NOW and the post UTC submission time is a week
                int diff = 7;
                command.Parameters.AddWithValue("@feedname", feedInput.feedName);
                command.Parameters.AddWithValue("@diff", diff);
                try
                {
                    command.Transaction = _mySqlConnection!.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    IEnumerable<IPostEntity> result = RefineData(command.ExecuteReader());
                    command.Transaction.Commit();
                    _mySqlConnection.Close();

                    // Defining the DataStoreCommunityFeed is unrelated to the SqlCommand so it is
                    // past the Close statement
                    IFeedEntity feed = new DataStoreCommunityFeed(feedInput.feedName, result, null);
                    //Console.WriteLine(feed.feedName + " " + feed.postList.ElementAt(0).postTitle);
                    //Console.WriteLine(feed);
                    return feed;
                }
                catch (Exception e)
                {
                    command.Transaction.Rollback();
                    // Trigger Logging Service
                    e.ToString();
                    throw e;
                }
            }
        }
    }
}