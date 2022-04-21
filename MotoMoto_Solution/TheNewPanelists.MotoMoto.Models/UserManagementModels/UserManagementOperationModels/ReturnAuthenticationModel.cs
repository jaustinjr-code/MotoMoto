using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class ReturnAuthenticationModel
    {
        public AuthenticationModel? _authenticationModel { get; set; }
        public bool flag = false;
        public void IsBooleanReturned(bool isFalse)
        {
            switch (isFalse)
            {
                case false:
                    flag = false;
                    break;
                case true:
                    flag = true;
                    break;
            }
        }
    }
}

