using Xunit;

namespace TheNewPanelists.DataAccessLayer
{
    public class LoggingDataAccessTest
    {
        public LoggingDataAccessTest()
        {

        }

        [Fact]
        public void Test1()
        {
            IDataAccess d = new LoggingDataAccess("CREATE", false);
            Assert.Equal(true, d.EstablishMariaDBConnection());
        }
    }
}
