using Microsoft.AspNetCore.Mvc;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.BusinessLayer;

namespace TheNewPanelists.MotoMoto.WebServices.FetchContent.Controllers;

[ApiController]
[Route("[controller]")]
public class SubmitCommentController
{

    //[HttpPut]
    [Route("SubmitComment")]
    public IResponseModel PutComment(CommentPostModel commentModel)
    {
        IContentManager manager = new CommentManager();
        (bool result, IResponseModel? response) = ((CommentManager)manager).IsContentRequestValid(commentModel);
        if (result)
            return (CommentPostResponseModel)response!;
        return (ExceptionResponseModel)response;
    }
}