using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Entities
{
    public class ProfileEntity : IBaseUser
    {
        public string? username { get; set; }
        public bool status { get; set; }
        public bool eventAccount { get; set; }
    }
}
