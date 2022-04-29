using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class LogEntryModel
    {
        public int _userId { get; set; }
        public string? _username { get; set; }
        public string? _levelName { get; set; } 
        public string? _categoryName { get; set; }
        public string? _logDescription { get; set; }
    }
}
