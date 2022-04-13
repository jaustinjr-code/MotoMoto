using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class DeleteAccountModel
    {
        public int userId { get; set; }
        public string? Username { get; set; }
        public string? VerifiedPassword { get; set; }
    }
}
