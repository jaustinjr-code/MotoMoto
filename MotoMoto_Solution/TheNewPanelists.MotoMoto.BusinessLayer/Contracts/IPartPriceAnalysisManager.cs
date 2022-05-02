using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public interface IPartPriceAnalysisManager
    {
        public PartComparisonModel CompareVehicleParts(PartComparisonModel partComparisonModel);
        public PartModel EvaluateVehiclePart(PartModel partModel);
        public PartListModel RetrieveSpecifiedCategorialParts(PartListModel partListModel);
        public PartModel UpdatePartPriceAndAddHistoryManager(PartModel partModel);
    }
}
