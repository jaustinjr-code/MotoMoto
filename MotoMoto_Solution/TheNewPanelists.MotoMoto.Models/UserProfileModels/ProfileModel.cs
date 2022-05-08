using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNewPanelists.MotoMoto.DataStoreEntities;

namespace TheNewPanelists.MotoMoto.Models
{
    public class ProfileModel
    {
        public int? userId { get; set; }
        public string? username { get; set; }
        public bool status { get; set; }
        public bool eventAccount { get; set; }
        public string? profileImagePath { get; set; }
        public string? profileDescription { get; set; }
        public List<UpvotedPostsModel>? upVotedPosts { get; set; }

        public List<UserPostModel>? userPosts { get; set; }
    }
}