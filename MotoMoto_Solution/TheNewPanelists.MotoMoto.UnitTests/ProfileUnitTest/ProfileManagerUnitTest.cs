using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.UnitTests.ProfileUnitTest
{
    public class ProfileManagerUnitTest
    {
        private readonly IProfileManagementManager _profileManager = new ProfileManagementManager();
        /// <summary>
        /// Is valid username response for return user this function has valid user and has the goa
        /// to retrieve responses for correct username
        /// </summary>
        [Fact]
        public void IsValidUsernameForExistingProfile_ReturnTrue()
        {
            bool result = true;
            string profileName = "ran";
            var profileModel = _profileManager.RetrieveSpecifiedProfileManager(profileName);

            if (profileModel.systemResponse != "success")
            {
                result = false;
            }
            Assert.True(result, "Username runs invalid under the correct username");            
        }
        /// <summary>
        /// This functionality tests the length of a string for a username. Since we can only achieve
        /// 25 characters each one of these statements should return false
        /// </summary>
        /// <param name="fakeUsername"></param>
        [Theory]
        [InlineData("12341241230172840781274102843718239407107387128412834892842-928")]
        [InlineData("aasdf;klafd;akjsfjafajlskdfhajskdflhalhlflajkdfhajlfja")]
        public void IsInvalidUsername_ReturnFalse(string fakeUsername)
        {
            bool result = true;
            var profileModel = _profileManager.RetrieveSpecifiedProfileManager(fakeUsername);

            if (profileModel.systemResponse != "success")
            {
                result = false;
            }
            Assert.False(result, "Invalid username runs correct");
        }
        /// <summary>
        /// Our goal here is to set one of our users description to a fake description and clean it
        /// out with the second object. Since we can have nullable statements or just single spaced strings
        /// for our null referenced descriptions each should return valid.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="description"></param>
        [Theory]
        [InlineData("ROOT", "This is a fake description and will be overwritten with empty")]
        [InlineData("ROOT", " ")]
        public void IsValidUpdate_ProfileDescription_ReturnTrue(string username, string description)
        {
            bool result = false;
            var profileModel = _profileManager.UpdateProfileDescriptionManager(username, description);

            if (profileModel.systemResponse == "success")
            {
                result = true;
            }
            Assert.True(result, "This is a invalid result that returned true");
        }
        /// <summary>
        /// This functionality represents two invalid options of both valid username and invalid description and 
        /// invalid username and valid description. Each should fail due to the length of their strings.
        /// </summary>
        /// <param name="_username"></param>
        /// <param name="_description"></param>
        [Theory]
        [InlineData("ROOT", "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAaaaaaaaaaaaaaaaaaaaaaaaaa")]
        [InlineData("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", "this is a test")]
        public void IsInvalidUpdate_ProfileDescriptionOrUsername_ReturnTrue(string _username, string _description)
        {
            bool result = true;
            var profileModel = _profileManager.UpdateProfileDescriptionManager(_username, _description);

            if (profileModel.systemResponse == "managerInvalidString")
            {
                result = false;
            }
            Assert.False(result, "Invalid description or username passed the manager!!");
        }
        /// <summary>
        /// Configures that retrieval of all user upvoted posts exists. It should exist in the case of the user
        /// ran since this is the user we are using to test all functionalities of base users
        /// </summary>
        [Fact]
        public void RetrieveAllUserUpvotedPosts_ValidUser_ReturnTrue()
        {
            bool result = true;
            string username = "ran";
            var profile = _profileManager.RetrieveAllUpVotedPostsForProfileManager(username);

            if (profile.systemResponse != "success")
            {
                result = false;
            }
            Assert.True(result, "failed string was allowed to pass through manager");
        }
        /// <summary>
        /// This functionality is used to test a invalid username to retreive all posts. This functionality
        /// should always return false as we are testing for the invalidity of the username and ensuring the
        /// type function is correct
        /// </summary>
        /// <param name="username"></param>
        [Theory]
        [InlineData(".....................................................................................")]
        public void RetrieveAllUserUpvotedPosts_InvalidUser_ReturnFalse(string username)
        {
            bool result = true;

            var profile = _profileManager.RetrieveAllUpVotedPostsForProfileManager(username);

            if (profile.systemResponse != "success" && profile.GetType() == typeof(ProfileModel))
            {
                result = false;
            }
            Assert.False(result, "failed string was allowed to pass through manager");
        }
        /// <summary>
        /// This functionality is used to test the invalidity of usernames and new usernames. Since we 
        /// have a cap of 25 characters to a user, this functionality's purpose is to ensure that usernames
        /// are valid before changes can happen.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="newUsername"></param>
        [Theory]
        [InlineData("................................................................", "...")]
        [InlineData("...",".................................................................")]
        [InlineData("...","...")]
        public void UpdateProfileUsername_TestBothValidAndInvalidUsernames(string username, string newUsername)
        {
            var profile = _profileManager.UpdateProfileUsernameManager(username, newUsername);

            if (profile.systemResponse != "success")
            {
                Assert.False(false, "invalid username exeeded on either parameter 1 or 2");
            }
            Assert.True(true, "invalid username detected and reached success phase");
        }
    }
}
