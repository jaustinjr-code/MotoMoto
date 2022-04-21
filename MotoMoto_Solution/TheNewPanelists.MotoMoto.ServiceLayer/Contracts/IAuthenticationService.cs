using System.Collections.Generic;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    interface IAuthenticationService
    {
        Dictionary<string, string> RequestInput();
    }
}