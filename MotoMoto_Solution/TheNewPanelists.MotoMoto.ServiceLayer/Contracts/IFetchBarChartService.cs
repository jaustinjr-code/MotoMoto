using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface IFetchBarChartService : IResponseService
    {
        // IUsageAnalyticModel analyticToFetch { get; }

        IResponseModel FetchBarChartMetrics(IUsageAnalyticModel analyticToFetch);
        //IUsageAnalyticEntity OrganizeBarChartMetrics();
    }
}