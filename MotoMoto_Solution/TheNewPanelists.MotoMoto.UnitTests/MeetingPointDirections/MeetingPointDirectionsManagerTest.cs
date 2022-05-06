using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class MeetingPointDirectionsManagerTest
    {
        // Test to determine if Business layer is able to retrieve information sent from Service layer with valid EventID
        [Fact]
        public void RetrieveDataFromService_ValidEventID()
        {
            // Arrange
            MeetingPointDirectionsDataAccess meetingPointDirectionsDAO = new MeetingPointDirectionsDataAccess();
            MeetingPointDirectionsService meetingPointDirectionsService = new MeetingPointDirectionsService(meetingPointDirectionsDAO);
            MeetingPointDirectionsManager meetingPointDirectionsManager = new MeetingPointDirectionsManager(meetingPointDirectionsService);
            ISet<EventDetailsModel>? location = new HashSet<EventDetailsModel>();
            bool result;

            // Act
            location = meetingPointDirectionsManager.FetchEventLocation(1);

            // Assert
            if (location!.Count() == 1)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            Assert.True(result);
        }

        // Test to determine if Business layer is unable to retrieve information sent from Service layer with invalid EventID
        [Fact]
        public void RetrieveDataFromService_InvalidEventID()
        {
            // Arrange
            MeetingPointDirectionsDataAccess meetingPointDirectionsDAO = new MeetingPointDirectionsDataAccess();
            MeetingPointDirectionsService meetingPointDirectionsService = new MeetingPointDirectionsService(meetingPointDirectionsDAO);
            MeetingPointDirectionsManager meetingPointDirectionsManager = new MeetingPointDirectionsManager(meetingPointDirectionsService);
            ISet<EventDetailsModel>? location = new HashSet<EventDetailsModel>();
            bool result;

            // Act
            location = meetingPointDirectionsManager.FetchEventLocation(-1);

            // Assert
            if (location!.Count() == 1)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            Assert.False(result);
        }
    }
}
