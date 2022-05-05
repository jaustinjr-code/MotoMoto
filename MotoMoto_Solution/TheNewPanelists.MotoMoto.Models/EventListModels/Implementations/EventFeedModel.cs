using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class EventFeedModel : IFeedModel
    {
        public string feedName { get; set; } = default!;
        public IEnumerable<IPostModel> postList { get; set; } = default!;
    }
}
