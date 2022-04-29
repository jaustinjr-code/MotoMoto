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

            int result = partFlaggingDataAccess.GetFlagCount(testFlag);
            Assert.Equal(result, 0);
        }

        [Fact]
        public void GetCountOfExistingFlag()
        {
            const string testId = "1";
            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            int result = partFlaggingDataAccess.GetFlagCount(testFlag);
            Assert.Equal(result, 100);
        }

        [Fact]
        public void CreateNonexistentFlag()
        {
            const string testId = "2";
            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            bool result = partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);
            
            //Remove flag from the table when unit test is completed, so that upon running again the flag no longer exists
            partFlaggingDataAccess.DeleteFlag(testFlag);

            Assert.True(result);
        }

        [Fact]
        public void IncrementExistingFlag()
        {
            const int ONE = 1;
            const string testId = "3";
            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            int previousCount = partFlaggingDataAccess.GetFlagCount(testFlag);
            bool result = partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);
            int subsequentCount = partFlaggingDataAccess.GetFlagCount(testFlag);

            Assert.Equal(subsequentCount, previousCount + ONE);
        }

        [Fact]
        public void DeleteNonExistentFlag()
        {
            const string testId = "4";

            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            bool result = partFlaggingDataAccess.DeleteFlag(testFlag);
            Assert.False(result);
        }

        [Fact]
        public void DeleteExistingFlag()
        {
            const string testId = "5";

            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);
            bool result = partFlaggingDataAccess.DeleteFlag(testFlag);
            Assert.True(result);
        }

        [Theory]
        [InlineDataAttribute("GreaterThanOne", 2)]
        [InlineDataAttribute("EqualToOne", 1)]
        [InlineDataAttribute("Zero", 0)]
        public void DecrementOrRemoveTests(string testName, int numCreations) 
        {
            bool result = false;
            
            FlagModel testFlag = new FlagModel(testName, testName, testName, "2022");
            
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            bool creationSuccessful = true;
            for (int flagCreateIt = 0; flagCreateIt < numCreations; ++flagCreateIt)
            {
                creationSuccessful = creationSuccessful && partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);
            }

            if (creationSuccessful)
            {
                int prevCount = partFlaggingDataAccess.GetFlagCount(testFlag);
                result = partFlaggingDataAccess.DecrementOrRemove(testFlag);
                int afterCount = partFlaggingDataAccess.GetFlagCount(testFlag);

                if (prevCount == 0)
                {
                    Assert.False(result);
                }
                else
                {
                    Assert.True(result && prevCount == afterCount + 1);
                }
            }
            else
            {
                Assert.True(false);
            }
        }
    }
}