using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.Models;
using MySql.Data.MySqlClient;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public interface IPartPriceAnalysisDataAccess
    {
        public bool EstablishMariaDBConnection();
        public PartListModel RetrieveAllCategorialPartInformationDataAccess(PartListModel listModel);
        public PartModel RetrievePartInformation(PartModel part);
        public PartModel RetrieveSpecifiedPartPriceHistory(PartModel partModel);
        public PartModel UpdatePartPrice(PartModel partModel);
    }
}
