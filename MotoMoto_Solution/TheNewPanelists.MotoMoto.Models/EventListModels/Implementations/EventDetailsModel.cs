using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;


namespace TheNewPanelists.MotoMoto.Models
{
    public class EventDetailsModel : IEventDetailsModel
    {
        public int eventID { get; set; }
        public string? eventLocation { get; set; }
        public string? eventTime { get; set; }
        public string? eventDate { get; set; }
        public IEnumerable<string>? registeredUsers { get; set; }

        // USED FOR MEETING POINT DIRECTIONS
        public string? streetAddress { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public string? zipCode { get; set; }
    }
}
