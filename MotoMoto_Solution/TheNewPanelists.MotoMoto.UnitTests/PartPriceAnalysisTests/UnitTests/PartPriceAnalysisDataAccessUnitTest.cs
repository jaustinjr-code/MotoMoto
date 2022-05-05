using Xunit;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartPriceAnalysisDataAccessUnitTest
    {

        /// <summary>
        /// Unit testing to see if our connection is valid. In the case that there is a downed
        /// datastore we use this test to determine our network datastore status
        /// </summary>
        [Fact]
        public void PartPriceAnalysisDataAccess_ValidMariaDBConnectionTest_ReturnTrue() 
        {
             IPartPriceAnalysisDataAccess _partPriceAnalysisDAO = new PartPriceAnalysisDataAccess();
        bool validConnection = _partPriceAnalysisDAO.EstablishMariaDBConnection();
            bool _dataAccessReturnValue = false;
            if (validConnection)
            {
                _dataAccessReturnValue = true;
            }
            Assert.True(_dataAccessReturnValue, "Connection with Default Connection String Failed!! Please Refer to PPA_DAL");
        }
        /// <summary>
        /// Unit testing with invalid connection string to see if we can still retrieve access to our
        /// data store. If this fails then there is an issue with connection string assignment in the 
        /// PPA_DAL
        /// </summary>
        [Theory]
        [InlineData("testStringThisShouldFail")]
        [InlineData("server=fake-connection.fake-localhost.com;user=thisIsFake;database=fakeDB;port=3306;password=fakePassword;")]
        public void PartPriceAnalysisDataAccess_InalidMariaDBConnectionTest_ReturnTrue(string _testString)
        {
            IPartPriceAnalysisDataAccess _partPriceAnalysisDAO = new PartPriceAnalysisDataAccess(_testString);
            bool validConnection = _partPriceAnalysisDAO.EstablishMariaDBConnection();
            bool _dataAccessReturnValue = false;
            if (validConnection)
            {
                _dataAccessReturnValue = true;
            }
            Assert.False(_dataAccessReturnValue, "Connection with Fake Connection String Failed!! If passed please Refer to PPA_DAL");
        }
    }
}
