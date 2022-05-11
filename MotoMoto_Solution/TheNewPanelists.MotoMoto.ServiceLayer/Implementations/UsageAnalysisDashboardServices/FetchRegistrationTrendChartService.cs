using System;
using System.Collections.Generic;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class FetchRegistrationTrendChartService : IFetchTrendChartService
    {
        /// <summary>
        /// Fetches the metrics for the Registration KPI for a trend chart
        /// </summary>
        /// <param name="analyticToFetch">IUsageAnalyticModel</param>
        /// <returns>IResponseModel</returns>
        public IResponseModel FetchTrendChartMetrics(IUsageAnalyticModel analyticToFetch)
        {
            try
            {
                IFetchKpiDataAccess kpiDataAccess = new FetchTrendKpiRegistrationDataAccess();
                IUsageAnalyticEntity result = kpiDataAccess.FetchChartMetrics(analyticToFetch);
                // Cast to Collection to avoid strict casting, but can I cast IEnumerable to ICollection?
                // if (((ICollection<IAxisDetailsEntity>)result.metricList).Count > 0)
                if (((List<IAxisDetailsEntity>)result.metricList).Count > 0)
                    return BuildResponse(result);
                return BuildDefaultResponse();
            }
            catch (Exception e)
            {
                return BuildExceptionResponse(e.Message);
            }
        }

        public IResponseModel BuildResponse(object result)
        {
            string message = "Retrieved Registration Metrics";
            bool complete = true;
            bool success = true;
            IResponseModel response = new ChartMetricsResponseModel((IUsageAnalyticEntity)result, message, complete, success);
            return response;
        }

        public IResponseModel BuildDefaultResponse()
        {
            string message = "No Registration Metrics Retrieved";
            bool complete = true;
            bool success = false;
            IResponseModel response = new ChartMetricsResponseModel(message, complete, success);
            return response;
        }

        public IResponseModel BuildExceptionResponse(string errorMessage)
        {
            string message = "Failed to Retrieve Registration Metrics: Error: " + errorMessage;
            IResponseModel response = new ExceptionResponseModel(message);
            return response;
        }
    }
}