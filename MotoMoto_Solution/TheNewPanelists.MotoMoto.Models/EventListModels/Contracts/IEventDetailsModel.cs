using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public interface IEventDetailsModel
    {
        int eventID { get; }
        string? eventLocation { get; set; }
        string? eventTime { get; set; }
        string? eventDate { get; set; }
        IEnumerable<string>? registeredUsers { get; set; }

    }
}
