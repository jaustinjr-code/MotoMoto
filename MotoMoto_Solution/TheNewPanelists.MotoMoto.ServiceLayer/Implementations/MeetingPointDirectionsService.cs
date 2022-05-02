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
        private readonly EventPostContentDataAccess _eventPostContentDAO;

        // Single argument constructor
        public MeetingPointDirectionsService(EventPostContentDataAccess eventPostContentDataAccess) { _eventPostContentDAO = eventPostContentDataAccess; }

        // Function to FetchAllEventPosts 
        public ISet<EventDetailsModel> FetchEventLocation()
        {
            // Use the DAO object to retrieve all rows from the EventDetails table and store it in a HashSet
            var eventDetailsEntities = _eventPostContentDAO.FetchAllPosts();

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
