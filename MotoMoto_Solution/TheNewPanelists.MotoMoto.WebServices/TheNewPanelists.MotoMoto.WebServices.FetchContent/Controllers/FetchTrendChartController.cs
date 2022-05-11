using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.FetchContent.Controllers;

[ApiController]
[Route("[controller]")]
public class FetchTrendChartController
{
    //[HttpPost]
    [Route("FetchLoginAnalytic")]
    public IResponseModel GetLoginAnalytic(BaseUser user)
    {
        IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Login Total");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, user.username);
        IFetchChartManager manager = new FetchTrendChartManager("login");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }

    //[HttpPost]
    [Route("FetchRegistrationAnalytic")]
    public IResponseModel GetRegistrationAnalytic(BaseUser user)
    {
        IUsageAnalyticModel model = new TrendChartAnalyticModel("Access Date", "Registration Total");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, user.username);
        IFetchChartManager manager = new FetchTrendChartManager("registration");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }

    //[HttpPost]
    [Route("FetchEventAnalytic")]
    public IResponseModel GetEventAnalytic(BaseUser user)
    {
        IUsageAnalyticModel model = new TrendChartAnalyticModel("Event Date", "Event Total");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, user.username);
        IFetchChartManager manager = new FetchTrendChartManager("event");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }
}