using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreUser : IBaseUser
    {
        public string? userType { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public string? email { get; set; }
        public string? salt { get; set; }
    }
}