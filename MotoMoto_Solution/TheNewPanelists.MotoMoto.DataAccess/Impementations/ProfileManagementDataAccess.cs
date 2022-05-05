using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class ProfileManagementDataAccess : IProfileDataAccess
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
                    _mySqlConnection.Close();
                    return true;
                default:
                    _mySqlConnection.Close();
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
                return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return userProfile.GetResponse(ResponseModel.response.success);
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
                    return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
                return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return userProfile.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Retrieveal of all user posts that a user has posted. This is not included for event accounts
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
                    return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
                return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return userProfile.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// This functionality updates a profile description and displays it on their page.
        /// this function is not dynamic so it will change on page refresh.
        /// </summary>
        /// <param name="profileModel"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileDescription(ProfileModel userProfile)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("UpdateProfileDescription", _mySqlConnection))
                {
                    _mySqlConnection.Open();
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@_username", userProfile.username);
                    command.Parameters.AddWithValue("@_newDescription", userProfile.profileDescription);

                    var value = ExecuteQuery(command);
                }
            }
            catch
            {
                return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return userProfile.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Functionailty will allow a user to update their profile username. This will change the user profile
        /// username and will cause changes to all posts and login information.
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileUsername(ProfileModel userProfile)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("UpdateProfileUsername", _mySqlConnection))
                {
                    _mySqlConnection.Open();
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@_username", userProfile.username);
                    command.Parameters.AddWithValue("@_newUsername", userProfile.newProfileUsername);

                    var value = ExecuteQuery(command);
                }
            }
            catch
            {
                return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return userProfile.GetResponse(ResponseModel.response.success);
        }
        /// <summary>
        /// Update profile image will take in a path of a new user profile. Since we are going to allow uploaded
        /// images, this functionality is subject to change based on the user
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileImage(ProfileModel userProfile)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("UpdateProfileUsername", _mySqlConnection))
                {
                    _mySqlConnection.Open();
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@_username", userProfile.username);
                    command.Parameters.AddWithValue("@_newURL", userProfile.profileImagePath);

                    var value = ExecuteQuery(command);
                }
            }
            catch
            {
                return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
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
        public ProfileModel UpdateProfileStatus(ProfileModel userProfile)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("UpdateProfileStatus", _mySqlConnection))
                {
                    _mySqlConnection.Open();
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@_username", userProfile.username);
                    command.Parameters.AddWithValue("@_newURL", userProfile.status);

                    var value = ExecuteQuery(command);
                }
            }
            catch
            {
                return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            finally
            {
                _mySqlConnection.Close();
            }
            return userProfile.GetResponse(ResponseModel.response.success);
        }
    }
}