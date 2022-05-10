using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public interface IResponseService
    {
        /// <summary>
        /// Builds the response of a result object that can be casted to any
        /// resulting data access object
        /// </summary>
        /// <param name="result"></param>
        /// <returns>IResponseModel</returns>
        IResponseModel BuildResponse(object result);

        /// <summary>
        /// Builds the default response of an improper data access result object
        /// </summary>
        /// <returns>IResponseModel</returns>
        IResponseModel BuildDefaultResponse();

        /// <summary>
        /// Builds the exception response of an exception thrown by the data access object
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>IResponseModel</returns>
        IResponseModel BuildExceptionResponse(string errorMessage);
    }
}