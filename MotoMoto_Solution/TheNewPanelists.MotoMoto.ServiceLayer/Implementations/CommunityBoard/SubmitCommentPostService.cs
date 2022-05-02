using System;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class SubmitCommentPostService : ISubmitContentService
    {
        public IContentModel contentToSubmit { get; set; }

        /// <summary>
        /// Overloaded Constructor, instantiates the IContentModel
        /// </summary>
        /// <param name="content"></param>
        public SubmitCommentPostService(IContentModel content)
        {
            contentToSubmit = (IPostModel)content;
        }

        /// <summary>
        /// Submits content using CommentContentDataAccess
        /// Builds a response on true, default response on false, and exception response on caught exception
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel SubmitContent()
        {
            IDataAccess commentDataAccess = new CommentContentDataAccess();
            try
            {
                bool result = ((CommentContentDataAccess)commentDataAccess).PutCommentPost((IPostModel)contentToSubmit);
                if (result)
                    return BuildResponse(result);
                return BuildDefaultResponse();
            }
            catch (Exception)
            {
                // Log the exception
                return BuildExceptionResponse("Incomplete Operation");
            }
        }

        /// <summary>
        /// Builds response model specific to the result
        /// </summary>
        /// <param name="result"></param>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildResponse(object result)
        {
            string message = "New Comment Submitted";
            bool complete = true;
            bool success = (bool)result;
            IResponseModel response = new CommentPostResponseModel(message, complete, success);
            return response;
        }

        /// <summary>
        /// Builds the default response if result is false
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildDefaultResponse()
        {
            string message = "No Comment Submitted";
            bool complete = true;
            bool success = false;
            IResponseModel response = new CommentPostResponseModel(message, complete, success);
            return response;
        }

        /// <summary>
        /// Builds exception response if the data access operation throws an exception
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