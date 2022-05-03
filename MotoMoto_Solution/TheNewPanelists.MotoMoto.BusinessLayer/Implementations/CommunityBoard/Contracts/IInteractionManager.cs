// Interface for UpvoteCommentManager and UpvotePostManager
// Should include validation of authenticated and authorized user
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public interface IInteractionManager
    {
        bool IsInteractionAuthorized(IInteractionModel inputModel);
        bool IsInteractionDetailsValid(IInteractionModel inputModel);
        (bool, IResponseModel) IsInteractionRequestValid(IInteractionModel inputModel);
    }
}