// Validates if the Comment input abides to the BRD
// Uses ContentManager
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using System.Reflection;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class CommentManager : IContentManager
    {
        /// <summary>
        /// Empty Default Constructor
        /// </summary>
        public CommentManager() { }

        /// <summary>
        /// Checks if the request input is Null or Empty
        /// Returns true if the input is null, the model is null
        /// Else it should be passed to IsNullOrEmptyModel method
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Boolean</returns>
        public bool IsNullOrEmptyRequest(object? input)
        {
            if (input == null) return true;
            else if (input is CommentPostModel)
                return IsNullOrEmptyModel((IPostModel)input);
            // Else the input was taken as a RequestModel
            foreach (PropertyInfo p in input.GetType().GetProperties())
            {
                // Source: https://stackoverflow.com/questions/4963160/how-to-determine-if-a-type-implements-an-interface-with-c-sharp-reflection
                // Checks if the current property is an implementing class of the IFeedModel interface
                if (typeof(IPostModel).IsAssignableFrom(p.GetValue(input, null)!.GetType()))
                {
                    IContentModel inputModel = (IContentModel)((IRequestModel)input).input;
                    return IsNullOrEmptyModel(inputModel);
                }
            }

            throw new Exception("No Model Found");
        }

        /// <summary>
        /// Checks if the input model is empty or null
        /// Returns true if required members are empty or null
        /// IContentModel is a FeedPostModel in this context
        /// </summary>
        /// <param name="contentModel"></param>
        /// <returns>Boolean</returns>
        public bool IsNullOrEmptyModel(IContentModel contentModel)
        {
            // Check for null or empty inputs
            // Post ID must not be zero or negative
            if (contentModel == null) return true;
            if (((CommentPostModel)contentModel).postID <= 0 ||
                ((CommentPostModel)contentModel).postDescription == null ||
                ((CommentPostModel)contentModel).postUser == null) return true;
            else if (((CommentPostModel)contentModel).postUser!.Equals("") ||
                ((CommentPostModel)contentModel).postDescription!.Equals("")) return true;
            return false;
        }

        /// <summary>
        /// Checks if the client's input is valid
        /// Inputs must be within bounds specified in BRD
        /// </summary>
        /// <param name="contentModel"></param>
        /// <returns>Boolean</returns>
        public bool IsValidRequestForm(IContentModel contentModel)
        {
            // Need to check if description is null before taking the length
            if (((IPostModel)contentModel).postDescription == null) return false;

            // Only BRD requirement is description length
            int descriptionLengthMin = 1, descriptionLengthMax = 1000;
            int descriptionLength = ((IPostModel)contentModel).postDescription!.Length;
            if (descriptionLengthMin <= descriptionLength && descriptionLength <= descriptionLengthMax)
                return true;

            return false;
        }

        /// <summary>
        /// Checks if the request is valid
        /// Processes the request if valid, and checks the response
        /// Else throw Exception for invalid request
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>(bool, IResponseModel)</returns>
        public (bool, IResponseModel) IsContentRequestValid(IContentModel inputModel)
        {
            bool valid = false;
            IResponseModel result;
            try
            {
                if (!IsNullOrEmptyRequest(inputModel) && IsValidRequestForm((IPostModel)inputModel))
                {
                    valid = true;
                    result = ProcessRequest((IPostModel)inputModel);

                    // ExceptionResponseModel is a valid response but there's no check for it to change valid back to false
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
        /// Checks if the response from the processed request is valid
        /// Returns false if the response is incomplete or has invalid status booleans
        /// Otherwise the response must be valid
        /// </summary>
        /// <param name="response"></param>
        /// <returns>Boolean</returns>
        private bool IsContentResponseValid(IResponseModel response)
        {
            if (response.isComplete)
                return true;
            else if (response.isComplete == false && response.isSuccess == false)
                return true;
            return false;
        }

        /// <summary>
        /// Processes the inputted request
        /// Private because this should only run if the request is valid
        /// </summary>
        /// <param name="commentInput"></param>
        /// <returns>IResponseModel</returns>
        private IResponseModel ProcessRequest(IPostModel commentInput)
        {
            ISubmitContentService service = new SubmitCommentPostService(commentInput);
            IResponseModel output = ((SubmitCommentPostService)service).SubmitContent(); // I don't think this needs to be casted to work

            if (IsContentResponseValid(output))
                return output;
            return service.BuildDefaultResponse();
        }
    }
}