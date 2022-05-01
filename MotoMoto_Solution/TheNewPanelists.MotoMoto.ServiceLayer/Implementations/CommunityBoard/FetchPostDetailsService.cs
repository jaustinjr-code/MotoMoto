using System;
using System.Collections.Generic;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class FetchPostDetailsService : IResponseService
    {
        IRequestModel contentToFetch { get; set; }

        /// <summary>
        /// Overload Constructor, instantiates IRequestModel
        /// </summary>
        /// <param name="content"></param>
        public FetchPostDetailsService(IRequestModel content)
        {
            contentToFetch = (FetchPostDetailsRequestModel)content;
        }

        /// <summary>
        /// Fetches the expanded details of a post including comments and images
        /// Builds a response on non null result, default response on null result, and
        /// exception response on caught exception
        /// NOTE: currently images are not supported
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel FetchPostDetails()
        {
            IDataAccess postDataAccess = new PostContentDataAccess();
            IResponseService fetchCommentService = new FetchCommentsService(contentToFetch);
            IResponseService fetchUpvotesService = new FetchUpvotesService(contentToFetch);
            // Image Service is the last thing to retrieve for post details

            // FetchCommentsService is a helper class for this occasion
            // Look at FetchCommentsService for Fetch Comments
            try
            {
                IPostEntity post = ((PostContentDataAccess)postDataAccess).FetchPost(contentToFetch);
                ((DataStorePost)post).upvoteCount = (int)((FetchUpvotesService)fetchUpvotesService).FetchPostUpvotes().output!;
                //Console.WriteLine(post);

                // Fetch the comments associated to the specified post id and assign it to the post entity
                IResponseModel commentResponse = ((FetchCommentsService)fetchCommentService).FetchComments();
                if (commentResponse.isComplete && commentResponse.isSuccess && commentResponse.output != null)
                    ((DataStorePost)post).commentList = (List<DataStoreComment>)((CommentPostResponseModel)commentResponse).output!;

                if (post != null)
                    return BuildResponse(post);
                return BuildDefaultResponse();
            }
            catch (Exception e)
            {
                // Log the exception
                return BuildExceptionResponse(e.Message);
            }
        }

        /// <summary>
        /// Builds response model specific to the response
        /// If a comment list exists then that will be included into the response
        /// </summary>
        /// <param name="result"></param>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildResponse(object result)
        {
            string message = "Post Details Retrieved";
            bool complete = true;
            bool success = true;
            IResponseModel response = new FeedPostResponseModel((DataStorePost)result, message, complete, success);

            return response;
        }

        /// <summary>
        /// Builds a default response if the response is null
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildDefaultResponse()
        {
            string message = "No Post Found";
            bool complete = true;
            bool success = false;
            IResponseModel response = new FeedPostResponseModel(message, complete, success);
            return response;
        }

        /// <summary>
        /// Builds exception response if any data access operation throws an exception
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