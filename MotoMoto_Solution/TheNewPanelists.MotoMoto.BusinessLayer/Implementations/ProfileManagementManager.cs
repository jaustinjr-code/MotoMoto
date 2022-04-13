using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class ProfileManagementManager
    {
        private readonly ProfileManagementService? _profileManagementService;

        public ProfileManagementManager(ProfileManagementService profileManagementService)
        {
            _profileManagementService = profileManagementService;
        }

        public bool DeleteProfileManager(DeleteAccountModel deleteAccountModel)
        {
            if (deleteAccountModel.userId < 0)
                return false;
            return _profileManagementService!.DeleteAccountProfile(deleteAccountModel);
        }

        public bool CreateAccountManager()
        {
            return (_profileManagementService!.CreateAccountProfile());
        }
    }
}
