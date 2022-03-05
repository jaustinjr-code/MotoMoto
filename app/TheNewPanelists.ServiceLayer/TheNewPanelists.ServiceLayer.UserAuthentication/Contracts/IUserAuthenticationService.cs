namespace TheNewPanelists.ServiceLayer.UserAuthentication 
{
    interface IUserAuthenticationService
    {
        bool validateRequest();
        bool SqlGenerator();
    }
}