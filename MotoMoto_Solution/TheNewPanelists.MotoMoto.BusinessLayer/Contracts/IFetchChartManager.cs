using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public interface IFetchChartManager
    {
        string analyticType { get; }
        IResponseModel IsAnalyticRequestValid(IUsageAnalyticFetchRequestModel request);
    }
}
