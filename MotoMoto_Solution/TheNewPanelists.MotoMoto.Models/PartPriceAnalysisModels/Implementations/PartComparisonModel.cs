using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class PartComparisonModel 
    {
        public List<PartModel>? comparisonParts = new List<PartModel>();
        public List<double>? currentPriceDifference = new List<double>();
        public bool returnCaseBool = true;
        public PartComparisonModel ReturnNullableStatementForPartPriceAnalysis() 
        {
            switch (comparisonParts?.Count)
            {
                case 0:
                    returnCaseBool = false;
                    return this;
            }
            return this;
        }
        
    }
}
