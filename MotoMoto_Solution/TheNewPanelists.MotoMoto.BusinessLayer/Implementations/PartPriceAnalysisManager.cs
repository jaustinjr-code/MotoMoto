using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public enum PartCategories
    {
        // each value is used to represent the parts that will be available on motomotoca.com
        // all values are subject to expand or decrease based on the extensibility of this project
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

        public PartPriceAnalysisManager()
        {
            _partPriceAnalysisService = new PartPriceAnalysisService();
        }
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
