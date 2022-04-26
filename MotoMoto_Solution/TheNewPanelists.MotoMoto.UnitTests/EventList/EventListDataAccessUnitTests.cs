using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataAccess;
using Xunit;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class EventListDataAccessUnitTests
    {
        [Fact]
        public void IsEventPostContentDBConnectionEstablished()
        {
            // Arrange
            IDataAccess eventPostContentAccess = new EventPostContentDataAccess();
            // Act
            bool result = eventPostContentAccess.EstablishMariaDBConnection();
            // Assert
            Assert.True(result);
        }
    }
}
