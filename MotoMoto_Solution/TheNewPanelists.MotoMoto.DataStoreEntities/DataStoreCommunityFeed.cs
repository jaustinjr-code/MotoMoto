namespace TheNewPanelists.MotoMoto.DataStoreEntities
{
    public class DataStoreCommunityFeed : IFeedEntity
    {
        public string feedName { get; }
        public IEnumerable<IPostEntity> postList { get; }
        public IAnalytic? feedAnalytic { get; }

        public DataStoreCommunityFeed(string name, IEnumerable<IPostEntity> posts, IAnalytic? analytic)
        {
            feedName = name;
            postList = posts;
            feedAnalytic = analytic;
        }

        public override string ToString()
        {
            string result = feedName;
            // string result = feedName + " " + postList.Count();
            foreach (IPostEntity p in postList)
            {
                result += "\t" + p;
            }
            return result;
        }

        // public string printEnumerable() {

        // }
    }
}