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
    public class EventListService
    {
        // Readonly means that the object/variable cannot be defined outside of the
        // constructor
        private readonly EventPostContentDataAccess _eventPostContentDAO;
        
        // Single argument constructor
        public EventListService(EventPostContentDataAccess eventPostContentDataAccess){ _eventPostContentDAO = eventPostContentDataAccess; }

        // Function to FetchAllEventPosts 
        public ISet<EventDetailsModel> FetchAllEventPosts(EventDetailsModel eventDetails)
        {
            var eventDetailsEntities = _eventPostContentDAO.FetchAllPosts();

            var events = eventDetailsEntities!.Select(evnt => new EventDetailsModel()
            {
                eventID = eventDetails!.eventID,
                eventLocation = eventDetails!.eventLocation,
                eventTime = eventDetails!.eventTime,
                eventDate = eventDetails!.eventDate
            }).ToHashSet();
            return events;
        }
    }
}
