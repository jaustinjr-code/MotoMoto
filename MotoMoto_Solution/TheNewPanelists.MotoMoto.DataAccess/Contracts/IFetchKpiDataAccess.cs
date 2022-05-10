using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public interface IFetchKpiDataAccess
    {
        IUsageAnalyticEntity FetchChartMetrics(IUsageAnalyticModel analyticModel);
    }
}