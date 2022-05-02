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
        public ISet<EventDetailsModel> FetchEventLocation()
        {
            // Use the DAO object to retrieve the event location
            var eventDetailsEntities = _meetingPointDirectionsDAO.FetchEventLocation();

            // Selects each row from the retrieved HashSet and stores it 
            var location = eventDetailsEntities!.Select(loc => new EventDetailsModel()
            {
                streetAddress = loc!.streetAddress,
                city = loc!.city,
                state = loc!.state,
                country = loc!.country,
                zipCode = loc!.zipCode
            }).ToHashSet();
            return location; // Returns the retrieved data back to the manager
        }

    }
}
