using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Models
{
    public class ProfileModel
    {
        public string? Username { get; set; }
        public bool Status { get; set; }
        public bool EventAccount { get; set; }
    }
}
