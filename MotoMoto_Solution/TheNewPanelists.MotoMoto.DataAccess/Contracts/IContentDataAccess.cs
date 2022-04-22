using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.Models;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.DataAccess
{
    public interface IContentDataAccess : IDataAccess
    {
        string SqlGenerator();
        IFeedEntity? GetPost(IFeedModel postInput); // A Post belongs to Feed t/f is a Feed Entity
        public IEnumerable<IPostEntity>? FetchAllPosts(IFeedModel feedInput); // A Post model belongs to a Feed model
    }
}