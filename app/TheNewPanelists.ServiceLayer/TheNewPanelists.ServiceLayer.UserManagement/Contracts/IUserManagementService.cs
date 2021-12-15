namespace TheNewPanelists.ServiceLayer.UserManagement {
    interface IServiceLayer
    {
        bool CreateAccountRequest();
        bool DeleteAccount();
        bool UpdateAccount();
        bool EnableAccount();
        bool DisableAccount();
    }
}
