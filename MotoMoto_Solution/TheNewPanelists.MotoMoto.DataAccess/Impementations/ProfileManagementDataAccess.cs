using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class ProfileManagementDataAccess
    {
        private MySqlConnection? _mySqlConnection = null;
        private string? _connectionString { get; set; }

        public ProfileManagementDataAccess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["motomotoDBConnection"].ConnectionString;
        }
        /// <summary>
        /// Uses the configuration file to set the name of the connection string. We do not use the overloaded constructor
        /// unless for expansion purposes where information needs to be stored to a new data store
        /// </summary>
        /// <param name="connectionString"></param>
        public ProfileManagementDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
        /// <summary>
        /// ExecuteQuery function is used
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private bool ExecuteQuery(MySqlCommand command)
        {
            switch (command.ExecuteNonQuery())
            {
                case 1:
                    _mySqlConnection!.Close();
                    return true;
                default:
                    _mySqlConnection!.Close();
                    return false;
            }
        }

        /// <summary>
        /// Retrieves a specified profile entity that is called upon 
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public ProfileModel RetrieveSpecifiedProfileEntity(ProfileModel userProfile)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("RetrieveSpecifiedProfile", _mySqlConnection))
                {
                    _mySqlConnection.Open();
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@_username", userProfile.username);

                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        userProfile.userId = reader.GetInt32("userId");
                        userProfile.username = reader.GetString("username");
                        userProfile.eventAccount = reader.GetBoolean("eventAccount");
                        userProfile.profileImagePath = reader.GetString("profileImage");
                        userProfile.profileDescription = reader.GetString("profileDescription");
                        userProfile.status = reader.GetBoolean("STATUS");
                    }
                    reader.Close();
                }
            }
            catch
            {
                if (userProfile.userId == null)
                    return new ProfileModel().GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
                return new ProfileModel().GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return userProfile.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Retrieves all the user profiles from the profile table
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public ProfileListModel GetAllProfiles(ProfileListModel profileListModel)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("GetAllProfiles", _mySqlConnection))
                {
                    _mySqlConnection.Open();
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;

                    MySqlDataReader reader = command.ExecuteReader();
                    IEnumerable<ProfileModel> list = new List<ProfileModel>();
                    while (reader.Read())
                    {
                        ProfileModel userProfile = new ProfileModel();
                        userProfile.userId = reader.GetInt32("userId");
                        userProfile.username = reader.GetString("username");
                        userProfile.eventAccount = reader.GetBoolean("eventAccount");
                        userProfile.profileImagePath = reader.GetString("profileImage");
                        userProfile.profileDescription = reader.GetString("profileDescription");
                        userProfile.status = reader.GetBoolean("STATUS");
                        ((List<ProfileModel>)list).Add(userProfile);
                    }
                    profileListModel.profiles = list;
                }
            }
            catch
            {
                if (profileListModel.profiles == null)
                    return new ProfileListModel().GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
                return new ProfileListModel().GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return profileListModel.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// This function ensure that if a user owns/registers an 
        /// account that all the information tied to their account
        /// will be stored
        /// </summary>
        /// <returns></returns>
        public ProfileModel InstertAllExsitingUsers()
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("DumpAllNewProfiles", _mySqlConnection))
                {
                    _mySqlConnection.Open();
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    var value = ExecuteQuery(command);
                }
            }
            catch
            {
                return new ProfileModel().GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return new ProfileModel().GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Deletes a specified profile on account deletion
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public ProfileModel DeleteProfileDataAccess(ProfileModel userProfile)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("DeleteProfile", _mySqlConnection))
                {
                    _mySqlConnection.Open();
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@_username", userProfile.username);

                    var value = ExecuteQuery(command);
                }
            }
            catch
            {
                return new ProfileModel().GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return new ProfileModel().GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Retrieves all the posts that a user has upvoted that is not their own posts
        /// to display on their profile
        /// 
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public ProfileModel RetrieveAllUpvotesPostsForProfile(ProfileModel userProfile)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("GetAllPostsWhereUserUpvoted", _mySqlConnection))
                {
                    _mySqlConnection.Open();
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@_username", userProfile.username);

                    MySqlDataReader reader = command.ExecuteReader();
                    IEnumerable<UpvotedPostsModel> postList = new List<UpvotedPostsModel>();
                    while (reader.Read())
                    {
                        UpvotedPostsModel upvotedPost = new UpvotedPostsModel
                        {
                            postId = reader.GetInt32("postId"),
                            postUsername = reader.GetString("postUsername"),
                            postTitle = reader.GetString("postTitle"),
                            feedName = reader.GetString("feedName"),
                            postDescription = reader.GetString("postDescription"),
                            submitTime = reader.GetDateTime("submitUTC"),
                            upvoteUsername = reader.GetString("upvoteUsername")
                        };
                        ((List<UpvotedPostsModel>)postList).Add(upvotedPost);
                    }
                    userProfile.upVotedPosts = postList;
                }
            }
            catch
            {
                if (userProfile.upVotedPosts == null)
                    return new ProfileModel().GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
                return new ProfileModel().GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return userProfile.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public ProfileModel GetAllUsersPosts(ProfileModel userProfile)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("GetAllUserCreatePosts", _mySqlConnection))
                {
                    _mySqlConnection.Open();
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@_username", userProfile.username);
                    MySqlDataReader reader = command.ExecuteReader();
                    IEnumerable<UserPostModel> postList = new List<UserPostModel>();
                    while (reader.Read())
                    {
                        UserPostModel post = new UserPostModel
                        {
                            postId = reader.GetInt32("postId"),
                            postTitle = reader.GetString("postTitle"),
                            feedName = reader.GetString("feedName"),
                            postDescription = reader.GetString("postDescription"),
                            submitUTC = reader.GetDateTime("submitUTC")
                        };
                        ((List<UserPostModel>)postList).Add(post);
                    }
                    userProfile.userPosts = postList;
                }
            }
            catch
            {
                if (userProfile.userPosts == null)
                    return new ProfileModel().GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
                return new ProfileModel().GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return userProfile.GetResponse(ResponseModel.response.success);
        }
    }
}

/**
         /// <summary>
        /// 
        /// </summary>
        /// <param name="profileModel"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public ProfileModel UpdateProfileDescription(ProfileModel profileModel)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "UPDATE Profile P SET P.profileDescription = '@v1' WHERE P.username = '@v2';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", profileModel!.profileDescription);
                parameters[1] = new MySqlParameter("@v2", profileModel!.username);

                command.Parameters.AddRange(parameters);
                ExecuteQuery(command)
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileModel"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public ProfileModel UpdateProfileImage(ProfileModel profileModel)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "UPDATE Profile P SET P.profileImage = '@v1' WHERE P.username = '@v2';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", profileModel!.profileImagePath);
                parameters[1] = new MySqlParameter("@v2", profileModel!.username);

                command.Parameters.AddRange(parameters);
                ExecuteQuery(command);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataStoreUser"></param>
        /// <returns></returns>
        public override async Task<IActionResult> UpdateProfileUsername(AccountModel accountUser)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool UpdateProfileStatus(ProfileModel profile)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.Connection = mySqlConnection!;
                command.CommandType = CommandType.Text;

                command.CommandText = "UPDATE Profile P SET P.Status = v0 WHERE P.username = '@v1';";
                var parameters = new MySqlParameter[2];
                parameters[0] = new MySqlParameter("@v0", profile.status);
                parameters[1] = new MySqlParameter("@v1", profile.username);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }
 
 
 */