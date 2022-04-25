using System.Reflection;
using TheNewPanelists.MotoMoto.ServiceLayer;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.BusinessLayer
{
    public class FeedManager : IContentManager
    {
        /// <summary>
        /// Default Empty Constructor
        /// </summary>
        public FeedManager() { }

        /// <summary>
        /// Checks if the request input is Null or Empty
        /// Returns true if the input is null, the model is null
        /// Else it should be passed to IsNullOrEmptyModel method
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsNullOrEmptyRequest(object? input)
        {
            if (input == null) return true;
            if (input is IFeedModel) return IsNullOrEmptyModel((IContentModel)input);
            //Console.WriteLine(input.GetType());
            foreach (PropertyInfo p in input.GetType().GetProperties())
            {
                //Console.WriteLine(typeof(IFeedModel).IsAssignableFrom(p.GetValue(input, null)!.GetType()));
                //Console.WriteLine();
                //typeof(IFeedModel).IsAssignableFrom(m.MemberType.GetType())

                // I don't care about the request type, as long as it has a IFeedModel then proceed
                // FeedManager is concerned with Feed models so a Feed model should be present
                // Source: https://stackoverflow.com/questions/4963160/how-to-determine-if-a-type-implements-an-interface-with-c-sharp-reflection
                // Checks if the current property is an implementing class of the IFeedModel interface
                if (typeof(IFeedModel).IsAssignableFrom(p.GetValue(input, null)!.GetType()))
                {
                    IContentModel inputModel = (IContentModel)((IRequestModel)input).input;
                    return IsNullOrEmptyModel(inputModel);
                }
            }
            //Console.WriteLine("No Model");
            // Check IRequestModel here for any invalid data
            throw new Exception("No Model Found");
        }

        /// <summary>
        /// Checks if the input model is empty
        /// Returns true if empty
        /// Else false
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public bool IsNullOrEmptyModel(IContentModel inputModel)
        {
            // Don't need to check if model is null because system gives warning
            // or doesn't allow you to make it null
            //if (inputModel == null) return true;
            string value = ((IFeedModel)inputModel).feedName;
            if (value.Equals("") || value == null)
                return true;
            return false;
        }

        /// <summary>
        /// Checks if the request is valid
        /// Processes the request if valid, and checks the response
        /// Else return false for invalid request
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        public bool IsContentRequestValid(IContentModel inputModel)
        {
            if (!IsNullOrEmptyRequest((IFeedModel)inputModel))
            {
                return IsContentResponseValid(ProcessRequest((IFeedModel)inputModel));
            }
            return false;
        }

        /// <summary>
        /// Checks if the response from the processed request is valid
        /// Returns false response is incomplete or an empty result
        /// Else the response must be valid
        /// </summary>
        /// <param name="outputModel"></param>
        /// <returns></returns>
        private bool IsContentResponseValid(IResponseModel outputModel)
        {
            // Check contents of IResponseModel for valid content
            if (!outputModel.isComplete) return false;
            // May not need the Count condition because the result would be apparent if there were no results
            // And what if the output is not a List? Right now I don't any new Feed services is necessary, but
            // that could change.
            if (!outputModel.isSuccess
                && (outputModel.output == null || ((List<IFeedEntity>)outputModel.output).Count == 0))
                return false;
            // Should I be checking the response message too? How do you test something that can be anything?
            return true;
        }

        /// <summary>
        /// Processes the inputted request
        /// Private because this should only run if the request is valid
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        private IResponseModel ProcessRequest(IFeedModel inputModel)
        {
            // The inputModel may have a string containing "feed"
            //if (inputModel.type.Equals("feed")) 
            //{
            IFetchContentService service = new LoadFeedService(inputModel);
            IResponseModel output = ((LoadFeedService)service).LoadFeed();
            if (IsContentResponseValid(output))
            {
                return output;
            }
            return service.BuildDefaultResponse();
            //}
        }
    }
}