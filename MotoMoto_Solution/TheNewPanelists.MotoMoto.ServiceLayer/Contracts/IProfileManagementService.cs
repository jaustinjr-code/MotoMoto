using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface IProfileManagementService
    {
        public ProfileModel CreateProfilesForAllNewUsersManager();
        public ProfileModel RetrieveSpecifiedProfileEntity(ProfileModel profileModel);
        public ProfileListModel RetrieveAllProfileModels();
        public ProfileModel RetrieveAllUpvotesPostsForProfile(ProfileModel profileModel);
        public ProfileModel UpdateProfileDescriptionService(ProfileModel profileModel);
        public ProfileModel UpdateProfileUsernameService(ProfileModel profileModel);
        public ProfileModel UpdateProfileStatus(ProfileModel profileModel);
        public ProfileModel UpdateProfileImage(ProfileModel profileModel);
        public ProfileModel DeleteProfileService(ProfileModel profileModel);
        public ProfileModel RetrieveSpecifiedUserPosts(ProfileModel profileModel);
    }
}
