using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class UpvotedPostsModel
    {
        public int likeid { get; set; }
        public int postid { get; set; }
        public bool vote { get; set; }
    }
}
