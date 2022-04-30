using MySql.Data.MySqlClient;
using System.Configuration;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class PostContentDataAccess : IDataAccess
    {
        private MySqlConnection? _mySqlConnection;
        private string? _connectionString; // Need config file to store connection string

        /// <summary>
        /// Default Empty Constructor
        /// </summary>
        public PostContentDataAccess() { }

        // NOTE: Recommended to use abstract base class instead of this method
        /// <summary>
        /// Establishes the MariaDB connection for cleaner code
        /// </summary>
        /// <returns>Boolean</returns>
        public bool EstablishMariaDBConnection()
        {
            // Does not work
            //ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["MotoMotoDevDBConnection"];
            //if (settings != null)
            //{
            //_connectionString = settings.ConnectionString;
            //}
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

        /// <summary>
        /// Refines the retrieved by the MySqlDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>IPostEntity</returns>
        public IPostEntity RefineData(MySqlDataReader reader)
        {
            // There should be only one post found
            if (reader.HasRows)
            {
                int postId = reader.GetInt32("postId");
                string postTitle = reader.GetString("postTitle");
                string postUsername = reader.GetString("postUsername");
                string postDescription = reader.GetString("postDescription");
                // Add images in the Service Layer
                IPostEntity post = new DataStorePost(postId, postTitle, postUsername, postDescription);
                reader.Close();
                return post;
            }
            reader.Close();
            throw new Exception("No post found");
        }

        /// <summary>
        /// Fetches the specific post according to IRequestModel
        /// Used for retrieving post details
        /// </summary>
        /// <param name="postInput"></param>
        /// <returns>IPostEntity</returns>
        public IPostEntity? FetchPost(IRequestModel postInput)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            // The post ID will be visible on the client side so using this info is okay
            string commandText = "SELECT * FROM Post WHERE postID = @postID;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                // Request Model is the simplest model for this specific operation
                // _mySqlConnection must not be null when beginning the transaction
                // Beginning the transaction will improve finding bugs in data integrity
                command.Parameters.AddWithValue("@postID", (int)((FetchPostDetailsRequestModel)postInput).input);
                command.Transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    IPostEntity result = ((IPostEntity)RefineData(command.ExecuteReader()));
                    command.Transaction.Commit();
                    _mySqlConnection.Close();
                    //Console.WriteLine(result);
                    return result;
                }
                catch (Exception e)
                {
                    command.Transaction.Rollback();
                    throw new Exception("No post fetched: Error Message: " + e.Message);
                }
            }
        }

        /// <summary>
        /// Puts a new post into the database
        /// </summary>
        /// <param name="postInput"></param>
        /// <returns>Boolean</returns>
        public bool PutPost(IPostModel postInput)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            // TODO: Insert Images in another class so to not require images here
            string commandText = "INSERT INTO Post (postUsername, feedName, postTitle, postDescription) VALUES (@postUsername, @feedName, @postTitle, @postDescription);";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Parameters.AddWithValue("@postUsername", postInput.postUser);
                command.Parameters.AddWithValue("@feedName", postInput.contentType); // The contentType is the feedName
                command.Parameters.AddWithValue("@postTitle", postInput.postTitle);
                command.Parameters.AddWithValue("@postDescription", postInput.postDescription);

                command.Transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    int result = command.ExecuteNonQuery();
                    command.Transaction.Commit();
                    _mySqlConnection.Close();
                    if (result == 1)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception e)
                {
                    command.Transaction.Rollback();
                    throw e; // Handle in Service layer
                }
            }
        }

        // NOTE: This can be put in an Interface or in an Abstract class
        /// <summary>
        /// Enum is used for the results of an upvote operation to avoid magic numbers
        /// </summary>
        enum UpvoteResult { NO_UPVOTE, NEW_UPVOTE, CHANGED_UVPOTE }

        /// <summary>
        /// Puts an upvote on the specific post according to the IInteractionModel
        /// </summary>
        /// <param name="interactionInput"></param>
        /// <returns>Boolean</returns>
        public bool PutUpvotePost(IInteractionModel interactionInput)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            string commandText = "INSERT UpvotePostAnalytics (postID, postTitle, upvoteUsername) VALUES(@postID, @postTitle, @upvoteUsername) ON DUPLICATE KEY UPDATE isUpvote = 1 + -1 * isUpvote;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Parameters.AddWithValue("@postID", ((UpvotePostModel)interactionInput).contentId);
                command.Parameters.AddWithValue("@postTitle", ((UpvotePostModel)interactionInput).contentTitle);
                command.Parameters.AddWithValue("@upvoteUsername", ((UpvotePostModel)interactionInput).interactUsername);

                command.Transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    int result = command.ExecuteNonQuery();
                    command.Transaction.Commit();
                    _mySqlConnection.Close(); // No finally block because return is in try block

                    // MariaDB will return 1 for new records and 2 for updates records
                    // Anything else is wrong for this operation
                    // Resulting numbers can be enums instead to avoid magic numbers
                    if (result == (int)UpvoteResult.NEW_UPVOTE || result == (int)UpvoteResult.CHANGED_UVPOTE)
                        return true;
                    else if (result == (int)UpvoteResult.NO_UPVOTE)
                        throw new Exception("No upvote made");
                    else
                        return false;
                }
                catch (Exception e)
                {
                    command.Transaction.Rollback();
                    throw e;
                }
            }
        }

        // Get Upvotes - Implement in different class
        // Get Comments - In CommentContentDataAccess
        // Get Images - Doesn't work yet
    }
}