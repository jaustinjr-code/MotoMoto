using TheNewPanelists.MotoMoto.Models;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface ISubmitKpiService : IResponseService
    {
        Task<IResponseModel> PutKpiAsync(IUsageMetricModel metricModel);
    }
}