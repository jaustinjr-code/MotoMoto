using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DSConfirmedAccount : iBaseRegistration
    {
        public int? RegistrationId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}

