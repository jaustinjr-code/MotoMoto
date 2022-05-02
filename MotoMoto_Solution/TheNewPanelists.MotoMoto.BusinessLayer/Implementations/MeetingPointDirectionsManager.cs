using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class MeetingPointDirectionsManager
    {
        // Create private readonly property for EventListService
        private readonly MeetingPointDirectionsService _meetingPointDirectionsService;

        // Single argument constructor
        public MeetingPointDirectionsManager(MeetingPointDirectionsService meetingPointDirectionsService) { _meetingPointDirectionsService = meetingPointDirectionsService; }

        // Function that will be used to Fetch event location from the datastore
        public ISet<EventDetailsModel> FetchEventLocation()
        {
            return _meetingPointDirectionsService.FetchEventLocation();
        }
    }
}
