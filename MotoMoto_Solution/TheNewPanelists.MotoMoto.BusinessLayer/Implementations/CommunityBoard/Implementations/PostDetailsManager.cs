// Validates if the Post input abides to the BRD
// Uses ContentManager
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using System.Reflection;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class PostDetailsManager : IContentManager
    {
        /// <summary>
        /// Empty Default Constructor
        /// </summary>
        public PostDetailsManager() { }

        /// <summary>
        /// Checks if the request input is Null or Empty
        /// Returns true if the input is null, the model is null
        /// Otherwise if model and input are the correct type return false
        /// </summary>
        /// <param name="input"></param>
        /// <returns>Boolean</returns>
        public bool IsNullOrEmptyRequest(object? input)
        {
            if (input == null) return true;
            if (!(input is FetchPostDetailsRequestModel) ||
                !(((FetchPostDetailsRequestModel)input).input is int) ||
                (int)((FetchPostDetailsRequestModel)input).input <= 0)
                return true;
            //Console.WriteLine(input.GetType());
            return false;
        }

        /// <summary>
        /// UNUSED
        /// </summary>
        /// <param name="contentModel"></param>
        /// <returns>Boolean</returns>
        public bool IsNullOrEmptyModel(IContentModel contentModel)
        {
            return false;
        }

        /// <summary>
        /// UNUSED
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>(bool, IResponseModel)</returns>
        public (bool, IResponseModel) IsContentRequestValid(IContentModel inputModel)
        {
            throw new InvalidDataException("Invalid Request");
        }

        /// <summary>
        /// Simplest request check, checks if input is null or empty
        /// If it is then throw an exception
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns>(bool, IResponseModel)</returns>
        public (bool, IResponseModel) IsRequestValid(IRequestModel inputModel)
        {
            bool valid = false;
            IResponseModel result;
            if (!IsNullOrEmptyRequest(inputModel))
            {
                valid = true;
                result = ProcessRequest(inputModel);
                if (result.isComplete == false && result.isSuccess == false)
                    valid = false;
            }
            else
            {
                result = new ExceptionResponseModel("Invalid Request");
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
            // This response would be an Exception Response Model
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
        private IResponseModel ProcessRequest(IRequestModel postInput)
        {
            IResponseService service = new FetchPostDetailsService(postInput);
            IResponseModel output = ((FetchPostDetailsService)service).FetchPostDetails();
            if (IsContentResponseValid(output))
            {
                return output;
            }
            return service.BuildDefaultResponse();
        }
    }
}