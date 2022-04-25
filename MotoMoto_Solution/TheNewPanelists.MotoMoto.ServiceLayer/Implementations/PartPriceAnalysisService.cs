using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class PartPriceAnalysisService
    {
        private readonly PartPriceAnalysisDataAccess? _partPriceAnalysisDAO;

        public PartPriceAnalysisService()
        {
            _partPriceAnalysisDAO = new PartPriceAnalysisDataAccess();
        }
        public PartPriceAnalysisService(PartPriceAnalysisDataAccess partPriceAnalysisDAO)
        {
            _partPriceAnalysisDAO = partPriceAnalysisDAO;
        }
        public PartListModel RetrievSpecifiedCategorialParts(PartListModel partListModel)
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
            ComparePrices(partComparisonModel);
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
    }
}
