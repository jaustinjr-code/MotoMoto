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
        public int? _userId { get; set; }
        public string? _username { get; set; }
        public bool _status { get; set; }
        public bool _eventAccount { get; set; }
        public string? _profileImagePath { get; set; }
        public string? _profileDescription { get; set; }
        public List<UpvotedPostsModel>? _upVotedPosts { get; set; }

        public List<UserPostModel>? _userPosts { get; set; }
    }
}