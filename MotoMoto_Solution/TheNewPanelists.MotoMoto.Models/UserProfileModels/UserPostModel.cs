using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNewPanelists.MotoMoto.Models
{
    public class UserPostModel
    { 
        public string? _postTitle { get; set; }
        public string? _contentType { get; set; }
        public string? _postDescription { get; set; }
        public DateTime? _submitUTC { get; set; }
    }
}


