using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.FetchContent.Controllers;

[ApiController]
[Route("[controller]")]
public class SubmitUpvotePostController
{

    //[HttpPut]
    [Route("SubmitUpvotePost")]
    public IResponseModel PutUpvotePost(UpvotePostModel interaction)
    {
        IInteractionManager manager = new UpvotePostManager();
        (bool result, IResponseModel response) = manager.IsInteractionRequestValid(interaction);

        if (result)
            return (FeedPostResponseModel)response;
        return (ExceptionResponseModel)response;
    }
}