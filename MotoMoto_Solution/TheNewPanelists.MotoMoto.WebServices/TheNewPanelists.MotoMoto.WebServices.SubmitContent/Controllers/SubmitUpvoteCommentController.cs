using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.FetchContent.Controllers;

[ApiController]
[Route("[controller]")]
public class SubmitUpvoteCommentController
{

    //[HttpPut]
    [Route("SubmitUpvoteComment")]
    public IResponseModel PutUpvoteComment(UpvoteCommentModel interaction)
    {
        IInteractionManager manager = new UpvoteCommentManager();
        (bool result, IResponseModel response) = manager.IsInteractionRequestValid(interaction);

        if (result)
            return (CommentPostResponseModel)response;
        return (ExceptionResponseModel)response;
    }
}