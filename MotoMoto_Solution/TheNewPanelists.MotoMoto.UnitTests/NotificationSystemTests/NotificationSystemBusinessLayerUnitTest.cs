using Xunit;
using TheNewPanelists.MotoMoto.BusinessLayer;
using System;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.UnitTests.NotificationSystemTests
{
    /// <summary>
    /// Unit tests for the notification system business layer
    /// </summary>
    public class NotificationSystemBusinessLayerUnitTest
    {
        [Fact]
        public void IsValid_RetrieveRegisteredEvents()
        {
            // Given
            NotificationSystemManager manager = new NotificationSystemManager();

            // When
            string? username = "";
            var list = manager.RetrieveRegisteredEvents(username);
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