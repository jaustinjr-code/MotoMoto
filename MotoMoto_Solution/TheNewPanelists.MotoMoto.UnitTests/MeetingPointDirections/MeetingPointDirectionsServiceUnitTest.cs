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

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class MeetingPointDirectionsServiceUnitTest
    {
        // Test to determine if Service layer can retrieve data from DAO with valid EventID
        [Fact]
        public void RetrieveInformationFromDataAccess_ValidEventID()
        {
            // Arrange
            MeetingPointDirectionsDataAccess meetingPointDirectionsDAO = new MeetingPointDirectionsDataAccess();
            MeetingPointDirectionsService meetingPointDirectionsService = new MeetingPointDirectionsService(meetingPointDirectionsDAO);
            ISet<EventDetailsModel>? location = new HashSet<EventDetailsModel>();
            bool result;

            // Act
            location = meetingPointDirectionsService.FetchEventLocation(1);

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

        // Test to determine that Service layer will not receive data with an invalid EventID
        [Fact]
        public void RetrieveInformationFromDataAccess_InvalidEventID()
        {
            // Arrange
            MeetingPointDirectionsDataAccess meetingPointDirectionsDAO = new MeetingPointDirectionsDataAccess();
            MeetingPointDirectionsService meetingPointDirectionsService = new MeetingPointDirectionsService(meetingPointDirectionsDAO);
            ISet<EventDetailsModel>? location = new HashSet<EventDetailsModel>();
            bool result;

            // Act
            location = meetingPointDirectionsService.FetchEventLocation(-1);

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
