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
        public ProfileModel RetrieveSpecifiedProfileEntity(string _username);
        public ProfileListModel RetrieveAllProfileModels();
        public ProfileModel RetrieveAllUpvotesPostsForProfile(string _username);
        public ProfileModel UpdateProfileDescriptionService(string _username, string _newDescription);
        public ProfileModel UpdateProfileUsernameService(string _username, string _newUsername);
        public ProfileModel UpdateProfileStatus(string _username, bool _status);

    }
}
