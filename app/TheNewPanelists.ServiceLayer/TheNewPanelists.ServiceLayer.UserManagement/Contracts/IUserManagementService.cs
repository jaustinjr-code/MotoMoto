namespace TheNewPanelists.ServiceLayer.UserManagement {
    interface IServiceLayer
    {
        bool CreateAccount();
        bool DeleteAccount();
        bool UpdateAccount();
        bool EnableAccount();
        bool DisableAccount();
    }
}
