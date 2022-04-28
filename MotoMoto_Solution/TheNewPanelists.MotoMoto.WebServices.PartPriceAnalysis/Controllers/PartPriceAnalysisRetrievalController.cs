using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.BusinessLayer;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.WebServices.PartPriceAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartPriceAnalysisRetrievalController : Controller
    {
        private readonly PartPriceAnalysisDataAccess _partPriceAnalysisDAO = new PartPriceAnalysisDataAccess();
        private readonly LogService _logService = new LogService();
        public async Task<PartListModel> RetrieveCategorialVehicleParts(int _categoryID, CancellationToken token= default(CancellationToken))
        {
            PartPriceAnalysisService partService = new PartPriceAnalysisService();
            PartPriceAnalysisManager partManager = new PartPriceAnalysisManager(partService);

            var partListModel = new PartListModel
            {
                categoryId = _categoryID
            };
            partListModel = partManager.RetrieveSpecifiedCategorialParts(partListModel);
            await Task.Delay(1_000, token);

            return partListModel;
        }
    }
}
