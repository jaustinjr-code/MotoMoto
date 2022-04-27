using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.WebServices.PartPriceAnalysis.Controllers
{
    [Route("partpriceAnalysis/[controller]")]
    public class PartPriceAnalysisController : Controller
    {
        private readonly PartPriceAnalysisDataAccess _partPriceAnalysisDAO = new PartPriceAnalysisDataAccess();
        private readonly LogService _logService = new LogService();

        public IActionResult PreFlightRoute()
        {
            return NoContent();
        }

        public async Task<PartListModel> RetrieveCategorialVehicleParts(int _categoryID, CancellationToken token= default(CancellationToken))
        {
            PartPriceAnalysisService partService = new PartPriceAnalysisService();
            PartPriceAnalysisManager partManager = new PartPriceAnalysisManager(partService);

            var partListModel = new PartListModel
            {
                categoryId = _categoryID
            };
            partListModel = partManager.RetrieveSpecifiedCategorialParts(partListModel);
            await Task.Delay(10_000, token);

            return partListModel;
        }
        public async Task<PartComparisonModel> RetrieveComparisonVehicleParts(int _partIdOne, int _partIdTwo, CancellationToken token= default(CancellationToken))
        {
            PartPriceAnalysisService partService = new PartPriceAnalysisService();
            PartPriceAnalysisManager partManager = new PartPriceAnalysisManager(partService);

            var partComparisonModel = new PartComparisonModel();
            var partOne = new PartModel()
            {
                partID = _partIdOne
            };
            var partTwo = new PartModel()
            {
                partID = _partIdTwo
            };
            partComparisonModel.comparisonParts?.Add(partOne);
            partComparisonModel.comparisonParts?.Add(partTwo);

            partComparisonModel = partManager.CompareVehicleParts(partComparisonModel);
            await Task.Delay(10_000, token);

            return partComparisonModel;
        }
        
        public async Task<PartModel> EvaluateSpecifiedVehiclePart(int _partId, CancellationToken token = default(CancellationToken))
        {
            PartPriceAnalysisService partService = new PartPriceAnalysisService();
            PartPriceAnalysisManager partManager = new PartPriceAnalysisManager(partService);

            var partModel = new PartModel()
            {
                partID = _partId
            };
            partModel = partManager.EvaluateVehiclePart(partModel);
            await Task.Delay(10_000, token);
            return partModel;
        }
    }
}
