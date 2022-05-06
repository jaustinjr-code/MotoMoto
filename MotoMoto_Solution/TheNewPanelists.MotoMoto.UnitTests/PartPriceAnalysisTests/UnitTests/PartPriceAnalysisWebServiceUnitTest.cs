using TheNewPanelists.MotoMoto.WebServices.PartPriceAnalysis;
using Xunit;
using TheNewPanelists.MotoMoto.Models;
using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartPriceAnalysisWebServiceUnitTest
    {
        private readonly PartPriceAnalysisEvaluationController _evaluationController = new PartPriceAnalysisEvaluationController();
        private readonly PartPriceAnalysisRetrievalController _retrievalController = new PartPriceAnalysisRetrievalController();
        /// <summary>
        /// Valid category retrieval checks if a model was successfully retrieved. This will not 
        /// give a error because we can come accross the case that the category is existent and 
        /// that data is valid upon retrieval. We are soley checking for the status code and model
        /// </summary>
        /// <param name="categoryID"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void PartPriceAnalysisCategorialRetrieval_ValidCategory_ReturnTrue(int testPart)
        {
            var actionResult = _retrievalController.RetrieveCategorialVehicleParts(testPart);

            var okResult = (ObjectResult)actionResult;
            var okResultPartModel = okResult.Value as PartModel;

            bool assertValue = false;
            if (okResult.StatusCode == 200 || okResultPartModel!.productURL != null && okResultPartModel.returnValue == true)
            {
                assertValue = true;
            }
            Assert.True(assertValue, "Category is existent in model and should return model with correct data");
        }
        /// <summary>
        ///  Valid status is determined solely on the category that is requested. This functionality
        ///  should return a status 200 because each category is existent in the DataStore allowing
        ///  us to extract information requested.
        /// </summary>
        /// <param name="categoryID"></param>
        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void PartPriceAnalysisCategorialRetrieval_ValidStatus_ReturnTrue(int categoryID)
        {
            var actionResult = _retrievalController.RetrieveCategorialVehicleParts(categoryID);

            var okResult = (ObjectResult)actionResult;

            bool assertValue = false;
            if (okResult.StatusCode == 200)
            {
                assertValue = true;
            }
            Assert.True(assertValue);
        }
        /// <summary>
        /// Unit test is used to determine whether an invalid category is allowed to move
        /// forward to the business layer of retrieval. This should 100% return a status code 
        /// 400 for invalid category.
        /// </summary>
        /// <param name="categoryID"></param>
        [Theory]
        [InlineData(-99999)]
        [InlineData(-1)]
        [InlineData(100)]
        [InlineData(99999)]
        public void PartPriceAnalysisWebServiceCategRetrieval_InvalidCategory_ReturnFalse(int categoryID)
        {
            var actionResult = _retrievalController.RetrieveCategorialVehicleParts(categoryID);

            var result = (StatusCodeResult)actionResult;
            bool assertValue = true;

            if (result.StatusCode != 200)
            {
                assertValue = false;
            }
            Assert.False(assertValue, "No valid categories should exist that are not already pre-defined! See Models if return true");   
        }
        /// <summary>
        /// ValidComparison checks if parts are allowed to be compared and since all values
        /// are within range of the DS, this functionality should always return true for 
        /// real numbers. 
        /// </summary>
        /// <param name="partOne"></param>
        /// <param name="partTwo"></param>
        [Theory]
        [InlineData(1,2)]
        [InlineData(10,11)]
        [InlineData(100,95)]
        [InlineData(10, 1)]
        public void PartPriceAnalysisComparison_ValidComparison_ReturnTrue(int partOne, int partTwo)
        {
            var actionResult = _evaluationController.RetrieveComparisonVehicleParts(partOne, partTwo);

            var okResult = (OkObjectResult)actionResult;
            var okResultPartComparisonModel = okResult.Value as PartComparisonModel;

            bool assertValue = false;
            if (okResultPartComparisonModel!.comparisonParts != null && okResultPartComparisonModel.returnCaseBool == true)
            {
                assertValue = true;
            }
            Assert.True(assertValue);
        }
        
    }
}
