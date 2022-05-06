using Xunit;
using TheNewPanelists.MotoMoto.ServiceLayer;
using System;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.UnitTests.NotificationSystemTests
{
    /// <summary>
    /// Unit tests for the notification system service layer
    /// </summary>
    public class NotificationSystemServiceLayerUnitTest
    {
        [Fact]
        public void IsValidRequest_RetrieveRegisteredEvents()
        {
            // Given
            NotificationSystemService service = new NotificationSystemService();

            // When
            string? username = "";
            var list = service.FetchRegisteredEvents(username);
            bool result;
            if (list.Count == 0 || list == null)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            // Then
            Assert.Equal(true, result);
        }
    }
}