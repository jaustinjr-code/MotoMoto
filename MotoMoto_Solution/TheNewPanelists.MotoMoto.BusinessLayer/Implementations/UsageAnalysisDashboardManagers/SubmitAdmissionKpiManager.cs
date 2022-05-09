using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class SubmitAdmissionKpiManager : ISubmitKpiManager
    {
        public async Task<IResponseModel> IsSubmitKpiRequestValidAsync(IUsageMetricModel metricModel)
        {
            // Checks the input
            string error;
            string type = metricModel.type.ToLower();
            if (!type.Equals("registration") &&
                !type.Equals("login"))
                error = "Invalid Type Request";
            else if (metricModel.metric < 0)
                error = "Invalid Metric Value";
            else error = "";
            if (!error.Equals(""))
                return new ExceptionResponseModel(error);

            try
            {
                IResponseModel response = await ProcessRequestAsync(metricModel);
                if (IsSubmitKpiResponseValid(response))
                {
                    return response;
                }
                throw new Exception("Invalid Response");
            }
            catch (Exception e)
            {
                return new ExceptionResponseModel(e.Message);
            }
        }

        private async Task<IResponseModel> ProcessRequestAsync(IUsageMetricModel metricModel)
        {
            ISubmitKpiService service = new SubmitAdmissionKpiService();
            var task = Task.Run(() => service.PutKpiAsync(metricModel));
            IResponseModel response = await task;
            return response;
        }

        private bool IsSubmitKpiResponseValid(IResponseModel response)
        {
            if (response.isComplete)
                return true;
            else if (response.isComplete == false && response.isSuccess == false)
                return true;
            return false;
        }

    }
}