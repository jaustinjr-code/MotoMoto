// Validates if the user is allowed to upvote a post
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class UpvotePostManager : IInteractionManager
    {
        /// <summary>
        /// Empty Default Constructor
        /// </summary>
        public UpvotePostManager() { }

        /// <summary>
        /// Checks if the user is authorized for this interaction
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>Boolean</returns>
        public bool IsInteractionAuthorized(IInteractionModel inputModel)
        {
            AuthorizationService service = new AuthorizationService();
            // return service.CheckAuthorized(inputModel.interactUsername);
            return service.CheckAuthorized(inputModel.interactUsername, "upvoteContent");
        }

        /// <summary>
        /// Checks if the input is valid
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>Boolean</returns>
        public bool IsInteractionDetailsValid(IInteractionModel inputModel)
        {
            // No need to check the username, that is done in Authorization step
            if (((UpvotePostModel)inputModel).contentId > 0) return true;
            return false;
        }

        /// <summary>
        /// Checks if the interaction request is valid
        /// Does authorization and input validation before processing request
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>(bool, IResponseModel)</returns>
        public (bool, IResponseModel) IsInteractionRequestValid(IInteractionModel inputModel)
        {

            bool valid = false;
            IResponseModel result;
            try
            {
                if (IsInteractionAuthorized(inputModel) && IsInteractionDetailsValid(inputModel))
                {
                    valid = true;
                    result = ProcessRequest(inputModel);

                    // Checks if the result was an exception which has false booleans
                    if (result.isComplete == false && result.isSuccess == false)
                        valid = false;
                }
                else
                {
                    result = new ExceptionResponseModel("Invalid Request");
                }
            }
            catch (Exception e)
            {
                valid = false;
                result = new ExceptionResponseModel(e.Message);
            }
            return (valid, result);
        }

        /// <summary>
        /// Checks if the response model is valid
        /// </summary>
        /// <param name="response"></param>
        /// <returns>Boolean</returns>
        private bool IsInteractionResponseValid(IResponseModel response)
        {
            if (response.isComplete)
                return true;
            else if (response.isComplete == false && response.isSuccess == false)
                return true;
            return false;
        }

        /// <summary>
        /// Processes the interaction input model
        /// Private to avoid running the operation without validating request
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>IResponseModel</returns>
        private IResponseModel ProcessRequest(IInteractionModel inputModel)
        {
            IContentInteractionService service = new UpvotePostInteractService(inputModel);
            IResponseModel response = ((UpvotePostInteractService)service).InteractContent();

            if (IsInteractionResponseValid(response))
                return response;
            return service.BuildDefaultResponse(); ;
        }
    }
}