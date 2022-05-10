using System;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class SubmitAdmissionKpiService : ISubmitKpiService
    {
        /// <summary>
        /// Empty Default Constructor
        /// </summary>
        public SubmitAdmissionKpiService() { }

        /// <summary>
        /// Puts in a KPI metric
        /// Builds a response on true, default response on false, and exception response on caught exception
        /// </summary>
        /// <returns>Task<IResponseModel></returns>
        public async Task<IResponseModel> PutKpiAsync(IUsageMetricModel metricModel)
        {
            ISubmitKpiDataAccess kpiDataAccess = new SubmitAdmissionKpiDataAccess();
            try
            {
                // Source: https://stackoverflow.com/questions/40774931/listmyobject-does-not-contain-a-definition-for-getawaiter
                // Beware this may not have correct results
                var myTask = Task.Run(() => kpiDataAccess.SubmitKpiMetricsAsync(metricModel));
                bool result = await myTask;
                if (result)
                    return BuildResponse(result);
                return BuildDefaultResponse();
            }
            catch (Exception)
            {
                // Log the exception
                return BuildExceptionResponse("Incomplete Operation");
                //throw e;
            }
        }

        /// <summary>
        /// Builds response model for the specific result
        /// </summary>
        /// <param name="result"></param>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildResponse(object result)
        {
            string message = "KPI Metric Submitted";
            bool complete = true;
            bool success = (bool)result;
            IResponseModel response = new CommentPostResponseModel(message, complete, success);
            return response;
        }

        /// <summary>
        /// Builds the default response if the result is false
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildDefaultResponse()
        {
            string message = "KPI Metric Not Submitted";
            bool complete = true;
            bool success = false;
            IResponseModel response = new CommentPostResponseModel(message, complete, success);
            return response;
        }

        /// <summary>
        /// Builds exception response if the data access operation throws an exception
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildExceptionResponse(string errorMessage)
        {
            IResponseModel response = new ExceptionResponseModel(errorMessage);
            return response;
        }
    }
}