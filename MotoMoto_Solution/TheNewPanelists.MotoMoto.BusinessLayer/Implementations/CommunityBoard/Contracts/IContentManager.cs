using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public interface IContentManager
    {
        //IRequestModel
        bool IsNullOrEmptyRequest(object? input);
        (bool, IResponseModel?) IsContentRequestValid(IContentModel inputModel);
    }
}