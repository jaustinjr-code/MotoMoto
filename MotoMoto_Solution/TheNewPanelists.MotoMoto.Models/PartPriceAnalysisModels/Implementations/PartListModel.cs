using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class PartListModel
    {
        public string? _partCategory { get; set; }
        public List<PartModel> _partList = new List<PartModel>();
    }
}
