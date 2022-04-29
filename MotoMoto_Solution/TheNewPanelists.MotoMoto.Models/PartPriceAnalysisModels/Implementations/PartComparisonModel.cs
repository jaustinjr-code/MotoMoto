namespace TheNewPanelists.MotoMoto.Models
{
    public class PartComparisonModel 
    {
        public IEnumerable<PartModel>? comparisonParts { get; set; }
        public IEnumerable<double>? currentPriceDifference { get; set; }
        public bool returnCaseBool = true;

        public PartComparisonModel ReturnNullableStatementForPartPriceAnalysis() 
        {
            switch (comparisonParts?.ToList().Count)
            {
                case 0:
                    returnCaseBool = false;
                    return this;
            }
            return this;
        }
    }
}
