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
        public ISet<EventDetailsModel> FetchAllEventDetails()
        {
            // EventDetailsModel which will be used to store the fetched results from the database
            //var eventDetailsModel = new EventDetailsModel(){};
            //return _eventListService.FetchAllEventPosts(eventDetailsModel);
            return _eventListService.FetchAllEventPosts();
        }

        /// <summary>
        /// Function that will be used to POST the created event into the datastore
        /// Validate user input
        /// if true then call the service
        /// </summary>
        /// <param name="time"></param>
        /// <param name="date"></param>
        /// <param name="streetAddress"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="zipCode"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public EventDetailsModel CreateEventPost(string time, string date, string streetAddress, string city, string state, string country, string zipCode, string title)
        {
            try
            {
                // Call function to validate input
                bool result = ValidateInput(time, date, streetAddress, city, state, country, zipCode, title);

                if (result)
                {
                    // Create EventDetails model using passed in user input
                    EventDetailsModel eventDetailsModel = new EventDetailsModel
                    {
                        eventTime = time,
                        eventDate = date,
                        eventStreetAddress = streetAddress,
                        eventCity = city,
                        eventState = state,
                        eventCountry = country,
                        eventZipCode = zipCode,
                        eventTitle = title
                    };

                    // if all validate input is true then call service
                    return _eventListService.CreateEventPost(eventDetailsModel);
                }
                else
                {
                    return new EventDetailsModel().GetResponse(ResponseModel.response.invalidStringParameter);
                }
            }
            catch
            {
                return new EventDetailsModel().GetResponse(ResponseModel.response.managerObjectFailOnRetrieval);
            }
        }

        /// <summary>
        /// Determines if any field inputted by the user is null
        /// If one of the input is null then return false
        /// Else return true
        /// </summary>
        /// <param name="time"></param>
        /// <param name="date"></param>
        /// <param name="streetAddress"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="country"></param>
        /// <param name="zipCode"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public bool ValidateInput(string time, string date, string streetAddress, string city, string state, string country, string zipCode, string title)
        {
            if (time != null || date != null || streetAddress != null || city != null || state != null || country != null || zipCode != null || title != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
