using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.FetchContent.Controllers;

[ApiController]
[Route("[controller]")]
public class FetchBarChartController
{
    //[HttpPost]
    [Route("FetchViewDisplayAnalytic")]
    public IResponseModel GetViewDisplayAnalytic(BaseUser user)
    {
        IUsageAnalyticModel model = new BarChartAnalyticModel("Views", "displayTotal");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, user.username);
        IFetchChartManager manager = new FetchBarChartManager("view");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }

    //[HttpPost]
    [Route("FetchViewDurationAnalytic")]
    public IResponseModel GetViewDurationAnalytic(BaseUser user)
    {
        IUsageAnalyticModel model = new BarChartAnalyticModel("Views", "durationAvg");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, user.username);
        IFetchChartManager manager = new FetchBarChartManager("view");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }

    //[HttpPost]
    [Route("FetchFeedPostAnalytic")]
    public IResponseModel GetFeedPostAnalytic(BaseUser user)
    {
        IUsageAnalyticModel model = new BarChartAnalyticModel("Feeds", "feedPostTotal");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, user.username);
        IFetchChartManager manager = new FetchBarChartManager("feed");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }
}