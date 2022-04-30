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

        public CommentContentDataAccess() { }


        public bool EstablishMariaDBConnection()
        {
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

                // You repeat this a few times, maybe put in it's own function?
                MySqlTransaction transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    int result = command.ExecuteNonQuery();
                    transaction.Commit();
                    _mySqlConnection.Close();
                    if (result == 1)
                        return true;
                    return false;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public IEnumerable<IContentEntity> FetchComments(IPostModel postInput)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            // Maybe use ASC if you want latest comments to start at bottom 
            string commandText = "SELECT * FROM Comment WHERE postID = @postID ORDER BY submitUTC DESC;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Parameters.AddWithValue("@postID", ((CommentPostModel)postInput).postID);

                MySqlTransaction transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    IEnumerable<IContentEntity> result = (List<IContentEntity>)RefineData(command.ExecuteReader()); // Might want to refine data here
                    transaction.Commit();
                    _mySqlConnection.Close();
                    // Exception for when result is null?
                    return result;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

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

                MySqlTransaction transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    int result = command.ExecuteNonQuery();
                    transaction.Commit();
                    _mySqlConnection.Close();
                    // Should use ENUM instead of numbers
                    if (result == 1 || result == 2)
                        return true;
                    else
                        throw new Exception("No upvote made");
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }
    }
}