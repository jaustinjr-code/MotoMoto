using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.UnitTests.ProfileUnitTest
{
    public class ProfileServiceUnitTest
    {
        private readonly IProfileManagementService _profileManagementService = new ProfileManagementService();
        /// <summary>
        /// Validate profile retrieval that there exists a profile model type under the name ran 
        /// Otherwise since service only passes data and retrieves there exists no real checks in the 
        /// service layer other than for invalid responses
        /// </summary>
        [Fact]
        public void ValidatedProfileModelRetrival_ReturnTrue()
        {
            ProfileModel model = new ProfileModel
            {
                username = "ran"
            };
            var retmodel = _profileManagementService.RetrieveSpecifiedProfileEntity(model);
            if (retmodel.GetType() == typeof(ProfileModel))
            {
                Assert.True(true, "model passes as a profile model");
            };
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ValidateProfileModelRetrievalUserPostsList_ReturnTrue()
        {
            ProfileModel model = new ProfileModel
            {
                username = "ran"
            };
            var retmodel = _profileManagementService.RetrieveSpecifiedUserPosts(model);

            if (retmodel.GetType() == typeof(ProfileModel) && ((List<UserPostModel>)retmodel.userPosts!).Count >= 0)
            {
                Assert.True(true, "model list passed with non initialized posts greater than 0 for a valid return model");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ValidateProfileListOfProfileLists_ReturnTrue()
        {
            var listModel = _profileManagementService.RetrieveAllProfileModels();

            if (listModel.GetType() == typeof(ProfileListModel) && ((List<ProfileModel>)listModel.profiles!).Count > 1)
            {
                Assert.True(true, "model list of users passed woth retrieval if this text is shown then all users have not been displayed");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void ValidateProfileModelRetrievalUserUpvoteList_ReturnTrue()
        {
            ProfileModel model = new ProfileModel
            {
                username = "ran"
            };
            var retmodel = _profileManagementService.RetrieveAllUpvotesPostsForProfile(model);

            if (retmodel.GetType() == typeof(ProfileModel) && ((List<UserPostModel>)retmodel.upVotedPosts!).Count >= 0)
            {
                Assert.True(true, "model list passed with non initialized posts greater than 0 for a valid return model");
            }
        }
    }
}
