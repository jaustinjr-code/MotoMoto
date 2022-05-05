using MySql.Data.MySqlClient;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using System.Data;
using System.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public interface IProfileDataAccess
    {
        public ProfileModel RetrieveAllUpvotesPostsForProfile(ProfileModel userProfile);
        public ProfileModel RetrieveSpecifiedProfileEntity(ProfileModel userProfile);
        public ProfileListModel GetAllProfiles(ProfileListModel profileListModel);
        public ProfileModel InstertAllExsitingUsers();
        public ProfileModel DeleteProfileDataAccess(ProfileModel userProfile);
        public ProfileModel GetAllUsersPosts(ProfileModel userProfile);
        public ProfileModel UpdateProfileDescription(ProfileModel userProfile);
        public ProfileModel UpdateProfileUsername(ProfileModel userProfile);
        public ProfileModel UpdateProfileImage(ProfileModel userProfile);
        public ProfileModel UpdateProfileStatus(ProfileModel userProfile);
    }
}
