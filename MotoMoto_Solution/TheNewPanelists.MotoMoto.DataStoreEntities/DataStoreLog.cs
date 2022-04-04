using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class DataStoreLog
    {
        int LogId { get; set; }
        string? LevelName { get; set; }
        string? CategoryName { get; set; }
        string? _dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
        string? Description { get; set; }
    }
}
