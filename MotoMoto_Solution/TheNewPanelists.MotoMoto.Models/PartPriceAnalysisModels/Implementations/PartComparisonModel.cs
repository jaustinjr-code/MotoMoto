namespace TheNewPanelists.MotoMoto.Models
{
    public class PartComparisonModel 
    {
        public IEnumerable<PartModel>? comparisonParts { get; set; }
        public IEnumerable<double>? currentPriceDifference { get; set; }
        public bool returnCaseBool = true;
        /// <summary>
        /// Safeguard function to determine that null comparison lists do not 
        /// pass through the business layer. 
        /// </summary>
        /// <returns></returns>
        public PartComparisonModel ReturnNullableStatementForPartPriceAnalysis() 
        {
            switch (((List<PartModel>)comparisonParts!).Count)
            {
                case 0:
                    returnCaseBool = false;
                    return this;
                default:
                    break;
            }
            if (returnCaseBool == true) 
            {
                ValidateProductID();
            }
            return this;
        }
        /// <summary>
        /// Returntype function used to evaluate true parts. This function
        /// is handled in the business layer to safeguard from parts that slip 
        /// through the controller
        /// </summary>
        public void ValidateProductID()
        {
            foreach (PartModel part in ((List<PartModel>)comparisonParts!))
            {
                if (part.partID < 0 || part.partName == "" || part.currentPrice <= 0)
                    returnCaseBool=false;
            }
        }
    }
}
