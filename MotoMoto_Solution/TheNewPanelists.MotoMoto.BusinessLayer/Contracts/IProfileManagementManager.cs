using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public interface IProfileManagementManager
    {
        public ProfileModel CreatProfilesForAllNewAccountsManager();
        public ProfileListModel RetrieveAllProfileManager();
        public ProfileModel RetrieveSpecifiedProfileManager(string _username);
        public ProfileModel RetrieveAllUpVotedPostsForProfileManager(string _username);
        public ProfileModel UpdateProfileDescriptionManager(string _username, string _newDescription);
        public ProfileModel UpdateProfileUsernameManager(string _username, string _newUsername);
        public ProfileModel UpdateProfileStatusManager(string _username, bool _status);
        public ProfileModel DeleteProfileManager(string _username);
    }
}
