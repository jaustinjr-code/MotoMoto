using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class UpvotedPostsModel
    {
        public int likeId { get; set; }
        public int postId { get; set; }
        public bool userVote { get; set; }
    }
}
