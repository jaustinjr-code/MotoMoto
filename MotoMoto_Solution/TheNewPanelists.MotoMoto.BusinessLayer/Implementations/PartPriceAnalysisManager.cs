using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class PartPriceAnalysisManager : IPartPriceAnalysisManager
    {
        private readonly IPartPriceAnalysisService? _partPriceAnalysisService;
        // Default constructor only used for unit testing //
        public PartPriceAnalysisManager()
        {
            _partPriceAnalysisService = new PartPriceAnalysisService();
        }
        public PartPriceAnalysisManager(IPartPriceAnalysisService partPriceAnalysisService)
        {
            _partPriceAnalysisService = partPriceAnalysisService;
        }
        /// <summary>
        /// Compares two vehicle parts but does an intial check whether there is at least 2 parts 
        /// in the list of parts, otherwise this function returns a null
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
        /// Evaluates a specified part and retrieves the information to display analytical data to a user.
        /// This function should neve fail as there is no comparisons going on.
        /// </summary>
        /// <param name="partModel"></param>
        /// <returns></returns>
        public PartModel EvaluateVehiclePart(PartModel partModel)
        {
            partModel = _partPriceAnalysisService!.RetrieveSpecifiedPartInformation(partModel);
            if (partModel.ReturnValueInvalidation().returnValue == false)
            {
                return partModel;
            }
            return _partPriceAnalysisService!.RetrieveSpecifiedPartHistory(partModel);
        }
        /// <summary>
        /// This function returns the list of categorial parts and checks if the category exists within the 
        /// predetermined list of items. Otherwise this function returns a boolean value for failure.
        /// </summary>
        /// <param name="partListModel"></param>
        /// <returns></returns>
        public PartListModel RetrieveSpecifiedCategorialParts(PartListModel partListModel)
        {
            if (partListModel.categoryId < partListModel.categories.Length && partListModel.categoryId >= 0)
            {
                return _partPriceAnalysisService!.RetrievSpecifiedCategorialParts(partListModel);
            }
            return partListModel.InvalidRetrunValueForNoTrueCategory();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partModel"></param>
        /// <returns></returns>
        public PartModel UpdatePartPriceAndAddHistoryManager(PartModel partModel)
        {
            if (partModel.currentPrice == partModel.newPrice || partModel.newPrice < 0 || partModel.partID < 0)
            {
                return partModel.ReturnInvalidPriceUpdate();
            }
            return _partPriceAnalysisService!.UpdatePartPriceAndRecordToHistoryService(partModel);
        }
    }
}
