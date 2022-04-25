using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models.PartPriceAnalysisModels
{
    public class PartPriceAnalyzationModel
    {
        public double partPrice1 { get; set; }
        public double partPrice2 { get; set; }
        public double calculatedPartPrices = 0;

        public double calculateDifference()
        {
            calculatedPartPrices = partPrice1 - partPrice2;
            return calculatedPartPrices;
        }
    }
}
