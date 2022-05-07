using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface IContentInteractionService : IResponseService
    {
        IInteractionModel contentToInteract { get; set; }

        IResponseModel InteractContent();
    }
}