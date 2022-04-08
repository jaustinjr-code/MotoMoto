using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreUser : IBaseUser
    {
        public string? _userType { get; set; }
        public string? _username { get; set; }
        public string? _password { get; set; }
        public string? _email { get; set; }
        public string? _salt { get; set; }
    }
}