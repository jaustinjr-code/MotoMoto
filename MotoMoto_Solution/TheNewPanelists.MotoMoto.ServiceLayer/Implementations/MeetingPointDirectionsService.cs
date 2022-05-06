using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class MeetingPointDirectionsService
    {
        private readonly MeetingPointDirectionsDataAccess _meetingPointDirectionsDAO;
        public MeetingPointDirectionsService(MeetingPointDirectionsDataAccess meetingPointDirectionsDataAccess) { _meetingPointDirectionsDAO = meetingPointDirectionsDataAccess; }

        // Function that retrieves fetched rows from Data Access and stores it into a Set
        public ISet<EventDetailsModel>? FetchEventLocation(int eventID)
        {
            try
            {
                var eventDetailsEntities = _meetingPointDirectionsDAO.FetchEventLocation(eventID);

                var location = eventDetailsEntities!.Select(loc => new EventDetailsModel()
                {
                    eventStreetAddress = loc!.eventStreetAddress,
                    eventCity = loc!.eventCity,
                    eventState = loc!.eventState,
                    eventCountry = loc!.eventCountry,
                    eventZipCode = loc!.eventZipCode
                }).ToHashSet();
                return location;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("ERROR: Could not retrieve information...", ex);
                // Log here
            }
        }
    }
}
