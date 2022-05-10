using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class UserPostModel
    { 
        public int postId { get; set; }
        public string? postTitle { get; set; }
        public string? feedName { get; set; }
        public string? postDescription { get; set; }
        public DateTime? submitUTC { get; set; }
    }
}


