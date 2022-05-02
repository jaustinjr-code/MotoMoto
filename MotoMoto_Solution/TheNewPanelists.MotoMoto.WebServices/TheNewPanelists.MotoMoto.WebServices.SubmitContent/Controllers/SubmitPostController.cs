using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.FetchContent.Controllers;

[ApiController]
[Route("[controller]")]
public class SubmitPostController
{

    //[HttpPut]
    [Route("SubmitPost")]
    public IResponseModel PutPost(FeedPostModel postModel)
    {
        // Only necessary if model binding doesn't work, but only needed a default constructor so it's fixed now
        //public IResponseModel? PutPost(string title, string feedName, string username, string description)
        //IPostModel postModel = new FeedPostModel(title, feedName, username, description);
        IContentManager manager = new PostManager();
        (bool result, IResponseModel? response) = ((PostManager)manager).IsContentRequestValid(postModel);
        if (result)
            return (FeedPostResponseModel)response!;
        return (ExceptionResponseModel)response;
    }
}