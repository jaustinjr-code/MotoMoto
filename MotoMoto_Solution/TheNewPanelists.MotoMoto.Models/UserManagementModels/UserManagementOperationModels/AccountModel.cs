using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Models
{
    public class AccountModel
    {
        public string? _accountType { get; set; }
        public string? _username { get; set; }
        public string? _newUsername { get; set; }
    }
}
