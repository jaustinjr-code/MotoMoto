using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface ISubmitContentService : IResponseService
    {
        IContentModel contentToSubmit { get; set; }
        IResponseModel SubmitContent();
    }
}