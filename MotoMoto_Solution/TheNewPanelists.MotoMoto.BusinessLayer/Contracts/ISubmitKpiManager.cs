using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public interface ISubmitKpiManager
    {
        Task<IResponseModel> IsSubmitKpiRequestValidAsync(IUsageMetricModel metricModel);
    }
}