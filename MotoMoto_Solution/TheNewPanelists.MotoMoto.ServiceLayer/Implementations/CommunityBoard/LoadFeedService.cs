using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System.Collections.Generic;
using System;
//using System.Diagnostics;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class LoadFeedService : IFetchContentService
    {
        public IContentModel contentToFetch { get; set; }

        /// <summary>
        /// Default Constructor, instantiates the IContentModel
        /// IContentModel is what needs to be fetched
        /// </summary>
        /// <param name="content"></param>
        public LoadFeedService(IContentModel content)
        {
            contentToFetch = content;
        }

        /// <summary>
        /// Service for retrieving posts for the specific feed according to the IContentModel
        /// </summary>
        /// <returns></returns>
        public IResponseModel LoadFeed()
        {
            // Takes the result from the DAL and builds a response model based on it
            IDataAccess contentDataAccess = new LoadFeedDataAccess();
            try
            {
                IFeedEntity? result = ((LoadFeedDataAccess)contentDataAccess).FetchAllPosts((IFeedModel)contentToFetch);
                if (result != null)
                    return BuildResponse(result);
            }
            catch (Exception e)
            {
                e.ToString(); // Use Logger for this
            }
            // What if the result is supposed to be null?
            // How do you distinguish the results?
            return BuildDefaultResponse();
        }

        /// <summary>
        /// Builds the response model specific to the content result
        /// </summary>
        /// <param name="contentResult"></param>
        /// <returns></returns>
        public IResponseModel BuildResponse(IContentEntity contentResult)
        {
            string message = ((IFeedModel)contentToFetch).feedName + " Feed Retrieved "
                + ((List<IPostEntity>)((IFeedEntity)contentResult).postList).Count + " Results Found";
            //Debug.WriteLine(message);
            // When will isComplete be false? Can use an async request to then timeout for incompleteness
            return new LoadFeedResponseModel((IFeedEntity)contentResult, message, true, true);
        }

        /// <summary>
        /// Builds the default response model if null result is returned
        /// </summary>
        /// <returns></returns>
        public IResponseModel BuildDefaultResponse()
        {
            return new LoadFeedResponseModel("No results found", true, false);
        }
    }
}