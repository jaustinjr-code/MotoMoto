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
            _partPriceAnalysisService.RetrieveSpecifiedPartInformation(testModel);

        }
    }
}
