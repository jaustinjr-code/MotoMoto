using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class PartComparisonModel 
    {
        public List<PartModel>? ComparisonParts { get; set; }

        public double currentPriceDifference { get; set; }

    }
}
