using System;
using System.Collections.Generic;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class FetchCommentsService : IFetchContentService
    {
        public IContentModel contentToFetch { get; set; }

        /// <summary>
        /// Overloaded Constructor, instantiates the IContentModel
        /// </summary>
        /// <param name="content"></param>
        public FetchCommentsService(IContentModel content)
        {
            contentToFetch = (IPostModel)content;
        }

        /// <summary>
        /// Fetches the comments from a specific post
        /// Builds a response on non empty result, default response on empty result, and
        /// exception response on caught exception
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel FetchComments()
        {
            IDataAccess commentDataAccess = new CommentContentDataAccess();
            try
            {
                IEnumerable<IPostEntity> commentList = (List<IPostEntity>)((CommentContentDataAccess)commentDataAccess).FetchComments((IPostModel)contentToFetch);
                if (((List<IPostEntity>)commentList).Count > 0)
                    return BuildResponse(commentList);
                return BuildDefaultResponse();
            }
            catch (Exception e)
            {
                return BuildExceptionResponse(e.Message);
            }
        }

        /// <summary>
        /// Builds the response model specific to the result
        /// </summary>
        /// <param name="result"></param>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildResponse(object result)
        {
            string message = ((List<IPostEntity>)result).Count + " Comments Retrieved";
            bool complete = true;
            bool success = true;
            IResponseModel response = new CommentPostResponseModel(message, complete, success);
            ((CommentPostResponseModel)response).output = (IEnumerable<IPostEntity>)result;
            return response;
        }

        /// <summary>
        /// Build default response if result is false
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildDefaultResponse()
        {
            string message = "No Comments Retrieved";
            bool complete = true;
            bool success = true;
            IResponseModel response = new CommentPostResponseModel(message, complete, success);
            return response;
        }

        /// <summary>
        /// Build exception response if data access operation throws an exception
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