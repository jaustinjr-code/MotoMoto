using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using Microsoft.AspNetCore.Cors;

namespace TheNewPanelists.MotoMoto.WebServices.PartPriceAnalysis
{
    [Route("[controller]")]
    public class PartPriceAnalysisEvaluationController : Controller
    {
        private readonly PartPriceAnalysisDataAccess _partPriceAnalysisDAO = new PartPriceAnalysisDataAccess();

        [HttpGet("compareParts")]
        public IActionResult RetrieveComparisonVehicleParts(int _partIdOne, int _partIdTwo, CancellationToken token = default(CancellationToken))
        {
            PartPriceAnalysisService partService = new PartPriceAnalysisService(_partPriceAnalysisDAO);
            PartPriceAnalysisManager partManager = new PartPriceAnalysisManager(partService);

            IEnumerable<PartModel> _compParts = new List<PartModel>();
            var partComparisonModel = new PartComparisonModel();

            try
            {
                var partOne = new PartModel()
                {
                    partID = _partIdOne
                };
                var partTwo = new PartModel()
                {
                    partID = _partIdTwo
                };

                partOne = partManager.EvaluateVehiclePart(partOne);
                partTwo = partManager.EvaluateVehiclePart(partTwo);

                ((List<PartModel>)_compParts).Add(partOne);
                ((List<PartModel>)_compParts).Add(partTwo);
                partComparisonModel.comparisonParts = _compParts;

                partComparisonModel = partManager.CompareVehicleParts(partComparisonModel);
                //await Task.Delay(1_000, token);

                return Ok(partComparisonModel);
            } 
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpGet("Evaluate")]
        public IActionResult EvaluateSpecifiedVehiclePart(int _partId, CancellationToken token = default(CancellationToken))
        {
            PartPriceAnalysisService partService = new PartPriceAnalysisService();
            PartPriceAnalysisManager partManager = new PartPriceAnalysisManager(partService);

            try
            {
                var partModel = new PartModel()
                {
                    partID = _partId
                };
                //await Task.Delay(1_000, token);
                partModel = partManager.EvaluateVehiclePart(partModel);

                return Ok(partModel);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
