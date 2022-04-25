using Xunit;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.Models;


namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartPriceAnalysisManagerUnitTest
    {
        private bool _result;
        private readonly PartPriceAnalysisManager _partpriceAnalysisManager = new PartPriceAnalysisManager();

        [Fact]
        public void IsInvalidEnumValueWithValidOperation_WithNoRealValueCategorialEntity_ReturnFalse()
        {
            var testPartPriceModel = new PartListModel
            {
                partCategory = "invalidOperation"
            };
            testPartPriceModel = _partpriceAnalysisManager!.RetrieveSpecifiedCategorialParts(testPartPriceModel);
            Assert.False(testPartPriceModel.returnValueNoRealCategory, "The Specified value is non-existent under the enum 'PartCategories'!");
        }

        [Fact]
        public void IsInvalidComparisonModelWithInvalidNumberOfObjectsPresent_ReturnFalse()
        {
            var testComparisonModel = new PartComparisonModel();
            testComparisonModel = _partpriceAnalysisManager.CompareVehicleParts(testComparisonModel);

            Assert.False(testComparisonModel.returnCaseBool, "The Specified comparison model cannot be compared due to no objects under the influence of being compared");
        }
    }
}
