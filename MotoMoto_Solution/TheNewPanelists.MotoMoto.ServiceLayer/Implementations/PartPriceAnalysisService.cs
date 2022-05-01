using System;
using System.Collections.Generic;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class PartPriceAnalysisService: IPartPriceAnalysisService
    {
        private readonly IPartPriceAnalysisDataAccess? _partPriceAnalysisDAO;
        /// <summary>
        /// Default constructor and only used for unit/integration testing
        /// This functionality is wasteful in resources so it is not good to
        /// use in other situational periods
        /// </summary>
        public PartPriceAnalysisService()
        {
            _partPriceAnalysisDAO = new PartPriceAnalysisDataAccess();
        }
        public PartPriceAnalysisService(IPartPriceAnalysisDataAccess partPriceAnalysisDAO)
        {
            _partPriceAnalysisDAO = partPriceAnalysisDAO;
        }
        /// <summary>
        /// This function retrieves the information from a specified categrorial part. We retrieve our 
        /// category name from the PartListModel that we have set on our category list that is configured
        /// as an array of strings that are default set as categories for data access
        /// </summary>
        /// <param name="partListModel"></param>
        /// <returns></returns>
        public PartListModel RetrievSpecifiedCategorialParts(PartListModel partListModel)
        {
            partListModel.categorySelect = partListModel.categories[partListModel.categoryId];
            return _partPriceAnalysisDAO!.RetrieveAllCategorialPartInformationDataAccess(partListModel);
        }
        /// <summary>
        /// This functionality is solely used to retrieve the specified information on a products price history.
        /// This information is subjected to change over time based on the amount of information that is retrieved 
        /// by the user. 
        /// </summary>
        /// <param name="partModel"></param>
        /// <returns></returns>
        public PartModel RetrieveSpecifiedPartHistory(PartModel partModel)
        {
            return _partPriceAnalysisDAO!.RetrieveSpecifiedPartPriceHistory(partModel);
        }

        public PartModel RetrieveSpecifiedPartInformation(PartModel partModel)
        {
            return _partPriceAnalysisDAO!.RetrievePartInformation(partModel);
        }
        /// <summary>
        /// Used to compare two same categorial vehicle part prices and their history of price changes
        /// We use this model to ensure that prices have changed over time and that we can display the 
        /// correlation of Price over the span of time
        /// </summary>
        /// <param name="partComparisonModel"></param>
        /// <returns></returns>
        public PartComparisonModel RetrieveSpecifiedComparisonPartPriceHistory(PartComparisonModel partComparisonModel)
        {
            foreach (PartModel part in ((List<PartModel>)partComparisonModel.comparisonParts!))
            {
                RetrieveSpecifiedPartInformation(part);
            }
            ComparePrices(partComparisonModel);
            return partComparisonModel;
        }
        /// <summary>
        /// Compare prices is a simple service that allows parsed vehicle price information 
        /// to be computed finding the price difference between two vehicle parts.
        /// </summary>
        /// <param name="partComparisonModel"></param>
        private void ComparePrices(PartComparisonModel partComparisonModel)
        {
            double min = -1;
            double max = -1;
            IEnumerable<double> _partComparisons = new List<double>();
            foreach (PartModel part in partComparisonModel.comparisonParts!)
            {
                if (part.currentPrice > max && part.currentPrice > min)
                {
                    switch(max)
                    {
                        case -1:
                            max = part.currentPrice;
                            min = max;
                            break;
                        default:
                            max = part.currentPrice;
                            break;
                    }
                }
                else
                {
                    min = part.currentPrice;
                }
            }
            double temp = max - min;
            ((List<double>)_partComparisons).Add(Math.Abs(temp));
            partComparisonModel.currentPriceDifference = _partComparisons;
        }
    }
}
