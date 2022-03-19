using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Entities
{
    public class AccountEntity : IBaseUser
    {
        public string? AccountType { get; set; }
        public string? username { get; set; }

    }
}
