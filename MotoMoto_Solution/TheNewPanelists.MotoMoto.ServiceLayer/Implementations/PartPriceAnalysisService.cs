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
            foreach (PartModel part in partComparisonModel!.comparisonParts!)
                if (part.currentPrice > max && part.currentPrice > min)
                {
                    max = part.currentPrice;
                }
                else
                {
                    min = part.currentPrice;
                }
            double temp = max - min;
            partComparisonModel!.currentPriceDifference!.Add(temp);
        }

        private void CompareMultiplePartModelPrices(PartComparisonModel partComparisonModel)
        {
            switch (partComparisonModel.comparisonParts!.Count)
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
            if (partComparisonModel!.comparisonParts!.Count > 0 && partComparisonModel!.comparisonParts!.Count > 2)
            {
                for (int i = 0; i < partComparisonModel!.comparisonParts.Count; i++)
                    for (int j = 1; j < i-1; j++)
                        partComparisonModel!.currentPriceDifference!.Add(1);
            }
        }
    }
}
