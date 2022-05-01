using System;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;

// Use for Post Details
namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class FetchUpvotesService : IResponseService
    {
        public IRequestModel contentToFetch { get; set; }

        /// <summary>
        /// Overloaded Constructor, instantiates request model
        /// </summary>
        /// <param name="content"></param>
        public FetchUpvotesService(IRequestModel content)
        {
            contentToFetch = content;
        }

        /// <summary>
        /// Fetches the upvote total for the specific post
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel FetchPostUpvotes()
        {
            IDataAccess upvoteDataAccess = new UpvoteAnalyticDataAccess();
            try
            {
                int result = ((UpvoteAnalyticDataAccess)upvoteDataAccess).FetchPostUpvoteTotal(contentToFetch);
                if (result >= 0)
                    return BuildResponse(result);
                return BuildDefaultResponse();
            }
            catch (Exception)
            {
                return BuildExceptionResponse("Incomplete Operation");
            }
        }

        /// <summary>
        /// Fetches the upvote total for the specific comment
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel FetchCommentUpvotes()
        {
            IDataAccess upvoteDataAccess = new UpvoteAnalyticDataAccess();
            try
            {
                int result = ((UpvoteAnalyticDataAccess)upvoteDataAccess).FetchCommentUpvoteTotal((IInteractionModel)contentToFetch);
                if (result >= 0)
                    return BuildResponse(result);
                return BuildDefaultResponse();
            }
            catch (Exception)
            {
                return BuildExceptionResponse("Incomplete Operation");
            }
        }

        /// <summary>
        /// Builds the response model for the total upvotes
        /// </summary>
        /// <param name="result"></param>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildResponse(object result)
        {
            string message = "Upvote Total: " + (int)result;
            bool complete = true;
            bool success = true;
            return new UpvoteAnalyticResponseModel((int)result, message, complete, success);
        }

        /// <summary>
        /// Builds the default response if total is negative
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildDefaultResponse()
        {
            string message = "Upvote Total: 0";
            bool complete = true;
            bool success = false;
            return new UpvoteAnalyticResponseModel(0, message, complete, success);
        }

        /// <summary>
        /// Builds the exception response if an exception was thrown
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildExceptionResponse(string errorMessage)
        {
            return new ExceptionResponseModel(errorMessage);
        }
    }
}