using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.FetchContent.Controllers;

[ApiController]
[Route("[controller]")]
public class SubmitKpiController
{
    //[HttpPost]
    [Route("SubmitLoginKpiMetric")]
    public async Task<IResponseModel> PutLoginKpiMetric(LoginUsageMetricModel metricModel)
    {
        ISubmitKpiManager manager = new SubmitAdmissionKpiManager();

        try
        {
            var task = Task.Run(() => manager.IsSubmitKpiRequestValidAsync(metricModel));
            IResponseModel response = await task;
            return response;
        }
        catch (Exception e)
        {
            return new ExceptionResponseModel(e.Message);
        }
    }

    //[HttpPost]
    [Route("SubmitRegistrationKpiMetric")]
    public async Task<IResponseModel> PutRegistrationKpiMetric(RegistrationUsageMetricModel metricModel)
    {
        ISubmitKpiManager manager = new SubmitAdmissionKpiManager();

        try
        {
            var task = Task.Run(() => manager.IsSubmitKpiRequestValidAsync(metricModel));
            IResponseModel response = await task;
            return response;
        }
        catch (Exception e)
        {
            return new ExceptionResponseModel(e.Message);
        }
    }

    //[HttpPost]
    [Route("SubmitViewKpiMetric")]
    public async Task<IResponseModel> PutViewKpiMetric(ViewUsageMetricModel metricModel)
    {
        ISubmitKpiManager manager = new SubmitViewKpiManager();

        try
        {
            var task = Task.Run(() => manager.IsSubmitKpiRequestValidAsync(metricModel));
            IResponseModel response = await task;
            return response;
        }
        catch (Exception e)
        {
            return new ExceptionResponseModel(e.Message);
        }
    }
}