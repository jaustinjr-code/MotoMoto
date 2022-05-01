using System;
using System.Collections.Generic;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    // This class is used in accordance with the Fetch Post Details Service
    // Although public, the return data is not useful if not applied to a Feed Post Response Model
    // The model is created in the Fetch Post Details Service
    public class FetchCommentsService : IResponseService
    {
        public IRequestModel contentToFetch { get; set; }

        /// <summary>
        /// Overloaded Constructor, instantiates the IContentModel
        /// </summary>
        /// <param name="content"></param>
        public FetchCommentsService(IRequestModel content)
        {
            contentToFetch = (FetchPostDetailsRequestModel)content;
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
                IEnumerable<DataStoreComment> commentList = (List<DataStoreComment>)((CommentContentDataAccess)commentDataAccess).FetchComments(contentToFetch);

                if (((List<DataStoreComment>)commentList).Count > 0)
                    return BuildResponse(commentList);
                return BuildDefaultResponse();
            }
            catch (Exception)
            {
                return BuildExceptionResponse("Incomplete Operation");
            }
        }

        /// <summary>
        /// Builds the response model specific to the result
        /// </summary>
        /// <param name="result"></param>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildResponse(object result)
        {
            string message = ((List<DataStoreComment>)result).Count + " Comments Retrieved";
            bool complete = true;
            bool success = true;
            IResponseModel response = new CommentPostResponseModel((IEnumerable<DataStoreComment>)result, message, complete, success);
            //((CommentPostResponseModel)response).output = (IEnumerable<IPostEntity>)result;
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