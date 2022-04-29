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
        private readonly PartPriceAnalysisDataAccess _partPriceAnalysisDAO = new PartPriceAnalysisDataAccess();
        private readonly LogService _logService = new LogService();

        [HttpGet]
        public async Task<ActionResult<PartListModel>> RetrieveCategorialVehicleParts(int _categoryID, CancellationToken token= default(CancellationToken))
        {
            PartPriceAnalysisService partService = new PartPriceAnalysisService();
            PartPriceAnalysisManager partManager = new PartPriceAnalysisManager(partService);

            try
            {
                var partListModel = new PartListModel
                {
                    categoryId = _categoryID
                };
                partListModel = partManager.RetrieveSpecifiedCategorialParts(partListModel);
                await Task.Delay(0_001, token);

                return Ok(partListModel);
            }
            catch
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            
        }
    }
}
