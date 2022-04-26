using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.ServiceLayer;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class EventListManager
    {
        // Create private readonly property for EventListService
        private readonly EventListService _eventListService;

        // Single argument constructor
        public EventListManager(EventListService eventListService){_eventListService = eventListService;}
    }
}
