using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface IFetchContentService
    {
        IContentModel contentToFetch { get; set; }
        IResponseModel BuildResponse(IContentEntity resultContent);
        IResponseModel BuildDefaultResponse();
    }
}