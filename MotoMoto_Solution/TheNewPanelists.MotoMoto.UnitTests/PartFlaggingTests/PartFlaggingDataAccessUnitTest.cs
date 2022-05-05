using Xunit;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartFlaggingDataAccessUnitTest
    {
        /// <summary>
        /// Uses data access layer to get count of nonexistent flag.
        /// Test passes if count is 0.
        /// </summary>
        [Fact]
        public async void GetCountOfNonexistentFlag()
        {
            const string TEST_ID = "0";
            FlagModel testFlag = new FlagModel(TEST_ID, TEST_ID, TEST_ID, TEST_ID);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            //Manually delete flag in case it exists
            await partFlaggingDataAccess.DeleteFlag(testFlag);

            int result = await partFlaggingDataAccess.GetFlagCount(testFlag);
            Assert.Equal(result, 0);
        }

        /// <summary>
        /// Uses data access layer to get count of existing flag with reserved count of 100.
        /// Test passes if count is 100.
        ///
        /// IMPORTANT: Do not modify flag with part_number: 1, carMake: 1, carModel: 1, carYear: 1
        /// </summary>
        [Fact]
        public async void GetCountOfExistingFlag()
        {
            const int DATBASE_COUNT = 100;
            const string TEST_ID = "1";

            FlagModel testFlag = new FlagModel(TEST_ID, TEST_ID, TEST_ID, TEST_ID);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            int result = await partFlaggingDataAccess.GetFlagCount(testFlag);
            Assert.Equal(result, DATBASE_COUNT);
        }

        /// <summary>
        /// Uses data access layer to create flag that does not exist.
        /// Test passes if the create or increment function modifies the database.
        /// </summary>    
        [Fact]
        public async void CreateNonexistentFlag()
        {
            const string TEST_ID = "2";

            FlagModel testFlag = new FlagModel(TEST_ID, TEST_ID, TEST_ID, TEST_ID);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            //Remove flag from the table when unit test is completed, so that upon running again the flag no longer exists
            await partFlaggingDataAccess.DeleteFlag(testFlag);

            var result = await partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);
            
            Assert.True(result);
        }

        /// <summary>
        /// Uses data access layer to create flag that does not exist.
        /// Test passes if the create or increment function modifies the database.
        /// </summary>    
        [Fact]
        public async void IncrementExistingFlag()
        {
            const int ONE = 1;
            const string TEST_ID = "3";

            FlagModel testFlag = new FlagModel(TEST_ID, TEST_ID, TEST_ID, TEST_ID);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            //Ensure part flag exists
            await partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);

            int previousCount = await partFlaggingDataAccess.GetFlagCount(testFlag);
            var result =  await partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);
            int subsequentCount = await partFlaggingDataAccess.GetFlagCount(testFlag);

            Assert.Equal(subsequentCount, previousCount + ONE);
        }

        /// <summary>
        /// Uses data access layer to delete a flag that does not exist.
        /// Test passes if delete function returns false because the table is not modified.
        /// </summary>    
        [Fact]
        public async void DeleteNonExistentFlag()
        {
            const string TEST_ID = "4";

            FlagModel testFlag = new FlagModel(TEST_ID, TEST_ID, TEST_ID, TEST_ID);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();
            
            //Manually delete flag to ensure that the flag does not exist in database before testing
            await partFlaggingDataAccess.DeleteFlag(testFlag);

            bool result = await partFlaggingDataAccess.DeleteFlag(testFlag);
            Assert.False(result);
        }

        /// <summary>
        /// Uses data access layer to delete a flag that does exist.
        /// Test passes if delete function returns true because the table is modified.
        /// </summary>    
        [Fact]
        public async void DeleteExistingFlag()
        {
            const string TEST_ID = "5";

            FlagModel testFlag = new FlagModel(TEST_ID, TEST_ID, TEST_ID, TEST_ID);

            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            //Manually Create Flag so that it is ensured to exist before deleting
            await partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);

            bool result = await partFlaggingDataAccess.DeleteFlag(testFlag);
            Assert.True(result);
        }

        /// <summary>
        /// Tests all possible cases of decrementing a flag.
        /// </summary>  
        [Theory]
        [InlineDataAttribute("GreaterThanOne", 2)]
        [InlineDataAttribute("EqualToOne", 1)]
        [InlineDataAttribute("Zero", 0)]
        public async void DecrementOrRemoveTests(string testName, int numCreations) 
        {
            bool result = false;
            
            FlagModel testFlag = new FlagModel(testName, testName, testName, "2022");
            
            PartFlaggingDataAccess partFlaggingDataAccess = new PartFlaggingDataAccess();

            bool creationSuccessful = true;
            for (int flagCreateIt = 0; flagCreateIt < numCreations; ++flagCreateIt)
            {
                creationSuccessful = creationSuccessful && await partFlaggingDataAccess.CreateOrIncrementFlag(testFlag);
            }

            if (creationSuccessful)
            {
                int prevCount = await partFlaggingDataAccess.GetFlagCount(testFlag);
                result = await partFlaggingDataAccess.DecrementOrRemove(testFlag);
                int afterCount = await partFlaggingDataAccess.GetFlagCount(testFlag);

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