using Xunit;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartFlaggingServiceUnitTest
    {
        /// <summary>
        /// Uses service layer to create a valid flag.
        /// Test is successful if flag creation is successful.
        /// </summary>  
        [Fact]
        public void CallValidFlagCreation()
        {
            const string testId = "6";
            FlagModel testFlag = new FlagModel(testId, testId, testId, testId);

            PartFlaggingService partFlaggingService = new PartFlaggingService();

            bool result = partFlaggingService.CallFlagCreation(testFlag);
            Assert.True(result);
        }

        /// <summary>
        /// Uses service layer to create an invalid flag.
        /// Test is successful if flag creation is not successful.
        /// </summary>  
        [Fact]
        public void CallInvalidFlagCreation()
        {
            FlagModel testFlag = new FlagModel();

            PartFlaggingService partFlaggingService = new PartFlaggingService();

            bool result = partFlaggingService.CallFlagCreation(testFlag);
            Assert.False(result);
        }

        /// <summary>
        /// Uses service layer to call the get flag count function for an existing flag and attain its result.
        /// Test is successful if count is greater than 0 because the flag exists
        /// </summary>  
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

        /// <summary>
        /// Uses service layer to call the get flag count function for a nonexisting flag and attain its result.
        /// Test is successful if count is equal to 0 because the flag does not exist
        /// </summary>  
        [Fact]
        public async void CallGetNonExistingFlagCount()
        {
            const int ZERO = 0;
            FlagModel testFlag = new FlagModel("10", "10", "10", "10");

            PartFlaggingService partFlaggingService = new PartFlaggingService();
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();
            
            //Delete flag to ensure that the flag does not exist
            await partFlaggingDataAccess.DeleteFlag(testFlag);

            //If a positive value is returned then the flag count was successfully retrieved
            int result = partFlaggingService.CallGetFlagCount(testFlag);
            Assert.True(result == ZERO);
        }

        /// <summary>
        /// Uses service layer to call the decrement flag count function in the 
        /// data access layer.
        /// Test passes if the result is false because the flag does not exist
        /// and cannot be decremented.
        /// </summary>  
        [Fact]
        public async void CallDecrementNonExistingFlagCount()
        {
            const string testName = "CallDecrementNonExistingFlagCount";
            FlagModel testFlag = new FlagModel(testName, testName, testName, "2022");

            PartFlaggingService partFlaggingService = new PartFlaggingService();
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();
            
            //Delete flag to ensure that the flag does not exist
            await partFlaggingDataAccess.DeleteFlag(testFlag);

            //If a positive value is returned then the flag count was successfully retrieved
            bool result = partFlaggingService.CallDecrementFlagCount(testFlag);
            Assert.False(result);
        }

        /// <summary>
        /// Uses service layer to call the decrement flag count function in the 
        /// data access layer for flags with count greater than one and count
        /// equal to one. These two cases are tested because these scenarios have
        /// different logic.
        /// Test is sucessful if the after count is one less than the count before decrement
        /// </summary>  
        [Theory]
        [InlineData("CallDecrementExistingFlagCount", 1)]
        [InlineData("CallDecrementExistingFlagCount", 2)]
        public async void CallDecrementExistingFlagCount(string testName, int minFlagCount)
        {
            FlagModel testFlag = new FlagModel(testName, testName, testName, "2022");
            
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();
            await partFlaggingDataAccess.DeleteFlag(testFlag);

            for (int flagCountIt = 0; flagCountIt < minFlagCount; ++flagCountIt)
            {
                await partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);
            }

            PartFlaggingService partFlaggingService = new PartFlaggingService();

            int prevCount = await partFlaggingDataAccess.GetFlagCount(testFlag);
            partFlaggingService.CallDecrementFlagCount(testFlag);
            int afterCount = await partFlaggingDataAccess.GetFlagCount(testFlag);

            Assert.True(prevCount == afterCount + 1);
        }





    }
}