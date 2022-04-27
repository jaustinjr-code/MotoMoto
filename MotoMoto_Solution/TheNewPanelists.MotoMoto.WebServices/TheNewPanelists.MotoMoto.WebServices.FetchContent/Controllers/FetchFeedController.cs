using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.FetchContent.Controllers;

[ApiController]
[Route("[controller]")]
public class FetchFeedController
{
    private static readonly string[] _feedNames = new string[]
    {
        "Lowrider",
        "Supercar",
        "European",
        "American Muscle",
        "Exotic",
        "Japanese",
        "Is That a Supra?!",
        "Economy",
        "Electic",
        "Sleeper",
        "Truck"
    };

    // Dynamic allocation from json data to objects in aspnet core
    [HttpPost]
    [Route("FetchFeed")]
    public LoadFeedResponseModel GetFeed(CommunityFeedModel feedModel)
    {

        IContentManager contentManager = new FeedManager();
        (bool isValidRequest, IResponseModel response) = ((FeedManager)contentManager).IsContentRequestValid(feedModel);
        if (isValidRequest)
        {
            return (LoadFeedResponseModel)response;
        }
        // This may break because no response data is defined
        return new LoadFeedResponseModel("Invalid request", false, false);
    }
}