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

        public PostContentDataAccess() { }

        // NOTE: Recommended to use abstract base class instead of this method
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

        // TODO: refine data for a Post details and Comment details
        // Should both be consider IPostModels?
        public IPostEntity RefineData(MySqlDataReader reader)
        {
            if (reader.HasRows)
            {
                int postId = reader.GetInt32("postId");
                string postTitle = reader.GetString("postTitle");
                string postUsername = reader.GetString("postUsername");
                string postDescription = reader.GetString("postDescription");
                IPostEntity post = new DataStorePost(postId, postTitle, postUsername, postDescription);
                return post;
            }
            throw new Exception("No post found");
        }

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
                command.Parameters.AddWithValue("@postID", (int)((FetchPostDetailsRequestModel)postInput).input);
                try
                {
                    // _mySqlConnection must not be null when beginning the transaction
                    // Beginning the transaction will improve finding bugs in data integrity
                    _mySqlConnection!.BeginTransaction();
                    IPostEntity result = ((IPostEntity)RefineData(command.ExecuteReader()));
                    _mySqlConnection.Close();
                    //Console.WriteLine(result);
                    return result;
                }
                catch (Exception e)
                {
                    // Use logger here but how?
                    e.ToString();
                    throw new Exception("No post fetched: Error Message: " + e.Message);
                }
            }

            // Don't need this if Exception is thrown
            //IPostEntity? entity = null;
            //return entity;
        }
        public bool PutPost(IPostModel postInput)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }

            // string commandText = "INSERT INTO PROFILE (userId, username) SELECT u.userId, u.username FROM USER u EXCEPT SELECT p.userId, p.username FROM PROFILE p;";
            string commandText = "INSERT INTO Post (postUsername, feedName, postTitle, postDescription) VALUES (@postUsername, @feedName, @postTitle, @postDescription);";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Parameters.AddWithValue("@postUsername", postInput.postUser);
                command.Parameters.AddWithValue("@feedName", postInput.contentType); // Is contentType the feedName?
                command.Parameters.AddWithValue("@postTitle", postInput.postTitle);
                command.Parameters.AddWithValue("@postDescription", postInput.postDescription);

                //command.Parameters.Add("@username", MySqlDbType.VarChar);
                //command.Parameters.Add("@feedName", MySqlDbType.VarChar); // Is contentType the feedName?
                //command.Parameters.Add("@postTitle", MySqlDbType.VarChar);
                //command.Parameters.Add("@postDescription", MySqlDbType.VarChar);

                //command.Parameters["@username"].Value = postInput.postUser;
                //command.Parameters["@feedName"].Value = postInput.contentType;
                //command.Parameters["@postTitle"].Value = postInput.postTitle;
                //command.Parameters["@postDescription"].Value = postInput.postDescription;
                //Console.WriteLine(command.CommandText);

                // Prints out the commandText
                string query = command.CommandText;
                foreach (MySqlParameter p in command.Parameters)
                {
                    query = query.Replace(p.ParameterName, p.Value.ToString());
                }
                Console.WriteLine(query);

                MySqlTransaction transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    int result = command.ExecuteNonQuery();
                    transaction.Commit();
                    // _mySqlConnection.Close();
                    if (result == 1)
                    {
                        return true;
                    }
                    return false;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    // return false;
                    throw e; // Handle in Service layer
                }
                finally
                {
                    _mySqlConnection!.Close();
                }
            }
        }

        public bool PutUpvotePost(IInteractionModel interactionInput)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            //INSERT PostAnalytics (Upvote_User)
            //VALUES(upvoteUsername) ON DUPLICATE KEY
            //UPDATE Delete_Flag = 1;
            string commandText = "INSERT UpvotePostAnalytics (postID, postTitle, upvoteUsername) VALUES(@postID, @postTitle, @upvoteUsername) ON DUPLICATE KEY UPDATE isUpvote = 1 + -1 * isUpvote;";
            using (MySqlCommand command = new MySqlCommand(commandText, _mySqlConnection))
            {
                command.Parameters.AddWithValue("@postID", ((UpvotePostModel)interactionInput).contentId);
                command.Parameters.AddWithValue("@postTitle", ((UpvotePostModel)interactionInput).contentTitle);
                command.Parameters.AddWithValue("@upvoteUsername", ((UpvotePostModel)interactionInput).interactUsername);

                MySqlTransaction transaction = _mySqlConnection!.BeginTransaction();
                try
                {
                    int result = command.ExecuteNonQuery();
                    transaction.Commit();
                    // _mySqlConnection.Close();
                    if (result == 1 || result == 2)
                        return true;
                    else if (result == 0)
                        throw new Exception("No upvote made");
                    else
                        return false;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    Console.WriteLine(e.HelpLink);
                    throw e;
                }
                finally
                {
                    _mySqlConnection.Close();
                }
            }
        }

        // Can be used to fetch all posts with same title
        // public IEnumerable<IPostEntity>? FetchAllPosts(IFeedModel feedInput)
        // {
        //     // IFeedModel is used for SqlGenerator input
        //     // IEnumerable should assigned to postList in IFeedModel
        //     IEnumerable<IPostEntity> postList = new List<IPostEntity>();
        //     return null;
        // }
        // Get Upvotes
        // Get Comments
        // Get Images
        // Get Content
    }
}