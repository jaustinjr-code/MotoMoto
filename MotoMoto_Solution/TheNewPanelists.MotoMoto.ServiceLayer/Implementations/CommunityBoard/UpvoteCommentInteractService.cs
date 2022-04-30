using System;
using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class UpvoteCommentInteractService : IContentInteractionService
    {
        public IInteractionModel contentToInteract { get; set; }

        /// <summary>
        /// Overloaded Constructor, instantiates IInteractionModel
        /// </summary>
        /// <param name="content"></param>
        public UpvoteCommentInteractService(IInteractionModel content)
        {
            contentToInteract = (UpvoteCommentModel)content;
        }

        /// <summary>
        /// Puts an upvote for the specific comment
        /// Builds a response on true, default response on false, and exception response on caught exception
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel InteractContent()
        {
            IDataAccess interactDataAccess = new CommentContentDataAccess();
            try
            {
                bool result = ((CommentContentDataAccess)interactDataAccess).PutUpvoteComment(contentToInteract);
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
        /// Builds response model for the specific result
        /// </summary>
        /// <param name="result"></param>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildResponse(object result)
        {
            string message = "Upvoted Comment";
            bool complete = true;
            bool success = (bool)result;
            IResponseModel response = new CommentPostResponseModel(message, complete, success);
            return response;
        }

        /// <summary>
        /// Builds the default response if the result is false
        /// </summary>
        /// <returns>IResponseModel</returns>
        public IResponseModel BuildDefaultResponse()
        {
            string message = "Comment Not Upvoted";
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