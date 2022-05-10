using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public interface ISubmitKpiDataAccess
    {
        public enum AnalyticResult { FAILED_UPDATE = -1, N0_ANALYTIC, NEW_ANALYTIC, UPDATED_ANALYTIC }
        Task<bool> SubmitKpiMetricsAsync(IUsageMetricModel metricModel);
    }
}