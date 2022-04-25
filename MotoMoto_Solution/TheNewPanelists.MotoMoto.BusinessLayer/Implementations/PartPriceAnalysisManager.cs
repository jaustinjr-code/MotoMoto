using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public enum PartCategories
    {
        alternator,
        brakePads,
        brakeRotor,
        cylinderHead,
        engineBlock,
        exhaustManifold,
        muffler,
        oilFilter,
        radiator,
        sparkPlug,
        timingBelt,
        timingChain,
        turbo,
        waterPump
    }
    public class PartPriceAnalysisManager
    {
        private readonly PartPriceAnalysisService? _partPriceAnalysisService;

        public PartPriceAnalysisManager(PartPriceAnalysisService partPriceAnalysisService)
        {
            _partPriceAnalysisService = partPriceAnalysisService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partComparisonModel"></param>
        /// <returns></returns>
        public PartComparisonModel CompareVehicleParts(PartComparisonModel partComparisonModel)
        {
            partComparisonModel = partComparisonModel.ReturnNullableStatementForPartPriceAnalysis();
            if (partComparisonModel.returnCaseBool == false)
            {
                return partComparisonModel;
            }
            return _partPriceAnalysisService!.RetrieveSpecifiedComparisonPartPriceHistory(partComparisonModel);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partModel"></param>
        /// <returns></returns>
        public PartModel EvaluateVehiclePart(PartModel partModel)
        {
            return _partPriceAnalysisService!.RetrieveSpecifiedPartHistory(partModel);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partListModel"></param>
        /// <returns></returns>
        public PartListModel RetrieveSpecifiedCategorialParts(PartListModel partListModel)
        {
            if (Enum.IsDefined(typeof(PartCategories), partListModel!.partCategory!))
            {
                return _partPriceAnalysisService!.RetrievSpecifiedCategorialParts(partListModel);
            }
            return partListModel.InvalidRetrunValueForNoTrueCategory();
        }
    }
}
