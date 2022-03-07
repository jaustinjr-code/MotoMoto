using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.BusinessLayer.EventAccountVerification
{
    interface IEvntAccntVerifManager
    {
        bool isValidRequest(Dictionary<IEvntAccntVerifManager, String> request);

    }
}
