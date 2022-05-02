using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Models
{
    public class MeetingPointDirectionsModel : IEventDetailsModel
    {
        public int eventID { get; set; }
        public string? streetAddress { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }
        public string? country { get; set; }
        public string? zipCode { get; set; }
    }
}
