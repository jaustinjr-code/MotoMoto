using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class DeleteAccountModel
    {
        public int _userId { get; set; }
        public string? _username { get; set; }
        public string? _verifiedPassword { get; set; }
    }
}
