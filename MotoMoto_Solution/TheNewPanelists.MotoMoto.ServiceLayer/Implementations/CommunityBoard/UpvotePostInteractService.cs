using System;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class UpvotePostInteractService : IContentInteractionService
    {
        public IInteractionModel contentToInteract { get; set; }

        /// <summary>
        /// Overloaded Constructor, instantiates the IInteractionModel
        /// </summary>
        /// <param name="content"></param>
        public UpvotePostInteractService(IInteractionModel content)
        {
            contentToInteract = (UpvotePostModel)content;
        }

        /// <summary>
        /// Puts an upvote for the specific post
        /// Builds a response on true, default response on false, and exception response on caught exception
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel InteractContent()
        {
            IDataAccess interactDataAccess = new PostContentDataAccess();
            try
            {
                bool result = ((PostContentDataAccess)interactDataAccess).PutUpvotePost(contentToInteract);
                if (result)
                    return BuildResponse(result);
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
            string message = "Upvoted Post";
            bool complete = true;
            bool success = (bool)result;
            IResponseModel response = new FeedPostResponseModel(message, complete, success);
            return response;
        }

        /// <summary>
        /// Builds the default response model if result is false
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildDefaultResponse()
        {
            string message = "Post Not Upvoted";
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