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

        // Readonly means that the object/variable cannot be defined outside of the
        // constructor
        private readonly MeetingPointDirectionsDataAccess _meetingPointDirectionsDAO;

        // Single argument constructor
        public MeetingPointDirectionsService(MeetingPointDirectionsDataAccess meetingPointDirectionsDataAccess) { _meetingPointDirectionsDAO = meetingPointDirectionsDataAccess; }

        // Function to FetchAllEventPosts 
        public ISet<EventDetailsModel> FetchEventLocation(int eventID)
        {
            // Use the DAO object to retrieve the event location
            var eventDetailsEntities = _meetingPointDirectionsDAO.FetchEventLocation(eventID);

            // Selects each row from the retrieved HashSet and stores it 
            var location = eventDetailsEntities!.Select(loc => new EventDetailsModel()
            {
                eventStreetAddress = loc!.eventStreetAddress,
                eventCity = loc!.eventCity,
                eventState = loc!.eventState,
                eventCountry = loc!.eventCountry,
                eventZipCode = loc!.eventZipCode
            }).ToHashSet();
            return location; // Returns the retrieved data back to the manager
        }

    }
}
