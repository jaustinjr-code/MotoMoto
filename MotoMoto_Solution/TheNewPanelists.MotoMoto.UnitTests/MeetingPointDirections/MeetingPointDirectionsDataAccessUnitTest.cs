using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class MeetingPointDirectionsDataAccessUnitTest
    {
        // Test to determine if the DAO is able to connect to the data store
        [Fact]
        public void ValidateDBConnectionEstablished()
        {
            // Arrange
            MeetingPointDirectionsDataAccess meetingPointDirectionsDAO = new MeetingPointDirectionsDataAccess();
            // Act
            bool result = meetingPointDirectionsDAO.EstablishDBConnection();
            // Assert
            Assert.True(result);
        }

        // Test to determine if a specified event location is retrieved
        [Fact]
        public void IsValidRequest_RetrieveOneRow()
        {
            // Arrange
            ISet<EventDetailsModel>? location = new HashSet<EventDetailsModel>();
            MeetingPointDirectionsDataAccess meetingPointDirectionsDAO = new MeetingPointDirectionsDataAccess();
            bool result;

            // Act
            location = meetingPointDirectionsDAO.FetchEventLocation(1);

            // Assert
            if(location!.Count() == 1)
            {
                result = true;
            } 
            else
            {
                result = false;
            }
            Assert.True(result);
        }

        // Test to determine inability to retrieve event location with invalid eventID
        [Fact]
        public void InvalidRequest_RetrieveOneRow_WrongEventID()
        {
            // Arrange
            ISet<EventDetailsModel>? location = new HashSet<EventDetailsModel>();
            MeetingPointDirectionsDataAccess meetingPointDirectionsDAO = new MeetingPointDirectionsDataAccess();
            bool result;

            // Act
            location = meetingPointDirectionsDAO.FetchEventLocation(-1);

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
