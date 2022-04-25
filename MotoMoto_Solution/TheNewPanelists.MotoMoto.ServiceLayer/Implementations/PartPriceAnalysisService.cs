using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class PartPriceAnalysisService
    {
        private readonly PartPriceAnalysisDataAccess? _partPriceAnalysisDAO;

        public PartPriceAnalysisService(PartPriceAnalysisDataAccess partPriceAnalysisDAO)
        {
            _partPriceAnalysisDAO = partPriceAnalysisDAO;
        }
        public PartListModel RetrievAllParts(PartListModel partListModel)
        {
            return _partPriceAnalysisDAO!.RetrieveAllCategorialPartInformationDataAccess(partListModel);
        }

        public PartModel RetrieveSpecifiedPartHistory(PartModel partModel)
        {
            return _partPriceAnalysisDAO!.RetrieveSpecifiedPartPriceHistory(partModel);
        }

        public PartComparisonModel RetrieveSpecifiedComparisonPartPriceHistory(PartComparisonModel partComparisonModel)
        {
            partComparisonModel = _partPriceAnalysisDAO!.RetrieveMultipleProductsToCompare(partComparisonModel);
            //ComparePrices(partComparisonModel);
            return partComparisonModel;
        }

        private void ComparePrices(PartComparisonModel partComparisonModel)
        {
            double min = double.PositiveInfinity;
            double max = double.NegativeInfinity;
            foreach (PartModel part in partComparisonModel!._comparisonParts!)
                if (part._currentPrice > max && part._currentPrice > min)
                {
                    max = part._currentPrice;
                }
                else
                {
                    min = part._currentPrice;
                }
            double temp = max - min;
            partComparisonModel!._currentPriceDifference!.Add(temp);
        }

        private void CompareMultiplePartModelPrices(PartComparisonModel partComparisonModel)
        {
            switch (partComparisonModel._comparisonParts!.Count)
            {
                case 2:
                    ComparePrices(partComparisonModel);
                    break;
                case 3:
                    break;
            }
        }

        private void CompareMultipleSpecifiedPartPrices(PartComparisonModel partComparisonModel)
        {
            double maxi = double.PositiveInfinity;
            double mini = double.NegativeInfinity;
            if (partComparisonModel!._comparisonParts!.Count > 0 && partComparisonModel!._comparisonParts!.Count > 2)
            {
                for (int i = 0; i < partComparisonModel!._comparisonParts.Count; i++)
                    for (int j = 0; j < i; j++)
                        if (maxi < partComparisonModel!._comparisonParts[i]._currentPrice)
                        {
                             = partComparisonModel!._comparisonParts[i]._currentPrice;
                        }
            }
        }
    }
}
