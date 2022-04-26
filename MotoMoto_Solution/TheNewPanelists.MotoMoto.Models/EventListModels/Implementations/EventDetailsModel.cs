using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class EventDetailsModel : IEventDetailsModel
    {
        public int eventID { get; set; }
        public string? eventLocation { get; set; }
        public string? eventTime { get; set; }
        public string? eventDate { get; set; }
        public IEnumerable<string>? registeredUsers { get; set; }
    }
}
