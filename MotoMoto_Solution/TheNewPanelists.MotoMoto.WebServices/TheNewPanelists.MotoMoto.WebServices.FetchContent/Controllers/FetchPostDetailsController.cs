using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.FetchContent.Controllers;

[ApiController]
[Route("[controller]")]
public class FetchPostDetailsController
{
    //[HttpPost]
    [Route("FetchPostDetails")]
    public IResponseModel GetPostDetails(int postId)
    {
        IRequestModel requestModel = new FetchPostDetailsRequestModel(postId);
        IContentManager manager = new PostDetailsManager();
        (bool result, IResponseModel response) = ((PostDetailsManager)manager).IsRequestValid(requestModel);
        if (result)
            return (FeedPostResponseModel)response;
        return (ExceptionResponseModel)response;
    }
}