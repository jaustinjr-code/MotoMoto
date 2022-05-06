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
        private readonly IPartPriceAnalysisDataAccess _partPriceAnalysisDAO = new PartPriceAnalysisDataAccess();

        /// <summary>
        /// Retrieves information on vehicle comparisons using part ID's to track the infromation of each part comparing 
        /// their prices over the past 6 months. We will be tracking each ID based off of return calls(checkbox) from the front end
        /// </summary>
        /// <param name="_partIdOne"></param>
        /// <param name="_partIdTwo"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("compareParts")]
        public IActionResult RetrieveComparisonVehicleParts(int _partIdOne, int _partIdTwo, CancellationToken token = default(CancellationToken))
        {
            IPartPriceAnalysisService partService = new PartPriceAnalysisService(_partPriceAnalysisDAO);
            IPartPriceAnalysisManager partManager = new PartPriceAnalysisManager(partService);

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
                if (partOne.returnValue == false || partTwo.returnValue == false)
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
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
        /// <summary>
        /// EvaluateSpecifiedVehicle part allows a user to extract information on a specified part listed 
        /// on the parts board. This API allows users to retrieve a specified part and view more information
        /// on the history of a product.
        /// </summary>
        /// <param name="_partId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("Evaluate")]
        public IActionResult EvaluateSpecifiedVehiclePart(int _partId, CancellationToken token = default(CancellationToken))
        {
            IPartPriceAnalysisService partService = new PartPriceAnalysisService(_partPriceAnalysisDAO);
            IPartPriceAnalysisManager partManager = new PartPriceAnalysisManager(partService);

            try
            {
                var partModel = new PartModel()
                {
                    partID = _partId
                };
                //await Task.Delay(1_000, token);
                partModel = partManager.EvaluateVehiclePart(partModel);
                if (partModel.returnValue == false)
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
                return Ok(partModel);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
