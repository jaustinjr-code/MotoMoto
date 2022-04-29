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

        public void PartPriceAnalysisCategorialRetrieval_ValidCategory_ReturnTrue()
        {
            int categoryID = 1;
            
            var actionResult = _retrievalController.RetrieveCategorialVehicleParts(categoryID);

            var okResult = (OkObjectResult)actionResult.Result;
            var actionPartList = okResult.Value as PartListModel;

            bool assertValue = false;
            if (actionPartList!.categoryId == categoryID && actionPartList!.categorySelect != null)
            {
                assertValue = true;
            }
            Assert.True(assertValue);
        }
    }
}
