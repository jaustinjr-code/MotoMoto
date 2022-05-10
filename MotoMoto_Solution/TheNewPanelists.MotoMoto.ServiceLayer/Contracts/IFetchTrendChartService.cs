using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface IFetchTrendChartService : IResponseService
    {
        // IUsageAnalyticModel contentToFetch { get; }

        IResponseModel FetchTrendChartMetrics(IUsageAnalyticModel analyticToFetch);
        //IUsageAnalyticEntity OrganizeBarChartMetrics();
    }
}