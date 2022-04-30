using MySql.Data.MySqlClient;
using System.Configuration;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class CommentContentDataAccess : IDataAccess
    {
        MySqlConnection? _mySqlConnection;
        string? _connectionString;

        /// <summary>
        /// Default Empty Constructor
        /// </summary>
        public CommentContentDataAccess() { }

        // NOTE: Recommended to use abstract base class instead of this method
        /// <summary>
        /// Establishes the MariaDB connection for cleaner code
        /// </summary>
        /// <returns>Boolean</returns>
        public bool EstablishMariaDBConnection()
        {
            // Does not work
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

        /// <summary>
        /// Refines the retrieved by the MySqlDataReader
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>IEnumerable<IContentEntity></returns>
        private IEnumerable<IContentEntity> RefineData(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                IEnumerable<IContentEntity> commentList = new List<IContentEntity>();
                while (reader.Read())
                {
                    int commentId = reader.GetInt32("commentID");
                    // All comments retrieved should have the same postID but it's best to make sure
                    int postId = reader.GetInt32("postID");
                    string commentUsername = reader.GetString("commentUsername");
                    string commentDescription = reader.GetString("commentDescription");
                    IContentEntity comment = new DataStoreComment(commentId, postId, commentUsername, commentDescription);
                    ((List<IContentEntity>)commentList).Add(comment);
                }
                return commentList;
            }
            throw new Exception("No comment found");
        }

        /// <summary>
        /// Puts a new comment into the database
        /// Treats a comment as another post, but with different requirements
        /// </summary>
        /// <param name="postInput"></param>
        /// <returns>Boolean</returns>
        public bool PutCommentPost(IPostModel postInput)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            // Cast IPostModel to a CommentModel
            string commandText = "INSERT INTO Comment (postID, commentUsername, commentDescription) VALUES (@postID, @commentUsername, @commentDescription);";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                // Post ID is present in the CommentPostModel and is not required for IPostModel
                command.Parameters.AddWithValue("@postID", ((CommentPostModel)postInput).postID);
                command.Parameters.AddWithValue("@commentUsername", postInput.postUser);
                command.Parameters.AddWithValue("@commentDescription", postInput.postDescription);

                command.Transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    int result = command.ExecuteNonQuery();
                    command.Transaction.Commit();
                    _mySqlConnection.Close();
                    if (result == 1)
                        return true;
                    return false;
                }
                catch (Exception e)
                {
                    command.Transaction.Rollback();
                    throw e;
                }
            }
        }

        /// <summary>
        /// Fetches comments for the specific post according to the IPostModel
        /// This operation may be compounded with the fetch post details operation
        /// therefore the return type is an IEnumerable that would be contained in another class
        /// </summary>
        /// <param name="postInput"></param>
        /// <returns>IEnumerable<IContentEntity></returns>
        public IEnumerable<IContentEntity> FetchComments(IPostModel postInput)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            // Use ASC if you want latest comments to start at bottom of comment section
            string commandText = "SELECT * FROM Comment WHERE postID = @postID ORDER BY submitUTC DESC;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Parameters.AddWithValue("@postID", ((CommentPostModel)postInput).postID);

                command.Transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    IEnumerable<IContentEntity> result = (List<IContentEntity>)RefineData(command.ExecuteReader()); // Might want to refine data here
                    command.Transaction.Commit();
                    _mySqlConnection.Close();

                    return result;
                }
                catch (Exception e)
                {
                    command.Transaction.Rollback();
                    throw e;
                }
            }
        }

        // NOTE: This can be put in an Interface or in an Abstract class
        /// <summary>
        /// Enum is used for the results of an upvote operation to avoid magic numbers
        /// </summary>
        enum UpvoteResult { NO_UPVOTE, NEW_UPVOTE, CHANGED_UVPOTE }

        /// <summary>
        /// Puts an upvote for the specific comment according to the IInteractionModel
        /// </summary>
        /// <param name="interactionInput"></param>
        /// <returns>Boolean</returns>
        public bool PutUpvoteComment(IInteractionModel interactionInput)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            string commandText = "INSERT UpvotePostAnalytics (commentID, postID, username) VALUES(@commentID, @postID, @username) ON DUPLICATE KEY UPDATE isUpvote = 1 + -1 * isUpvote;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Parameters.AddWithValue("@commentID", ((UpvoteCommentModel)interactionInput).contentId);
                command.Parameters.AddWithValue("@postID", ((UpvoteCommentModel)interactionInput).postId);
                command.Parameters.AddWithValue("@username", ((UpvoteCommentModel)interactionInput).interactUsername);

                command.Transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    int result = command.ExecuteNonQuery();
                    command.Transaction.Commit();
                    _mySqlConnection.Close();

                    // MariaDB will return 1 for new records and 2 for updates records
                    // Anything else is wrong for this operation
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
    }
}