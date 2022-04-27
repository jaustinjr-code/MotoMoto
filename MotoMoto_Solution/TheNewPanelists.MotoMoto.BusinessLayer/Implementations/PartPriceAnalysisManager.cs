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
        /// EvaluateVehiclePart allows for users to evaluate a specified vehicle part and determine the price point of the 
        /// actual part. 
        /// </summary>
        /// <param name="partModel"></param>
        /// <returns></returns>
        public PartModel EvaluateVehiclePart(PartModel partModel)
        {
            return _partPriceAnalysisService!.RetrieveSpecifiedPartHistory(partModel);
        }
        /// <summary>
        /// This functionality is used to fetch information from the datastore allowing users to see specified parts in the category of
        /// their choice.
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
