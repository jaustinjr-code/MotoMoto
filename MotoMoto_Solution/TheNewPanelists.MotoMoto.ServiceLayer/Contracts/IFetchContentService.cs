using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface IFetchContentService : IResponseService
    {
        IContentModel contentToFetch { get; set; }
    }
}