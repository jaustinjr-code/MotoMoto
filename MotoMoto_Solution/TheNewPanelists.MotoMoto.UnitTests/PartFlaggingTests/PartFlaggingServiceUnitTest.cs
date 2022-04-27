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

            bool result = partFlaggingService.callFlagCreation(testFlag);
            Assert.True(result);
        }

        [Fact]
        public void CallInvalidFlagCreation()
        {
            FlagModel testFlag = new FlagModel();

            PartFlaggingService partFlaggingService = new PartFlaggingService();

            bool result = partFlaggingService.callFlagCreation(testFlag);
            Assert.False(result);
        }

        [Fact]
        public void CallGetExistingFlagCount()
        {
            const int ZERO = 0;
            FlagModel testFlag = new FlagModel("10", "10", "10", "10");

            PartFlaggingService partFlaggingService = new PartFlaggingService();
            
            //Create flag to ensure that the flag exists
            partFlaggingService.callFlagCreation(testFlag);

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
            
            //Create flag to ensure that the flag exists
            partFlaggingDataAccess.deleteFlag(testFlag);

            //If a positive value is returned then the flag count was successfully retrieved
            int result = partFlaggingService.CallGetFlagCount(testFlag);
            Assert.True(result == ZERO);
        }
    }
}