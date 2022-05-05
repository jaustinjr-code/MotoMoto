using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class UpvotedPostsModel
    {
        public int postId { get; set; }
        public string? postUsername { get; set; }
        public string? feedName { get; set; }
        public string? postTitle { get; set; }
        public string? postDescription { get; set; }
        public DateTime submitTime { get; set; }
        public string? upvoteUsername { get; set; }
    }
}
