using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class PartPriceAnalysisManager
    {
        private readonly PartPriceAnalysisService? _partPriceAnalysisService;

        public PartPriceAnalysisManager(PartPriceAnalysisService partPriceAnalysisService)
        {
            _partPriceAnalysisService = partPriceAnalysisService;
        }

        public PartComparisonModel CompareVehicleParts(PartComparisonModel partComparisonModel)
        {
            throw new NotImplementedException();
        }

        public PartModel EvaluateVehiclePart(PartModel partModel)
        {
            throw new NotImplementedException();
        }
    }
}
