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
        private readonly MySqlConnection _mySqlConnection = new MySqlConnection();
        private string _connectionString = "server=moto-moto.crd4iyvrocsl.us-west-1.rds.amazonaws.com;user=dev_moto;database=pro_moto;port=3306;password=motomoto;";
        public ProfileManagementDataAccess() { }
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
        /// Establish mariadbconnection ensures the our connection string
        /// is valid. If it does not pass the establishment portion then
        /// our system is notified that connection cannot be passed
        /// </summary>
        /// <returns></returns>
        public bool EstablishMariaDBConnection()
        {
            try
            {
                _mySqlConnection.ConnectionString = _connectionString;
                _mySqlConnection.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            if (!EstablishMariaDBConnection())
            {
                return userProfile;
            }
            try
            {
                using (MySqlCommand command = new MySqlCommand("GetSpecifiedUserProfile", _mySqlConnection))
                {
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@_username", userProfile.username);
                    MySqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ProfileModel profile = new ProfileModel
                        {
                            userId = reader.GetInt32("userId"),
                            username = reader.GetString("username"),
                            eventAccount = reader.GetBoolean("eventAccount"),
                            profileImagePath = reader.GetString("profileImage"),
                            profileDescription = reader.GetString("profileDescription"),
                            status = reader.GetBoolean("STATUS")

                        };
                        userProfile = profile;
                    }
                }
            }
            catch (Exception)
            {
                if (userProfile.username == null)
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
        /// Retrieves all the user profiles from the profile table
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public ProfileListModel GetAllProfiles(ProfileListModel profileListModel)
        {
            if (!EstablishMariaDBConnection())
            {
                return profileListModel;
            }
            try
            {
                using (MySqlCommand command = new MySqlCommand("GetAllProfiles", _mySqlConnection))
                {
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
        public ProfileModel InsertAllExsitingUsers()
        {
            if (!EstablishMariaDBConnection())
            {
                return new ProfileModel().GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
            }
            try
            {
                using (MySqlCommand command = new MySqlCommand("DumpAllNewProfiles", _mySqlConnection))
                {
                    command.Transaction = _mySqlConnection.BeginTransaction();
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    var value = command.ExecuteNonQuery();
                    command.Transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
            if (!EstablishMariaDBConnection())
            {
                return userProfile;
            }
            try
            {
                using (MySqlCommand command = new MySqlCommand("DeleteProfile", _mySqlConnection))
                {
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
            if (!EstablishMariaDBConnection())
            {
                return userProfile;
            }
            try
            {
                using (MySqlCommand command = new MySqlCommand("GetAllPostsWhereUserUpvoted", _mySqlConnection))
                {
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
            if (!EstablishMariaDBConnection())
            {
                return userProfile;
            }
            try
            {
                using (MySqlCommand command = new MySqlCommand("GetAllUserCreatePosts", _mySqlConnection))
                {
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
                if (userProfile.userPosts == null)
                {
                    userProfile.userPosts = new List<UserPostModel>();
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
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                mySqlConnection.Open();
                string comm = "UpdateProfileDescription";

                using (var command = new MySqlCommand(comm, mySqlConnection))
                {
                    try
                    {
                        command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@_username", MySqlDbType.VarChar);
                        command.Parameters.Add("@_newDescription", MySqlDbType.VarChar);


                        command.Parameters["@_username"].Value = userProfile.username;
                        command.Parameters["@_newDescription"].Value = userProfile.profileDescription;
                        int numChanged = command.ExecuteNonQuery();

                        Console.WriteLine(numChanged);

                        if (numChanged > 0)
                            return userProfile.GetResponse(ResponseModel.response.success);
                    }
                    catch
                    {
                        return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectNonExistent);
                    }
                    finally
                    { 
                        mySqlConnection.Close();
                    }
                    return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectOutOfRange);
                }
            }
        }
        /// <summary>
        /// Functionailty will allow a user to update their profile username. This will change the user profile
        /// username and will cause changes to all posts and login information. Cannot implicitly run this function
        /// until all subclasses are dealt with
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileUsername(ProfileModel userProfile)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                mySqlConnection.Open();
                string comm = "UpdateProfileUsername";
                using (var command = new MySqlCommand(comm, mySqlConnection))
                {
                    try
                    {
                        command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@_username", MySqlDbType.VarChar);
                        command.Parameters.Add("@_newUsername", MySqlDbType.VarChar);

                        command.Parameters["@_username"].Value = userProfile.username;
                        command.Parameters["@_newUsername"].Value = userProfile.newProfileUsername;
                        int numChanged = command.ExecuteNonQuery();

                        if (numChanged > 0)
                            Console.WriteLine(numChanged);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                    }
                    finally 
                    { 
                        mySqlConnection.Close(); 
                    }
                    return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectOutOfRange);
                }
            }
        }
        /// <summary>
        /// Update profile image will take in a path of a new user profile. Since we are going to allow uploaded
        /// images, this functionality is subject to change based on the user
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileImage(ProfileModel userProfile)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                mySqlConnection.Open();
                string comm = "UpdateProfileImageRoute";

                using (var command = new MySqlCommand(comm, mySqlConnection))
                {
                    command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@_username", MySqlDbType.VarChar);
                    command.Parameters.Add("@_newURL", MySqlDbType.VarChar);

                    command.Parameters["@_username"].Value = userProfile.username;
                    command.Parameters["@_newURL"].Value = userProfile.profileImagePath;
                    Console.WriteLine(command.CommandText);
                    int numChanged = command.ExecuteNonQuery();

                    if (numChanged > 0)
                        return userProfile.GetResponse(ResponseModel.response.success);
                }
                return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectOutOfRange);
            }
        }
        /// <summary>
        /// Updating profile status to determine whether an account is active or not
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        public ProfileModel UpdateProfileStatus(ProfileModel userProfile)
        {
            using (MySqlConnection mySqlConnection = new MySqlConnection(_connectionString))
            {
                mySqlConnection.Open();
                string comm = "UpdateProfileStatus";
                using (var command = new MySqlCommand(comm, mySqlConnection))
                {
                    try
                    {
                        byte tinyIntBool = 0;   
                        command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add("@_username", MySqlDbType.VarChar);
                        command.Parameters.Add("@_status", MySqlDbType.Byte);

                        command.Parameters["@_username"].Value = userProfile.username;
                        switch(userProfile.status)
                        {
                            case true:
                                tinyIntBool = 1;
                                break;
                            default:
                                break;
                        }
                        command.Parameters["@_status"].Value = tinyIntBool;

                        int numChanged = command.ExecuteNonQuery();
                        Console.WriteLine(numChanged);
                        if (numChanged > 0)
                            return userProfile.GetResponse(ResponseModel.response.success);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine(ex.Message);
                    }
                    finally
                    {
                        mySqlConnection.Close();
                    }
                    return userProfile.GetResponse(ResponseModel.response.dataAccessFailedObjectOutOfRange);
                }
            }
        }
    }
}