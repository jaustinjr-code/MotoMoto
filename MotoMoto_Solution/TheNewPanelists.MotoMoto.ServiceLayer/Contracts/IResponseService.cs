using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface IResponseService
    {
        IResponseModel BuildResponse(object result);
        IResponseModel BuildDefaultResponse();
        IResponseModel BuildExceptionResponse(string errorMessage);
    }
}