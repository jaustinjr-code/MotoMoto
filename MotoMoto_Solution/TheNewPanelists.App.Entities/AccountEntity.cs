using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.App.DataStoreEntities;

namespace TheNewPanelists.App.Entities
{
    public class AccountEntity : IBaseUser
    {
        public int AccountType { get; set; }

        public string? username { get; set; }

        public bool eventAccount { get; set; }
    }
}
