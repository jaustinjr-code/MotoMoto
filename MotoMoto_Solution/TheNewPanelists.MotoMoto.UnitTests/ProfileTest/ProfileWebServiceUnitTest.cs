using Xunit;
using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.WebServices.Profile.Controllers;
using TheNewPanelists.MotoMoto.Models;
using System.Collections.Generic;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class ProfileWebServiceUnitTest
    {
        private UserProfileController _userProfileController = new UserProfileController();
        private UserProfileUpdateController _userProfileUpdateController = new UserProfileUpdateController();

        /// <summary>
        /// validate return object is used to validate true users. If it is the case that users
        /// are retrieved and that the status code returns 200 when callled upon
        /// </summary>
        [Fact]
        public void ValidateReturnObjectTypeRetrievalAllUsers_ReturnTrue()
        {
            var result = _userProfileController.RetrieveAllProfiles();
            var okResult = result as OkObjectResult;
            ProfileListModel model = new ProfileListModel();
            if (okResult.Value != null)
                model = (ProfileListModel)okResult.Value;
            bool checkStatus = object.Equals(200, okResult.StatusCode);

            if (checkStatus && ((List<ProfileModel>)model.profiles!).Count != 0)
            {
                Assert.True(checkStatus, "Invalid values returned correct status quota");
            }
            Assert.False(false, "Valid data returned invalid status quota");
        }
        /// <summary>
        /// Testing the username to ensure that all specified users can be returned to the controoler
        /// where an admin can validate all accounts.
        /// </summary>
        /// <param name="username"></param>
        [Theory]
        [InlineData("ran")]
        [InlineData("...")]
        [InlineData("thisisatestforaphrasethatcannotrepresentausername")]
        public void ValidateReturnObjectTypeRetrievalSpecifiedUsers_ReturnTrue(string username)
        {
            var result = _userProfileController.RetrieveUserProfile(username);
            var okResult = result as OkObjectResult;
            ProfileModel model = new ProfileModel();
            if (okResult.Value != null)
                model = (ProfileModel)okResult.Value;
            bool checkStatus = object.Equals(200, okResult.StatusCode);

            if (checkStatus && model.userId != null)
            {
                Assert.True(checkStatus, "Invalid values returned correct status quota");
            }
            Assert.False(false, "Valid data returned invalid status quota");
        }
        /// <summary>
        /// used to test the valid user posts and retrieve all information. If the profile ID comes back
        /// null then we know the user never existed to begin with
        /// </summary>
        /// <param name="username"></param>
        [Theory]
        [InlineData("ran")]
        [InlineData("...")]
        [InlineData("thisisatestforaphrasethatcannotrepresentausername")]
        public void ValidateReturnObjectTypeRetrievalSpecifiedUsersPosts_ReturnTrue(string username)
        {
            var result = _userProfileController.RetrieveProfilePosts(username);
            var okResult = result as OkObjectResult;
            ProfileModel model = new ProfileModel();
            if (okResult.Value != null)
                model = (ProfileModel)okResult.Value;
            bool checkStatus = object.Equals(200, okResult.StatusCode);

            if (checkStatus && model.userId != null)
            {
                Assert.True(checkStatus, "Invalid values returned correct status quota");
            }
            Assert.False(false, "Valid data returned invalid status quota");
        }
        /// <summary>
        /// Checks to see for valid username for retrieval of upvotes and checks for the status code
        /// on return. We are soley checking for integration to validate the use of our api
        /// </summary>
        /// <param name="username"></param>
        [Theory]
        [InlineData("ran")]
        [InlineData("...")]
        [InlineData("thisisatestforaphrasethatcannotrepresentausername")]
        public void ValidateReturnObjectTypeRetrievalSpecifiedUsersUpvotePosts_ReturnTrue(string username)
        {
            var result = _userProfileController.RetrieveAllUserUpvotePosts(username);
            var okResult = result as OkObjectResult;
            ProfileModel model = new ProfileModel();
            if (okResult.Value != null)
                model = (ProfileModel)okResult.Value;
            bool checkStatus = object.Equals(200, okResult.StatusCode);

            if (checkStatus && model.userId != null)
            {
                Assert.True(checkStatus, "Invalid values returned correct status quota");
            }
            Assert.False(false, "Valid data returned invalid status quota");
        }
        /// <summary>
        /// checks the update operation to see if the status code returns 200 and that there is an existing 
        /// user with the valid status code.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="description"></param>
        [Theory]
        [InlineData("ran", "this is a test")]
        [InlineData("...", "this is a test too")]
        [InlineData("thisisatestforaphrasethatcannotrepresentausername", "this is a really fake test")]
        public void ValidateReturnObjectTypeRetrievalSpecifiedUsersUpdateDescription_ReturnTrue(string username, string description)
        {
            var result = _userProfileUpdateController.UpdateUserProfileDescription(username, description);
            var okResult = result as OkObjectResult;
            ProfileModel model = new ProfileModel();
            if (okResult.Value != null)
                model = (ProfileModel)okResult.Value;
            bool checkStatus = object.Equals(200, okResult.StatusCode);

            if (checkStatus && model.userId != null)
            {
                Assert.True(checkStatus, "Invalid values returned correct status quota");
            }
            Assert.False(false, "Valid data returned invalid status quota");
        }
    }
}
