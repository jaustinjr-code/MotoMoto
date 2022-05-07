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
        private readonly MeetingPointDirectionsService _meetingPointDirectionsService;
        public MeetingPointDirectionsManager(MeetingPointDirectionsService meetingPointDirectionsService) { _meetingPointDirectionsService = meetingPointDirectionsService; }

        // Initiates the execution of fetching the event location using the specified eventID
        public ISet<EventDetailsModel>? FetchEventLocation(int eventID)
        {
            return _meetingPointDirectionsService.FetchEventLocation(eventID);
        }
    }
}
