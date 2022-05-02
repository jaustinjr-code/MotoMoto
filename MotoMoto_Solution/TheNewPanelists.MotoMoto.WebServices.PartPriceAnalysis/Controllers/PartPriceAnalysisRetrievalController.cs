using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.WebServices.PartPriceAnalysis
{
    [Route("[controller]")]
    public class PartPriceAnalysisRetrievalController : Controller
    {
        private readonly IPartPriceAnalysisDataAccess _partPriceAnalysisDAO = new PartPriceAnalysisDataAccess();

        [HttpGet]
        public IActionResult RetrieveCategorialVehicleParts(int _categoryID, CancellationToken token= default(CancellationToken))
        {
            IPartPriceAnalysisService partService = new PartPriceAnalysisService(_partPriceAnalysisDAO);
            IPartPriceAnalysisManager partManager = new PartPriceAnalysisManager(partService);

            try
            {
                var partListModel = new PartListModel
                {
                    categoryId = _categoryID
                };
                partListModel = partManager.RetrieveSpecifiedCategorialParts(partListModel);
                if (partListModel.returnValueNoRealCategory == false)
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }

                return Ok(partListModel);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
