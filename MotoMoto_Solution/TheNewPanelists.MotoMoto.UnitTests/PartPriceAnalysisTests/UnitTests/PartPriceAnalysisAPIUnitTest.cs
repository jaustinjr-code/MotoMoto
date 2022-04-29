using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TheNewPanelists.MotoMoto.WebServices.PartPriceAnalysis;

namespace TheNewPanelists.MotoMoto.UnitTests
{
    public class PartPriceAnalysisAPIUnitTest
    {
        private readonly PartPriceAnalysisEvaluationController _evaluationController;

        public PartPriceAnalysisAPIUnitTest()
        {
            _evaluationController = new PartPriceAnalysisEvaluationController();
        }
    }
}
