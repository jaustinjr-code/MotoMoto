// Validates if the Post input abides to the BRD
// Uses ContentManager
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using System.Reflection;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class PostManager : IContentManager
    {
        /// <summary>
        /// Empty Default Constructor
        /// </summary>
        public PostManager() { }

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
            else if (input is FeedPostModel)
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
            // Images don't need to be checked because user doesn't have to submit images 
            if (contentModel == null) return true;
            if (((IPostModel)contentModel).postTitle == null ||
                ((IPostModel)contentModel).contentType == null ||
                ((IPostModel)contentModel).postUser == null ||
                ((IPostModel)contentModel).postDescription == null) return true;
            else if (((IPostModel)contentModel).postTitle!.Equals("") ||
                ((IPostModel)contentModel).contentType!.Equals("") ||
                ((IPostModel)contentModel).postUser!.Equals("") ||
                ((IPostModel)contentModel).postDescription!.Equals("")) return true;
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
            // Need to check if title or description is null before taking the lengths
            if (((IPostModel)contentModel).postTitle == null ||
                ((IPostModel)contentModel).postDescription == null) return false;

            // Input bounds specified by the BRD
            int titleLengthMin = 15, titleLengthMax = 75;
            int descriptionLengthMin = 1, descriptionLengthMax = 1500;

            // Input lenghts
            int titleLength = ((IPostModel)contentModel).postTitle!.Length;
            int descriptionLength = ((IPostModel)contentModel).postDescription!.Length;

            // Bounds check
            if ((titleLengthMin <= titleLength && titleLength <= titleLengthMax) &&
                (descriptionLengthMin <= descriptionLength && descriptionLength <= descriptionLengthMax))
                return true;

            // Need to check if Image size is within memory bounds
            // Username and Feed name are not check because the user doesn't input these
            // System will catch an incorrect Username or Feed name in a lower layer
            return false;
        }

        /// <summary>
        /// Checks if the request is valid
        /// Processes the request if valid, and checks the response
        /// Else throw exception for invalid request
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>(bool, IResponseModel)</returns>
        public (bool, IResponseModel) IsContentRequestValid(IContentModel inputModel)
        {
            if (!IsNullOrEmptyRequest(inputModel) && IsValidRequestForm((IPostModel)inputModel))
            {
                return (true, ProcessRequest((IPostModel)inputModel));
            }
            //return (false, null);
            throw new InvalidDataException("Invalid Request");
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
        /// <param name="postInput"></param>
        /// <returns>IResponseModel</returns>
        private IResponseModel ProcessRequest(IPostModel postInput)
        {
            ISubmitContentService service = new SubmitPostService(postInput);
            IResponseModel output = service.SubmitContent(); // I don't think this needs to be casted to work
            if (IsContentResponseValid(output))
            {
                return output;
            }
            return service.BuildDefaultResponse();
        }
    }
}