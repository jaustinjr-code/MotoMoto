using System;
using System.Collections.Generic;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface IPartPriceAnalysisService
    {
        public PartListModel RetrievSpecifiedCategorialParts(PartListModel partListModel);
        public PartModel RetrieveSpecifiedPartHistory(PartModel partModel);
        public PartModel RetrieveSpecifiedPartInformation(PartModel partModel);
        public PartComparisonModel RetrieveSpecifiedComparisonPartPriceHistory(PartComparisonModel partComparisonModel);

    }
}
