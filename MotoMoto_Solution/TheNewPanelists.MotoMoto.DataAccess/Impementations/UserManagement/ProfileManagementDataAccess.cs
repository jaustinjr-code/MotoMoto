using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data;
using System.Configuration;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class ProfileManagementDataAccess : IDataAccess
    {
        private MySqlConnection? mySqlConnection = null;
        private string? _connectionString { get; set; }

        public ProfileManagementDataAccess() 
        {
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                    _connectionString = cs.ConnectionString;
            }
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
                    mySqlConnection!.Close();
                    return true;
                default:
                    mySqlConnection!.Close();
                    return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool EstablishMariaDBConnection()
        {
            try
            {
                mySqlConnection = new MySqlConnection(_connectionString);
                mySqlConnection.Open();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userProfile"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public ProfileModel RetrieveSpecifiedProfileEntity(ProfileModel userProfile)
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

                command.CommandText = $"SELECT * FROM Profile P WHERE P.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userProfile!.username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                ProfileModel returnProfile = new ProfileModel();
                while (myReader.Read())
                {
                    returnProfile.username = myReader.GetString("username");
                    returnProfile.status = myReader.GetBoolean("status");
                    returnProfile.eventAccount = myReader.GetBoolean("eventAccount");
                }
                myReader.Close();
                mySqlConnection!.Close();
                GetUserUpvotedPosts(returnProfile);
                GetUserPosts(returnProfile);
                return returnProfile;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public ISet<ProfileModel> GetAllProfiles()
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

                command.CommandText = $"SELECT * FROM Profile";

                MySqlDataReader myReader = command.ExecuteReader();
                ISet<ProfileModel> accountsSet = new HashSet<ProfileModel>();
                while (myReader.Read())
                {
                    ProfileModel userProfile = new ProfileModel();
                    userProfile.username = myReader.GetString("typeName");
                    userProfile.status = myReader.GetBoolean("status");
                    userProfile.eventAccount = myReader.GetBoolean("eventAccount");
                    userProfile.profileDescription = myReader.GetString("profileDescription");
                    userProfile.profileImagePath = myReader.GetString("profileImage");
                    accountsSet.Add(userProfile);
                }
                myReader.Close();
                mySqlConnection!.Close();
                return accountsSet;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool InsertNewProfileEntity()
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

                command.CommandText = @"INSERT INTO Profile (userId, username) SELECT u.userId, u.username FROM User u 
                                        EXCEPT SELECT p.userId, p.username FROM Profile p;";
                return (ExecuteQuery(command));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAccount"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool DeleteProfile(DeleteAccountModel userAccount)
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

                command.CommandText = "DELETE * FROM Profile P WHERE P.USERNAME = \'@v1\';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!.username);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileModel"></param>
        /// <exception cref="NullReferenceException"></exception>
        private void GetUserUpvotedPosts(ProfileModel profileModel)
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

                command.CommandText = "SELECT * FROM VotePosts v WHERE v.username = '@v1';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", profileModel.username);

                MySqlDataReader myReader = command.ExecuteReader();
                command.Parameters.AddRange(parameters);

                while (myReader.Read())
                {
                    if (myReader.GetBoolean("vote") == true)
                    {
                        var upvotepost = new UpvotedPostsModel()
                        {
                            likeId = myReader.GetInt32("likeid"),
                            postId = myReader.GetInt32("postid"),
                            userVote = myReader.GetBoolean("vote")
                        };
                        profileModel.upVotedPosts!.Add(upvotepost);
                    }
                }
                myReader.Close();
                mySqlConnection!.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileModel"></param>
        /// <exception cref="NullReferenceException"></exception>
        private void GetUserPosts(ProfileModel profileModel)
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

                command.CommandText = "SELECT * FROM Posts v WHERE v.postUsername = '@v1';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", profileModel.username);

                MySqlDataReader myReader = command.ExecuteReader();
                command.Parameters.AddRange(parameters);

                while (myReader.Read())
                {
                    var upvotepost = new UserPostModel()
                    {
                        postTitle = myReader.GetString("postTitle"),
                        postDescription = myReader.GetString("postDescription"),
                        contentType = myReader.GetString("feedName"),
                        submitUTC = myReader.GetDateTime("submitUTC ")
                    };
                    profileModel.userPosts!.Add(upvotepost);
                }
                myReader.Close();
                mySqlConnection!.Close();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileModel"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool UpdateProfileDescription(ProfileModel profileModel)
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
                return (ExecuteQuery(command));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="profileModel"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public bool UpdateProfileImage(ProfileModel profileModel)
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
                return (ExecuteQuery(command));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataStoreUser"></param>
        /// <returns></returns>
        public bool UpdateProfileUsername(AccountModel accountUser)
        {
            UserManagementDataAccess _userManagementDAO = new UserManagementDataAccess();
            var retrievalAccount = _userManagementDAO.RetrieveSpecifiedUserEntity(accountUser);

            switch (retrievalAccount)
            {
                case null:
                    return false;
                default:
                    using (MySqlCommand command = new MySqlCommand())
                    {
                        command.Transaction = mySqlConnection!.BeginTransaction();
                        command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                        command.Connection = mySqlConnection!;
                        command.CommandType = CommandType.Text;
                        command.CommandText = "UPDATE Profile P SET P.username = '@v1' WHERE P.username = '@v2';";
                        var parameters = new MySqlParameter[1];
                        parameters[0] = new MySqlParameter("@v1", accountUser!.username);
                        parameters[1] = new MySqlParameter("@v2", retrievalAccount!.username);

                        command.Parameters.AddRange(parameters);
                        return (ExecuteQuery(command));
                    }
            }
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
    }
}