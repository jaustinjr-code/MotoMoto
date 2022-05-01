using TheNewPanelists.MotoMoto.WebServices.PartPriceAnalysis;
using Xunit;
using TheNewPanelists.MotoMoto.Models;
using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartPriceAnalysisAPIUnitTest
    {
        private readonly PartPriceAnalysisEvaluationController _evaluationController = new PartPriceAnalysisEvaluationController();
        private readonly PartPriceAnalysisRetrievalController _retrievalController = new PartPriceAnalysisRetrievalController();

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void PartPriceAnalysisCategorialRetrieval_ValidCategory_ReturnTrue(int categoryID)
        {
            var actionResult = _retrievalController.RetrieveCategorialVehicleParts(categoryID);

            var okResult = (OkObjectResult)actionResult;
            var okResultPartListModel = okResult.Value as PartListModel;

            bool assertValue = false;
            if (okResultPartListModel!.categoryId == categoryID && okResultPartListModel!.categorySelect != null)
            {
                assertValue = true;
            }
            Assert.True(assertValue);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-5)]
        [InlineData(99999)]
        public void PartPriceAnalysisCategorialRetrieval_InvalidCategory_ReturnFalse(int categoryID)
        {
            var actionResult = _retrievalController.RetrieveCategorialVehicleParts(categoryID);

            var okResult = (OkObjectResult)actionResult;
            var okResultPartListModel = okResult.Value as PartListModel;

            bool assertValue = false;
            if (okResultPartListModel!.categoryId == categoryID && okResultPartListModel!.categorySelect != null)
            {
                assertValue = true;
            }
            Assert.False(assertValue);
        }
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
