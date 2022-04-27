using Xunit;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartFlaggingDataAccessUnitTest
    {
        [Fact]
        public void GetCountOfNonexistentFlag()
        {
            const string testId = "0";
            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            int result = partFlaggingDataAccess.getFlagCount(testFlag);
            Assert.Equal(result, 0);
        }

        [Fact]
        public void GetCountOfExistingFlag()
        {
            const string testId = "1";
            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            int result = partFlaggingDataAccess.getFlagCount(testFlag);
            Assert.Equal(result, 100);
        }

        [Fact]
        public void CreateNonexistentFlag()
        {
            const string testId = "2";
            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            bool result = partFlaggingDataAccess.createOrIncrementFlag(testFlag);
            
            //Remove flag from the table when unit test is completed, so that upon running again the flag no longer exists
            partFlaggingDataAccess.deleteFlag(testFlag);

            Assert.True(result);
        }

        [Fact]
        public void IncrementExistingFlag()
        {
            const int ONE = 1;
            const string testId = "3";
            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            int previousCount = partFlaggingDataAccess.getFlagCount(testFlag);
            bool result = partFlaggingDataAccess.createOrIncrementFlag(testFlag);
            int subsequentCount = partFlaggingDataAccess.getFlagCount(testFlag);

            Assert.Equal(subsequentCount, previousCount + ONE);
        }

        [Fact]
        public void DeleteNonExistentFlag()
        {
            const string testId = "4";

            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            bool result = partFlaggingDataAccess.deleteFlag(testFlag);
            Assert.False(result);
        }

        [Fact]
        public void DeleteExistingFlag()
        {
            const string testId = "5";

            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            partFlaggingDataAccess.createOrIncrementFlag(testFlag);
            bool result = partFlaggingDataAccess.deleteFlag(testFlag);
            Assert.True(result);
        }
        
    }
}