using System;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class SubmitPostService : ISubmitContentService
    {
        public IContentModel contentToSubmit { get; set; }

        /// <summary>
        /// Overloaded Constructor, instantiates the IContentModel
        /// IContentModel is what needs to be submitted
        /// </summary>
        /// <param name="content"></param>
        public SubmitPostService(IContentModel content)
        {
            contentToSubmit = (IPostModel)content;
        }

        /// <summary>
        /// Submits the content using PostContentDataAccess
        /// Builds a response on true, default response on false, and exception response on caught exception
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel SubmitContent()
        {
            IDataAccess postDataAccess = new PostContentDataAccess();
            try
            {
                bool result = ((PostContentDataAccess)postDataAccess).PutPost((IPostModel)contentToSubmit);
                if (result)
                    return BuildResponse(result);
                return BuildDefaultResponse();
            }
            catch (Exception e)
            {
                return BuildExceptionResponse(e.Message);
            }
            //throw new Exception();
        }

        /// <summary>
        /// Builds the response model specific to the content submission
        /// </summary>
        /// <param name="result"></param>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildResponse(object result)
        {
            string message = "New Post Submitted";
            bool complete = true;
            bool success = (bool)result;
            IResponseModel response = new FeedPostResponseModel(message, complete, success);
            return response;
        }

        /// <summary>
        /// Builds the default response model if false result
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildDefaultResponse()
        {
            string message = "No Post Submitted";
            bool complete = true;
            bool success = false;
            IResponseModel response = new FeedPostResponseModel(message, complete, success);
            return response;
        }

        /// <summary>
        /// Builds the exception response model if the data access operation throws an exception
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildExceptionResponse(string errorMessage)
        {
            IResponseModel response = new ExceptionResponseModel(errorMessage);
            return response;
        }
    }
}