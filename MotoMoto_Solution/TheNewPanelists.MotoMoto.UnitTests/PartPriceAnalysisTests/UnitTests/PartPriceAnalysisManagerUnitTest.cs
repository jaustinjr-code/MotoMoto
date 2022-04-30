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
        /// <summary>
        /// Test function determines if a category exists which in our case we are testing <alternator>. 
        /// We are testing the functionality of the return value in our business layer.
        /// </summary>
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
            Assert.True(retrievalValue, "Business Layer Defect on Retrieving Categorial Parts!!");
        }
        /// <summary>
        /// IsValidComparison will check if parts with partID 1 and 2 are existent. If they are, then
        /// we will update the information of partname, price, and productURL to determine real pricepoints
        /// of our object
        /// </summary>
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
            Assert.True(retrievalValue, "Business Layer Defective Comparing Vehicle Parts!!");
        }
        /// <summary>
        /// Functionality checks for partID's existence and retrieves information based on the part. Since 
        /// we know no price is going to cost $0.00 we can check to see if there is a part that exists with ID#1
        /// </summary>
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
        /// <summary>
        /// InvalidCategory will be used to check a failure case of Returning Categorial information. 
        /// This should return false since we have no information for the index -1 and also index -1 throws
        /// an our of bounds exception which should be handled in our business layer.
        /// </summary>
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
        /// <summary>
        /// InvalidComparisonModel tests if we have a null comparison model. In the case that we do select less than two
        /// items the goal is to return false since we must compare more than one part. 
        /// </summary>
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
