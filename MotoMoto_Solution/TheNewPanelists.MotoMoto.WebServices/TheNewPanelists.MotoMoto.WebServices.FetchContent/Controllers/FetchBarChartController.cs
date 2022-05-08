using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.FetchContent.Controllers;

[ApiController]
[Route("[controller]")]
public class FetchBarChartController
{
    //[HttpGet]
    [Route("FetchViewDisplayAnalytic")]
    public IResponseModel GetViewDisplayAnalytic()
    {
        IUsageAnalyticModel model = new BarChartAnalyticModel("Views", "displayTotal");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
        IFetchChartManager manager = new FetchBarChartManager("view");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }

    //[HttpGet]
    [Route("FetchViewDurationAnalytic")]
    public IResponseModel GetViewDurationAnalytic()
    {
        IUsageAnalyticModel model = new BarChartAnalyticModel("Views", "durationAvg");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
        IFetchChartManager manager = new FetchBarChartManager("view");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }

    //[HttpGet]
    [Route("FetchFeedPostAnalytic")]
    public IResponseModel GetFeedPostAnalytic()
    {
        IUsageAnalyticModel model = new BarChartAnalyticModel("Feeds", "feedPostTotal");
        IUsageAnalyticFetchRequestModel request = new FetchUsageAnalyticRequestModel(model, "ran");
        IFetchChartManager manager = new FetchBarChartManager("feed");
        IResponseModel response = manager.IsAnalyticRequestValid(request);
        return response;
    }
}