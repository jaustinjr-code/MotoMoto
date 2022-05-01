using Xunit;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartPriceAnalysisServiceUnitTest
    {
        private readonly PartPriceAnalysisService _partPriceAnalysisService;
        private bool _test;

        public PartPriceAnalysisServiceUnitTest()
        {
            _partPriceAnalysisService = new PartPriceAnalysisService();
        }
        /// <summary>
        /// All testmodel data will be updated with approproate, real information by the data
        /// access call. We are testing to ensure that a true model is returned of historical
        /// data => Data type should return a IEnum list of historical data from the past 6 months
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void PartPriceAnalysisServicePartHistoryRetrieval_ValidPart_ReturnTrue()
        {
            _test = false;
            var testModel = new PartModel()
            {
                partID = 1,
                partName = "test",
            };
            testModel = _partPriceAnalysisService.RetrieveSpecifiedPartInformation(testModel);
            if (testModel.currentPrice > 0 && testModel.partName!.Length > 0)
            {
                _test = true;
            }
            Assert.True(_test);
        }
        [Fact]
        public void PartPriceAnalysisServiceRetrievePartHistory_ReturnValidHistory_ReturnTrue()
        {
            _test = false;
            var testModel = new PartModel()
            {
                partID = 1,
                partName = "test",
            };
            testModel = _partPriceAnalysisService.RetrieveSpecifiedPartHistory(testModel);
            List<IPartPriceHistory> _testHist = ((List<IPartPriceHistory>)testModel!.historicalPrices!);
            if (_testHist.Count > 0)
            {
                _test = true;
            }
            Assert.True(_test, "List for the partID 1 Should Exist!!");
        }
        [Fact]
        public void PartPriceAnalysisServiceRetrieveRealComparisonValue_ReturnTrue()
        {
            _test = false;
            IEnumerable<PartModel> testComps = new List<PartModel>();
            var testModel = new PartModel()
            {
                partID = 1,
                partName = "test",
            };
            var testModel2 = new PartModel()
            {
                partID = 2,
                partName = "test2",
            };
            var testCompModel = new PartComparisonModel();
            ((List<PartModel>)testComps).Add(testModel);
            ((List<PartModel>)testComps).Add(testModel2);
            testCompModel.comparisonParts = testComps;

            testCompModel = _partPriceAnalysisService.RetrieveSpecifiedComparisonPartPriceHistory(testCompModel);
            // if there is a price difference, we know there was a comparison
            if (testCompModel.currentPriceDifference != null)
            {
                _test = true;
            }
            Assert.True(_test, "No Comparison was Made!! Faulure in comparison price history");
        }
        [Fact]
        public void PartPriceAnalysisServicePartHistoryRetrieval_InvalidPart_ReturnFalse()
        {
            _test = false;
            var testModel = new PartModel()
            {
                partID = -1,
                partName = "test",
            };
            testModel = _partPriceAnalysisService.RetrieveSpecifiedPartInformation(testModel);
            if (testModel.currentPrice > 0 && testModel.partName!.Length > 0)
            {
                _test = true;
            }
            Assert.False(_test, "There should be nothing returned since partID is invalid");
        }
        [Fact]
        public void PartPriceAnalysisServiceRetrievePartHistory_ReturnNoValidPartORHistory_ReturnFalse()
        {
            _test = true;
            var testModel = new PartModel()
            {
                partID = -1,
                partName = "test",
            };
            testModel = _partPriceAnalysisService.RetrieveSpecifiedPartHistory(testModel);
            if (testModel.currentPrice <= 0 || testModel.historicalPrices == null)
            {
                _test = false;
            }
            Assert.False(_test, $"List for the partID -1 Should NOT Exist");
        }
    }
}
