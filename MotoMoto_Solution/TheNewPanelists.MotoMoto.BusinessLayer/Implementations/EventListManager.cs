using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class EventListManager
    {
        // Create private readonly property for EventListService
        private readonly EventListService _eventListService;

        // Single argument constructor
        public EventListManager(EventListService eventListService){_eventListService = eventListService;}

        // Function that will be used to FetchAllEventDetails from the datastore using the EventListService object
        public ISet<EventDetailsModel> FetchAllEventDetails(int evntID)
        {
            // EventDetailsModel which will be used to store the fetched results from the database
            var eventDetailsModel = new EventDetailsModel()
            {
                eventID = evntID,
                eventLocation = "",
                eventTime = "",
                eventDate = ""
            };
            return _eventListService.FetchAllEventPosts(eventDetailsModel);
        }
    }
}
