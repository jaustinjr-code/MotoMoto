using Xunit;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.Models;
using System.Collections.Generic;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartPriceAnalysisManagerUnitTest
    {
        private readonly PartPriceAnalysisManager _partpriceAnalysisManager = new PartPriceAnalysisManager();
        
        [Fact]
        public void IsValidIntValueForCategory_ExistingCategory_ReturnTrue()
        {
            var testPartListModel = new PartListModel()
            {
                categoryId = 0,
            };
            testPartListModel = _partpriceAnalysisManager!.RetrieveSpecifiedCategorialParts(testPartListModel);
            bool retrievalValue = false;
            if (testPartListModel.categorySelect != null && ((List<IPartEntity>)testPartListModel.partList!).Count > 0)
            {
                retrievalValue = true;
            }
            Assert.True(retrievalValue, "Information inserted was valid");
        }
        [Fact]
        public void IsValidComparisonModel_ExistingParts_ReturnTrue()
        {
            var partOne = new PartModel()
            {
                partID = 1,
                partName = "testPart",
                currentPrice = 10.00,
                productURL = "localhost:8080"
            };
            var partTwo = new PartModel()
            {
                partID = 2,
                partName = "testPart",
                currentPrice = 20.00,
                productURL = "localhost:8080"
            };
            PartComparisonModel testModel = new PartComparisonModel()
            {
                comparisonParts = new List<PartModel>() { partOne, partTwo },
            };
            _partpriceAnalysisManager.CompareVehicleParts(testModel);
            bool retrievalValue = false;
            if (((List<double>)testModel.currentPriceDifference!).Count! != 0)
            {
                retrievalValue = true;
            }
            Assert.True(retrievalValue);
        }
        [Fact]
        public void IsValidPartID_ExistingPart_ReturnTrue()
        {
            var testPart = new PartModel()
            {
                partID = 1
            };
            testPart = _partpriceAnalysisManager.EvaluateVehiclePart(testPart);
            bool retrievalValue = false;
            if (testPart.currentPrice > 0 && testPart.partName != "")
            {
                retrievalValue = true;
            }
            Assert.True(retrievalValue, "Part Non-Existent in the DS");
        }
        [Fact]
        public void IsInvalidIntCategory_WithNonExistentCategory_ReturnFalse()
        {
            var testPartPriceModel = new PartListModel
            {
                categoryId = -1,
            };
            testPartPriceModel = _partpriceAnalysisManager!.RetrieveSpecifiedCategorialParts(testPartPriceModel);
            Assert.False(testPartPriceModel.returnValueNoRealCategory, "The Specified value is existent under the enum 'PartCategories'!");
        }
        [Fact]
        public void IsInvalidComparisonModelWith_InvalidNumberOfModelsPresent_ReturnFalse()
        {
            PartComparisonModel testComparisonModel = new PartComparisonModel()
            {
                comparisonParts = new List<PartModel>(),
                currentPriceDifference = new List<double>()
            };
            testComparisonModel = _partpriceAnalysisManager.CompareVehicleParts(testComparisonModel);
            Assert.False(testComparisonModel.returnCaseBool, "The Specified comparison model cannot be compared due to no objects under the influence of being compared");
        }
    }
}
