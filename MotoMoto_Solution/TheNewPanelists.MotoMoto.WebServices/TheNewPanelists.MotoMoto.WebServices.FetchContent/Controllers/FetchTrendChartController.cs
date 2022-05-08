using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.FetchContent.Controllers;

[ApiController]
[Route("[controller]")]
public class FetchTrendChartController
{
    //[HttpGet]
    [Route("FetchLoginAnalytic")]
    public IResponseModel GetLoginAnalytic()
    {
        IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Login Total");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
        IFetchChartManager manager = new FetchTrendChartManager("login");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }

    //[HttpGet]
    [Route("FetchRegistrationAnalytic")]
    public IResponseModel GetRegistrationAnalytic()
    {
        IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Registration Total");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
        IFetchChartManager manager = new FetchTrendChartManager("registration");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }

    //[HttpGet]
    [Route("FetchEventAnalytic")]
    public IResponseModel GetEventAnalytic()
    {
        IUsageAnalyticModel model = new TrendChartAnalyticModel("Event Date", "Event Total");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
        IFetchChartManager manager = new FetchTrendChartManager("event");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }
}