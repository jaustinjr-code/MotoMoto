namespace TheNewPanelists.ServiceLayer.Authentication 
{
    interface IAuthenticationService
    {
        Dictionary<string, string> RequestInput();
    }
}