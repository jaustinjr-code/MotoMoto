using TheNewPanelists.MotoMoto.DataAccess;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;
using System;
//using System.Diagnostics;

namespace TheNewPanelists.MotoMoto.ServiceLayer
{
    public class LoadFeedService : IFetchContentService
    {
        public IContentModel contentToFetch { get; set; }

        public LoadFeedService(IContentModel content)
        {
            contentToFetch = content;
        }

        public IResponseModel LoadFeed()
        {
            // Should this be handled by the Business Layer?
            //if (contentToFetch == null) throw new NullReferenceException();
            IContentDataAccess contentDataAccess = new LoadFeedDataAccess();
            IFeedEntity? result = ((LoadFeedDataAccess)contentDataAccess).FetchAllPosts((IFeedModel)contentToFetch);
            if (result != null)
                return BuildResponse(result);
            // What if the result is supposed to be null?
            // How do you distinguish the results?
            return BuildDefaultResponse();
        }

        public IResponseModel BuildResponse(IContentEntity contentResult)
        {
            string message = ((IFeedModel)contentToFetch).feedName + " Feed Retrieved";
            //Debug.WriteLine(message);
            // When will isComplete be false? Can use an async request to then timeout for incompleteness
            return new LoadFeedResponseModel(message, true, true);
        }

        public IResponseModel BuildDefaultResponse()
        {
            return new LoadFeedResponseModel("No result found", true, false);
        }
    }
}