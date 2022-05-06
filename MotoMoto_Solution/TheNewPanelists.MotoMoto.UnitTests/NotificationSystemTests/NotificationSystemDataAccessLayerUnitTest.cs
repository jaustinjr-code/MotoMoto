using Xunit;
using TheNewPanelists.MotoMoto.DataAccess;
using MySql.Data.MySqlClient;
using System;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.UnitTests.NotificationSystemTests
{
    /// <summary>
    /// Unit tests for the notification system business layer
    /// </summary>
    public class NotificationSystemDataAccessLayerUnitTest
    {
        // Makign sure MariaDBConnection is working
        [Fact]
        public void IsValidMySqlConnection_EstablishMariaDBConnection()
        {
            // Given
            NotificationSystemDataAccess dataAccess = new NotificationSystemDataAccess();

            // When
            bool result = dataAccess.EstablishMariaDBConnection();

            // Then
            Assert.True(result);
        }
    }
}