namespace TheNewPanelists.MotoMoto.Models
{
    public class PartComparisonModel 
    {
        public IEnumerable<PartModel>? comparisonParts { get; set; }
        public IEnumerable<double>? currentPriceDifference { get; set; }
        public bool returnCaseBool = true;

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
