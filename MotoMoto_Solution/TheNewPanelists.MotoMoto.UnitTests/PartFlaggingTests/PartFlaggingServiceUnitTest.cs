using Xunit;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartFlaggingServiceUnitTest
    {
        [Fact]
        public void CallValidFlagCreation()
        {
            const string testId = "6";
            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingService partFlaggingService = new PartFlaggingService();

            bool result = partFlaggingService.CallFlagCreation(testFlag);
            Assert.True(result);
        }

        [Fact]
        public void CallInvalidFlagCreation()
        {
            FlagModel testFlag = new FlagModel();

            PartFlaggingService partFlaggingService = new PartFlaggingService();

            bool result = partFlaggingService.CallFlagCreation(testFlag);
            Assert.False(result);
        }

        [Fact]
        public void CallGetExistingFlagCount()
        {
            const int ZERO = 0;
            FlagModel testFlag = new FlagModel("10", "10", "10", "10");

            PartFlaggingService partFlaggingService = new PartFlaggingService();
            
            //Create flag to ensure that the flag exists
            partFlaggingService.CallFlagCreation(testFlag);

            //If a positive value is returned then the flag count was successfully retrieved
            int result = partFlaggingService.CallGetFlagCount(testFlag);
            Assert.True(result > ZERO);

        }

        [Fact]
        public void CallGetNonExistingFlagCount()
        {
            const int ZERO = 0;
            FlagModel testFlag = new FlagModel("10", "10", "10", "10");

            PartFlaggingService partFlaggingService = new PartFlaggingService();
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();
            
            //Delete flag to ensure that the flag does not exist
            partFlaggingDataAccess.DeleteFlag(testFlag);

            //If a positive value is returned then the flag count was successfully retrieved
            int result = partFlaggingService.CallGetFlagCount(testFlag);
            Assert.True(result == ZERO);
        }

        [Fact]
        public void CallDecrementNonExistingFlagCount()
        {
            const string testName = "CallDecrementNonExistingFlagCount";
            FlagModel testFlag = new FlagModel(testName, testName, testName, "2022");

            PartFlaggingService partFlaggingService = new PartFlaggingService();
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();
            
            //Delete flag to ensure that the flag does not exist
            partFlaggingDataAccess.DeleteFlag(testFlag);

            //If a positive value is returned then the flag count was successfully retrieved
            bool result = partFlaggingService.CallDecrementFlagCount(testFlag);
            Assert.False(result);
        }

        [Theory]
        [InlineData("CallExistingFlagCount", 1)]
        [InlineData("CallExistingFlagCount", 2)]
        public void CallExistingFlagCount(string testName, int minFlagCount)
        {
            FlagModel testFlag = new FlagModel(testName, testName, testName, "2022");
            
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();
            partFlaggingDataAccess.DeleteFlag(testFlag);

            for (int flagCountIt = 0; flagCountIt < minFlagCount; ++flagCountIt)
            {
                partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);
            }

            PartFlaggingService partFlaggingService = new PartFlaggingService();

            int prevCount = partFlaggingDataAccess.GetFlagCount(testFlag);
            partFlaggingService.CallDecrementFlagCount(testFlag);
            int afterCount = partFlaggingDataAccess.GetFlagCount(testFlag);

            Assert.True(prevCount == afterCount + 1);
        }





    }
}