using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class UpvotedPostsModel
    {
        public int _likeId { get; set; }
        public int _postId { get; set; }
        public bool _userVote { get; set; }
    }
}
