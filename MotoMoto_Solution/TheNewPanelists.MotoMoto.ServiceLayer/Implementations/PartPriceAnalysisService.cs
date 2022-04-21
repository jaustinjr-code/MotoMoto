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

        private PartComparisonModel EvaluateCurrentPriceDifference(PartComparisonModel partComparisonModel)
        {
            double tempMax = -1;
            double tempMin = -1;
            foreach (PartModel part in partComparisonModel!.ComparisonParts!)
                if (part.PartDatePrices![part.PartDatePrices.Count-1].Price > (int)tempMax)
                {
                    tempMax = part.PartDatePrices[part.PartDatePrices.Count - 1].Price;
                } 
                else
                {
                    tempMin = part.PartDatePrices[part.PartDatePrices.Count - 1].Price;
                }
            partComparisonModel.currentPriceDifference = tempMax - tempMin;
            return partComparisonModel;
        }

        public PartComparisonModel RetrievePartComparisonInformation(PartComparisonModel partComparisonModel)
        {
            foreach (PartModel part in partComparisonModel!.ComparisonParts!)
                continue;
            return null;
        }

        //public 
    }
}
