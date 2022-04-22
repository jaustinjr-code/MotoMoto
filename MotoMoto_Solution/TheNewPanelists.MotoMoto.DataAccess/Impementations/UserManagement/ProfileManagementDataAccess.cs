using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public class ProfileManagementDataAccess : IDataAccess
    {
        private MySqlConnection? mySqlConnection = null;
        private readonly string _connectionString = "server=localhost;user=dev_moto;database=dev_UM;port=3306;password=motomoto;";

        public ProfileManagementDataAccess() {}

        public ProfileManagementDataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }
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
                command.CommandType = CommandType.Text;

                command.CommandText = $"SELECT * FROM Profile P WHERE P.USERNAME = @v1";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userProfile!.Username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                ProfileModel returnProfile = new ProfileModel();
                while (myReader.Read())
                {
                    returnProfile.Username = myReader.GetString("username");
                    returnProfile.Status = myReader.GetBoolean("status");
                    returnProfile.EventAccount = myReader.GetBoolean("eventAccount");
                }
                myReader.Close();
                mySqlConnection!.Close();
                return returnProfile;
            }
        }
        public ISet<ProfileModel> GetAllProfiles()
        {
            MySqlCommand command = new MySqlCommand();
            MySqlDataReader myReader = command.ExecuteReader();
            ISet<ProfileModel> accountsSet = new HashSet<ProfileModel>();
            while (myReader.Read())
            {
                ProfileModel userProfile = new ProfileModel();
                userProfile.Username = myReader.GetString("typeName");
                userProfile.Status = myReader.GetBoolean("status");
                userProfile.EventAccount = myReader.GetBoolean("eventAccount");
                accountsSet.Add(userProfile);
            }
            myReader.Close();
            mySqlConnection!.Close();
            return accountsSet;
        }
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
                command.CommandType = CommandType.Text;

                command.CommandText = @"INSERT INTO Profile (userId, username) SELECT u.userId, u.username FROM User u 
                                        EXCEPT SELECT p.userId, p.username FROM Profile p;";
                return (ExecuteQuery(command));
            }
        }
        public bool DeleteProfileEntity(DeleteAccountModel userAccount)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandType = CommandType.Text;

                command.CommandText = $"DELETE * FROM Profile P WHERE P.USERNAME = \'@v1\';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", userAccount!.Username);

                command.Parameters.AddRange(parameters);
                return(ExecuteQuery(command));
            }
        }
        public ProfileModel RetrieveProfileInformation(ProfileModel profileModel)
        {
            if (!EstablishMariaDBConnection())
            {
                throw new NullReferenceException();
            }
            using (MySqlCommand command = new MySqlCommand())
            {
                command.Transaction = mySqlConnection!.BeginTransaction();
                command.CommandTimeout = TimeSpan.FromSeconds(60).Seconds;
                command.CommandType = CommandType.Text;

                command.CommandText = "SELECT * FROM Profile P WHERE P.username = '@v1';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", profileModel.Username);

                command.Parameters.AddRange(parameters);

                MySqlDataReader myReader = command.ExecuteReader();
                ProfileModel returnProfile = new ProfileModel();
                while (myReader.Read())
                {
                    returnProfile.Username = myReader.GetString("username");
                    returnProfile.Status = myReader.GetBoolean("status");
                    returnProfile.EventAccount = myReader.GetBoolean("eventAccount");
                    returnProfile.ProfileDescription = myReader.GetString("profileDescription");
                    returnProfile.ProfileImagePath = myReader.GetString("profileImage");
                }
                myReader.Close();
                mySqlConnection!.Close();
                GetUserUpvotedPosts(returnProfile);
                GetUserPosts(returnProfile);
                return returnProfile;
            }
        }
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
                command.CommandType = CommandType.Text;

                command.CommandText = "SELECT * FROM VotePosts v WHERE v.username = '@v1';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", profileModel.Username);

                MySqlDataReader myReader = command.ExecuteReader();
                command.Parameters.AddRange(parameters);
                
                while (myReader.Read())
                {
                    if (myReader.GetBoolean("vote") == true)
                    {
                        var upvotepost = new UpvotedPostsModel()
                        {
                            likeid = myReader.GetInt32("likeid"),
                            postid = myReader.GetInt32("postid"),
                            vote = myReader.GetBoolean("vote")
                        };
                        profileModel.UpVotedPosts!.Add(upvotepost);
                    }
                }
                myReader.Close();
                mySqlConnection!.Close();
            }
        }

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
                command.CommandType = CommandType.Text;

                command.CommandText = "SELECT * FROM Posts v WHERE v.postUsername = '@v1';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", profileModel.Username);

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
                command.CommandType = CommandType.Text;

                command.CommandText = $"UPDATE Profile P SET P.profileDescription = '@v1' WHERE P.username = '@v2';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", profileModel!.ProfileDescription);
                parameters[1] = new MySqlParameter("@v2", profileModel!.Username);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }

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
                command.CommandType = CommandType.Text;

                command.CommandText = $"UPDATE Profile P SET P.profileImage = '@v1' WHERE P.username = '@v2';";
                var parameters = new MySqlParameter[1];
                parameters[0] = new MySqlParameter("@v1", profileModel!.ProfileImagePath);
                parameters[1] = new MySqlParameter("@v2", profileModel!.Username);

                command.Parameters.AddRange(parameters);
                return (ExecuteQuery(command));
            }
        }
    }
}